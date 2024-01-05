using Godot;

/** CameraController
    Handle camera, set Follow for the camera to follow this node*/
public partial class CameraController : Node
{
    public Node2D Follow;

    private Camera2D _camera = new();
    private float _moveSpeed = 100;

    public override void _Ready()
    {
        _camera.PositionSmoothingEnabled = true;
        _camera.PositionSmoothingSpeed = 10;

        GetNode("/root/Main/World").AddChild(_camera);
    }

    public override void _Process(double delta)
    {
        if (Follow == null)
        {
            return;
        }

        _camera.GlobalPosition = Follow.GlobalPosition;
    }
}
