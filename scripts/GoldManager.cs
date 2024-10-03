using Godot;

public partial class GoldManager : Node 
{
    [Export] private float goldMiningIntervalSeconds;
    public int CurrentGold = 10;

    private float timer = 0;

    public override void _Process(double delta)
    {
        timer += (float)delta;
        if (timer >= goldMiningIntervalSeconds) 
        {
            timer = 0;
            CurrentGold += 1;
            GD.Print($"CurrentGold {CurrentGold}");
        }
    }
}