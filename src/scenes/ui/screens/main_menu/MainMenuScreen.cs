using Godot;

public partial class MainMenuScreen : Screen
{
    [Signal]
    public delegate void HostButtonPressedEventHandler();

    [Signal]
    public delegate void JoinButtonPressedEventHandler();

    private void OnHostButtonPressed()
    {
        EmitSignal(nameof(HostButtonPressed));
    }

    private void OnJoinButtonPressed()
    {
        EmitSignal(nameof(JoinButtonPressed));
    }

    private void OnButtonPressed()
    {
        ScreenController.ChangeScreen("InGameScreen");
    }
}
