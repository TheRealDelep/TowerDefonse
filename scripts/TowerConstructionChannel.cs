using System;
using Godot;

[GlobalClass]
public partial class TowerConstructionChannel : Godot.Resource
{
    [Signal]
    public delegate void TowerRequestedEventHandler(Vector3 worldPos, Vector2 screenPos);

    [Signal]
    public delegate void TowerRequestCanceledEventHandler();

    [Signal]
	public delegate void TowerSelectedEventHandler(TowerModel towerModel);

    [Signal]
	public delegate void TowerSiteReachedEventHandler();
    
    [Export]
    public TowerModel[] TowerModels;

    public TowerConstructionChannel() {}

    public void FireConstructionSelectionRequest(Vector3 worldPos, Vector2 screenPos) 
    {
        EmitSignal(SignalName.TowerRequested, worldPos, screenPos);
    }

    public void FireConstructionRequestCancel() 
    {
        EmitSignal(SignalName.TowerRequestCanceled);
    }

    public void FireConstructionSelected(TowerModel model) 
    {
        EmitSignal(SignalName.TowerSelected, model);
    }

    public void FireConstructionSiteReached() 
    {
        EmitSignal(SignalName.TowerSiteReached);
    }
}