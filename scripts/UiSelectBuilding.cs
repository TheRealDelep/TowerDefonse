using Godot;

public partial class UiSelectBuilding : Control
{
	[Export]
	private Container ContainerButton;

	[Export]
	private TowerConstructionChannel towerChannel;

	public override void _Ready()
	{
		
		foreach (var child in ContainerButton.GetChildren())
		{
			ContainerButton.RemoveChild(child);
			child.QueueFree();
		}
		foreach (var model in towerChannel.TowerModels)
		{
            Button button = new Button(){ Icon = model.Icon};
			button.SizeFlagsHorizontal = SizeFlags.ExpandFill;
			ContainerButton.AddChild(button);
			button.Pressed += () => towerChannel.FireConstructionSelected(model);
		}
	}
}
