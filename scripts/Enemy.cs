using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	[Signal]
	public delegate void EnemyShouldDestroyEventHandler(Enemy enemy);

	public void DestroyEnemy()
	{
		EmitSignal(SignalName.EnemyShouldDestroy, this);
	}
}
