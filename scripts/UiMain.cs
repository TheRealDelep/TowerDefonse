using Godot;

public partial class UiMain : Control
{
	[Export] private UiSelectBuilding uiSelectBuilding;
	[Export] private Control gameOverScene;
	[Export] private Timer gameOverTimer;
	[Export] private TowerConstructionChannel towerConstructionChannel;
	[Export] private Level level;
	[Export] private Node main;
	
	public override void _Ready()
	{
		towerConstructionChannel.TowerRequested += ShowSelectedUI;
		towerConstructionChannel.TowerSelected += (_) => uiSelectBuilding.Visible = false;
		uiSelectBuilding.Visible = false;
		gameOverScene.Visible = false;
		gameOverTimer.Timeout += () => main.GetTree().ReloadCurrentScene();
		level.CastleDestroyed += () => ShowGameOver();
	}

	public void ShowSelectedUI(Vector3 worldPos, Vector2 screenPos)
	{
		uiSelectBuilding.Position = screenPos;
		uiSelectBuilding.Visible = true;
	}

	public void ShowGameOver()
	{
		gameOverScene.Visible = true;
		gameOverTimer.Start(5);
	}
}
