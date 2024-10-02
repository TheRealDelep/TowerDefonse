using Godot;

public partial class PlayerController : CharacterBody3D
{
    [Export] private float moveSpeed;
    [Export] private Camera3D camera;
    [Export] private CollisionObject3D ground;
    [Export] private TowerConstructionChannel towerConstructionChannel;

    private Vector3? target = null;
    private Vector3? constructionSite = null;

    public override void _PhysicsProcess(double delta)
    {
        if (target is not null) { Move((float)delta); }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("LeftClick")) 
        { 
            target = GetClickTarget() switch 
            {
                null => null,
                Vector3 v => v with { Y = Position.Y }
            };  
        }

        if (Input.IsActionJustPressed("RightClick")) 
        {
            var worldPos = GetClickTarget(); 

            if (worldPos is null) { return; }

            constructionSite = worldPos.Value with { Y = Position.Y };

            towerConstructionChannel.FireConstructionSelectionRequest(worldPos.Value, GetViewport().GetMousePosition());
            towerConstructionChannel.TowerSelected += OnTowerSelected;
        }
    }

    private void OnTowerSelected(TowerModel model)
    {
        target = constructionSite;
        towerConstructionChannel.TowerSelected -= OnTowerSelected;
    }

    private void Move(float delta) 
    {
        LookAt(target.Value, Vector3.Up, true);

        var dir = (target.Value - Position).Normalized();
        var moveDelta = dir * moveSpeed * (float)delta;

        var newDir = (target.Value - (Position + moveDelta)).Normalized(); 
        if (newDir.Dot(dir) < 0) 
        {
            moveDelta = target.Value - Position;

            if (constructionSite.HasValue && target.Value == constructionSite.Value) 
            {
                towerConstructionChannel.FireConstructionSiteReached();
                constructionSite = null;
            }

            target = null;
        }

        MoveAndCollide(moveDelta);
    }

    private Vector3? GetClickTarget()
    {
        var mousePos = GetViewport().GetMousePosition();
        var origin = camera.ProjectRayOrigin(mousePos);
        var destination = origin + (camera.ProjectRayNormal(mousePos) * 1000);

        var space = GetWorld3D().DirectSpaceState;
        var query = PhysicsRayQueryParameters3D.Create(origin, destination);

        query.CollideWithBodies = true;
        query.CollideWithAreas = false;
        query.CollisionMask = 2;

        var res = space.IntersectRay(query);
        if (res.Count > 0)
        {
            if ((Rid)res["rid"] == ground.GetRid()) 
            {
                return (Vector3)res["position"];
            }
        }

        return null;
    }
}