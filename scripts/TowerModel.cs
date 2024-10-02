using Godot;

[GlobalClass]
public partial class TowerModel : Resource 
{
    [Export] public string Name;
    [Export] public int Cost;
    [Export] public float ConstructionTime;

    [Export] public PackedScene PrefabPath;
    [Export] public Texture2D Icon;
}