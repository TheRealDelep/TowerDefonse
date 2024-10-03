using Godot;
using System;

public partial class UIGameOver : Control
{
	[Export] private Timer gameOverTimer;
	[Export] private Node main;

	public override void _Ready()
	{
		gameOverTimer.Timeout += () => main.GetTree().ReloadCurrentScene();
	}

	public void ShowUI()
	{
		Visible = true;
		gameOverTimer.Start(5);
	}
}
