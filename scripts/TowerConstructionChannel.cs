using System;
using Godot;

[GlobalClass]
public partial class TowerConstructionChannel : Godot.Resource
{
    [Signal]
    public delegate void ConstructionRequestedEventHandler(Vector3 worldPos, Vector2 screenPos);

    [Signal]
    public delegate void ConstructionRequestCanceledEventHandler();

    [Signal]
	public delegate void BuildingSelectedEventHandler(TowerModel towerModel);
    
    [Export]
    public TowerModel[] TowerModels;

    public TowerConstructionChannel() {}

    public void FireConstructionSelectionRequest(Vector3 worldPos, Vector2 screenPos) 
    {
        EmitSignal(SignalName.ConstructionRequested, worldPos, screenPos);
    }

    public void FireConstructionRequestCancel() 
    {
        EmitSignal(SignalName.ConstructionRequested);
    }

    public void FireConstructionSelected(TowerModel model) 
    {
        EmitSignal(SignalName.BuildingSelected, model);
    }
}