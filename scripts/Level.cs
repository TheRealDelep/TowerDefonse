using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public struct EnemyPath
{
	public PathFollow3D PathFollow3D;
	public Node3D Enemy;
}

public partial class Level : Node3D
{
	[Export]
	public PackedScene EnemyPrefab;
	[Export]
	public Path3D PathEnemies;
	public List<EnemyPath> EnemyPaths = new();
	[Export]
	public Timer Timer;
	private Random random = new Random();
	[Export]
	public  Area3D nexus;
	public float countEnemies = 0;
	[Signal]
	public delegate void CastleDestroyedEventHandler();

	public override void _Ready()
	{
		Timer.Timeout += SpawnEnemy;
		(GetNode("ground") as Node3D).Visible = false;
		nexus.BodyEntered += BodyEntered;
	}

	public void BodyEntered(Node3D body)
	{
		if(countEnemies >= 10) return;

		if(body is Enemy)
		{
			KillEnemy(body as Enemy);
			countEnemies += 1;
			if(countEnemies >= 10) EmitSignal(SignalName.CastleDestroyed);
		}
	}

	public void KillEnemy(Enemy enemy)
	{
		EnemyPath enemyPath = EnemyPaths.First(ep => ep.Enemy == enemy);
		EnemyPaths.Remove(enemyPath);
		enemyPath.Enemy.QueueFree();
		enemyPath.PathFollow3D.QueueFree();
	}

	public void SpawnEnemy()
	{
        Enemy instance = EnemyPrefab.Instantiate<Enemy>();
        PathFollow3D pathFollow = new PathFollow3D();
        
		EnemyPaths.Add(new EnemyPath(){Enemy = instance, PathFollow3D = pathFollow});
		PathEnemies.AddChild(pathFollow);
		pathFollow.ProgressRatio = 0.0f;
		instance.Visible = false;
		instance.EnemyShouldDestroy += (enemy) => KillEnemy(enemy);
		GetParent().AddChild(instance);
		Timer.Start(1 + random.NextDouble());
	}

	public override void _Process(double delta)
	{
		foreach (var item in EnemyPaths)
		{
			item.Enemy.Visible = true;
			item.PathFollow3D.ProgressRatio += (float)delta * .1f;
			item.Enemy.Position = item.PathFollow3D.Position;
		}
	}
}
