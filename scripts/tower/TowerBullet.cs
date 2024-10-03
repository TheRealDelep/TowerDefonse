using Godot;

public partial class TowerBullet : Node3D, IPoolable
{
    public bool IsActive { get; set; }
    public Enemy target;

    public float Speed;

    public override void _Process(double delta)
    {
        if (!IsActive) 
        { 
            Visible = false;
            return; 
        }

        if (target is null) 
        {
            IsActive = false;
            return;
        }

        Visible = true;

        var dir = (target.Position - Position).Normalized();
        Position += dir * Speed * (float)delta;

        if (Position.DistanceTo(target.Position) < 0.5) 
        {
            target.DestroyEnemy();
            IsActive = false;
        }
    }
}