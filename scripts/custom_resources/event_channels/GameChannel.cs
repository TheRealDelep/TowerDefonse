using Godot;

public partial class TowerConstructionChannel : Resource
{
    [Signal]
    public delegate void GameChannelEventHandler();

    public void FireGameChannel()
    {
        EmitSignal(SignalName.GameChannel);
    }
}