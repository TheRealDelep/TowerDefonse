using Godot;
using System;

public partial class UIHudGame : Control
{
	[Export] private Label hudCoins;
	[Export] private Label hudHitPoint;
	[Export] private GoldManager gold;
	[Export] private Level level;

	public override void _Process(double delta)
	{
		hudCoins.Text = (gold.CurrentGold > 1 ? "Sesterces " : "Sesterce ") + gold.CurrentGold;
		hudHitPoint.Text = "Hewlett Packard : " + (10 - level.countEnemies);
	}
}
