using Godot;
using System;

public partial class UiMain : Control
{
	[Export] private UiSelectBuilding uiSelectBuilding;
	[Export] private TowerConstructionChannel towerConstructionChannel;
	
	public override void _Ready()
	{
		towerConstructionChannel.TowerRequested += ShowSelectedUI;
		towerConstructionChannel.TowerSelected += (_) => uiSelectBuilding.Visible = false;
		uiSelectBuilding.Visible = false;
	}

	public void ShowSelectedUI(Vector3 worldPos, Vector2 screenPos)
	{
		uiSelectBuilding.Position = screenPos;
		uiSelectBuilding.Visible = true;
	}
}
