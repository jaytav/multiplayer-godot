using Godot;

public partial class Screen : Control
{
    protected ScreenController ScreenController;

    public override void _Ready()
    {
        ScreenController = GetNode<ScreenController>("/root/ScreenController");
    }
}
