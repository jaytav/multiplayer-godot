using Godot;

public partial class MatchmakingClientCharacter : CharacterBody2D
{

    private float _speed = 300.0f;
    private float _jumpVelocity = -400.0f;
    private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private Camera2D _camera;

    public override void _EnterTree()
    {
        _camera = GetNode<Camera2D>("Camera2D");
        SetMultiplayerAuthority(int.Parse(Name));

        if (IsMultiplayerAuthority())
        {
            _camera.Enabled = true;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsMultiplayerAuthority())
        {
            return;
        }

        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity.Y += _gravity * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = _jumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * _speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
