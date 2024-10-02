using System;
using Godot;

[GlobalClass]
public partial class TowerConstructionChannel : Godot.Resource
{
    public event Action<Vector3, Vector2> ConstructionRequested;
    public event Action ConstructionRequestCanceled;
    public event Action<TowerModel, Vector3> ConstructionSelected;
    public event Action ConstructionSiteReached;

    public TowerConstructionChannel() {}

    public void FireConstructionSelectionRequest(Vector3 worldPos, Vector2 screenPos) 
    {
        ConstructionRequested?.Invoke(worldPos, screenPos);
    }

    public void FireConstructionRequestCancel() 
    {
        ConstructionRequestCanceled?.Invoke();
    }

    public void FireConstructionSelected(TowerModel model, Vector3 worldPos) 
    {
        ConstructionSelected?.Invoke(model, worldPos);
    }

    public void FireConstructionSiteReached() 
    {
        ConstructionSiteReached?.Invoke();
    }
}