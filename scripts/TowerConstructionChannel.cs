using System;
using Godot;

[GlobalClass]
public partial class TowerConstructionChannel : Godot.Resource
{
    public event Action<Vector3, Vector2> ConstructionRequested;
    public event Action ConstructionRequestCanceled;
    public event Action<TowerModel, Vector3> ConstructionSelected;

    public TowerConstructionChannel() {}

    public void FireConstructionRequest(Vector3 worldPos, Vector2 screenPos) 
    {
        ConstructionRequested?.Invoke(worldPos, screenPos);
    }

    public void FireConstructionRequestCancel() 
    {

    }

    public void FireConstructionSelected(TowerModel model, Vector3 worldPos) 
    {

    }
}