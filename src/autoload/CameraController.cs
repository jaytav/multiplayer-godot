using Godot;

/** CameraController
    Handle camera, set Follow for the camera to follow this node*/
public partial class CameraController : Node
{
    public Node2D Follow;

    private Camera2D _camera;
    private float _moveSpeed = 100;

    public override void _Ready()
    {
        _camera = GetNode<Camera2D>("/root/Main/World/Camera2D");
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
