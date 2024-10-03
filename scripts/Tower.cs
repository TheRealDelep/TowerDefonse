using Godot;

public partial class Tower : Node3D 
{
    [Export] private float radius;
    [Export] private float projectileSpeed;
    [Export] private float fireRateInSeconds;

    [Export(PropertyHint.File)] 
    private string bulletPath;

    [Export] private Area3D detectionArea;

    private static Pool<TowerBullet> bulletPool;

    private float timeSinceLastFire;
    [Export] private Slider slider;

    public override void _Ready()
    {
        var parent = GetParent();
        bulletPool ??= new Pool<TowerBullet>(bulletPath, parent, 100);
    }

    public override void _Process(double delta)
    {
        timeSinceLastFire += (float)delta;
        slider.Value = timeSinceLastFire;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (timeSinceLastFire < fireRateInSeconds) 
        {
            return;
        }

        var bodies = detectionArea.GetOverlappingBodies();
        foreach (var body in bodies)
        {
            if (body is Enemy enemy)
            {
                GD.Print("Found an enemy");
                timeSinceLastFire = 0;
                var bullet = bulletPool.GetOne();
                bullet.Position = Position;
                bullet.target = enemy;
                bullet.Speed = projectileSpeed;
                break; 
            }
        }
    }
}