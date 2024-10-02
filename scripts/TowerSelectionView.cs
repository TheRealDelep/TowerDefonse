using Godot;

public partial class TowerSelectionView : CanvasGroup
{
    [Export] private TowerConstructionChannel towerConstructionChannel;
    [Export] private TowerList towerList;

    public override void _Ready()
    {
        towerConstructionChannel.ConstructionRequested += OnConstructionRequested;
        towerConstructionChannel.ConstructionRequestCanceled += OnConstructionRequestCanceled;
    }

    public override void _ExitTree()
    {
        towerConstructionChannel.ConstructionRequested -= OnConstructionRequested;
        towerConstructionChannel.ConstructionRequestCanceled -= OnConstructionRequestCanceled;
    }

    private void OnConstructionRequested(Vector3 worldPos, Vector2 screenPos) 
    {
        Position = screenPos;
        Visible = true; 
    }

    private void OnConstructionRequestCanceled() 
    {
        Visible = false;
    }
}