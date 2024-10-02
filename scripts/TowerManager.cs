using Godot;

public partial class TowerManager : Node3D
{
    [Export] private TowerConstructionChannel channel;

    private TowerModel towerToBuild;
    private Vector3 constructionSitePosition;
    
    public override void _Ready()
    {
        channel.TowerSelected += OnConstructionSelected;
        channel.TowerSiteReached += OnConstructionSiteReached;
        channel.TowerRequested += OnConstructionRequested;
    }

    private void OnConstructionRequested(Vector3 worldPos, Vector2 _)
    {
        constructionSitePosition = worldPos;
    }

    private void OnConstructionSelected(TowerModel model) 
    {
        towerToBuild = model;
    }

    private void OnConstructionSiteReached()
    {
        var instance = towerToBuild.PrefabPath.Instantiate<Node3D>();
        instance.Position = constructionSitePosition;
        AddChild(instance);
    }
}
