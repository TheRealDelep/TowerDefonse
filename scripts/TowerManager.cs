using Godot;

public partial class TowerManager : Node3D
{
    [Export] private TowerConstructionChannel channel;

    private TowerModel towerToBuild;
    private Vector3 constructionSitePosition;
    
    public override void _Ready()
    {
        channel.ConstructionSelected += OnConstructionSelected;
        channel.ConstructionSiteReached += OnConstructionSiteReached;
    }

    private void OnConstructionSelected(TowerModel model, Vector3 position) 
    {
        towerToBuild = model;
        constructionSitePosition = position;
    }

    private void OnConstructionSiteReached()
    {
        var instance = towerToBuild.PrefabPath.Instantiate<Node3D>();
        instance.Position = constructionSitePosition;
        AddChild(instance);
    }
}
