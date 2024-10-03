using Godot;

public partial class UiMain : Control
{
	[Export] private UiSelectBuilding uiSelectBuilding;
	[Export] private UIGameOver gameOverScene;
	[Export] private TowerConstructionChannel towerConstructionChannel;
	[Export] private Level level;
	[Export] private Node main;
	[Export] private GoldManager gold;
	
	public override void _Ready()
	{
		towerConstructionChannel.TowerRequested += ShowSelectedUI;
		towerConstructionChannel.TowerSelected += (_) => uiSelectBuilding.Visible = false;
		uiSelectBuilding.Visible = false;
		gameOverScene.Visible = false;
		level.CastleDestroyed += () => gameOverScene.ShowUI();
	}

	public void ShowSelectedUI(Vector3 worldPos, Vector2 screenPos)
	{
		uiSelectBuilding.Position = screenPos;
		uiSelectBuilding.Visible = true;
	}
}
