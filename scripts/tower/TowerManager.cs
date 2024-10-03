using Godot;

public partial class TowerManager : Node3D
{
    [Export] private TowerConstructionChannel channel;
    [Export] private GoldManager goldManager;

    public ConstructionRequest CurrentConstructionRequest;
    
    public override void _Ready()
    {
        channel.TowerRequested += OnConstructionRequested;
        channel.TowerRequestCanceled += OnConstructionCanceled;
        channel.TowerSelected += OnConstructionSelected;
        channel.TowerSiteReached += OnConstructionSiteReached;
    }

    private void OnConstructionRequested(Vector3 worldPos, Vector2 _)
    {
        CurrentConstructionRequest = new()
        {
            State = ConstructionEnumState.WaitingForModel,
            Model = null,
            Position = worldPos
        };

        GD.Print($"Construction Requested at {worldPos}");
    }

    private void OnConstructionCanceled()
    {
        CurrentConstructionRequest = new()
        {
            State = ConstructionEnumState.None,
            Model = null,
            Position = Vector3.Zero
        };

        GD.Print("Construction Canceled");
    }

    private void OnConstructionSelected(TowerModel model) 
    {
        if (model.Cost > goldManager.CurrentGold)
        {
            return;
        }

        goldManager.CurrentGold -= model.Cost;
        CurrentConstructionRequest.Model = model;
        CurrentConstructionRequest.State = ConstructionEnumState.Building;
        GD.Print($"Construction Selected: model: {CurrentConstructionRequest.Model}, position {CurrentConstructionRequest.Position}, State: {CurrentConstructionRequest.State}");
    }

    private void OnConstructionSiteReached()
    {
        var instance = CurrentConstructionRequest.Model.PrefabPath.Instantiate<Node3D>();
        instance.Position = CurrentConstructionRequest.Position;
        AddChild(instance);

        CurrentConstructionRequest = new() 
        {
            State = ConstructionEnumState.None,
            Model = null,
            Position = Vector3.Zero
        };

        GD.Print("Construction Site Reached");
    }
}

public struct ConstructionRequest 
{
    public ConstructionEnumState State;
    public TowerModel Model;
    public Vector3 Position;
}

public enum ConstructionEnumState 
{
    None,
    WaitingForModel,
    Building
}