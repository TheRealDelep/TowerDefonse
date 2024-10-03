using Godot;

public partial class TowerSelectionView : CanvasGroup
{
    [Export] private TowerConstructionChannel towerConstructionChannel;
    [Export] private TowerList towerList;

    public override void _Ready()
    {
        towerConstructionChannel.TowerRequested += OnConstructionRequested;
        towerConstructionChannel.TowerRequestCanceled += OnConstructionRequestCanceled;
    }

    public override void _ExitTree()
    {
        towerConstructionChannel.TowerRequested -= OnConstructionRequested;
        towerConstructionChannel.TowerRequestCanceled -= OnConstructionRequestCanceled;
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