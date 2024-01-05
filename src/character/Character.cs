using Godot;

public partial class Character : Node2D
{
    private int _moveSpeed = 500;

    public override void _EnterTree()
    {
        SetMultiplayerAuthority(int.Parse(Name));
    }

    public override void _Process(double delta)
    {
        if (!IsMultiplayerAuthority())
        {
            return;
        }

        Vector2 direction = new();

        if (Input.IsActionPressed("move_left"))
        {
            direction += Vector2.Left;
        }

        if (Input.IsActionPressed("move_right"))
        {
            direction += Vector2.Right;
        }

        if (Input.IsActionPressed("move_up"))
        {
            direction += Vector2.Up;
        }

        if (Input.IsActionPressed("move_down"))
        {
            direction += Vector2.Down;
        }

        GlobalPosition += direction * (float)delta * _moveSpeed;
    }
}
