using Godot;

public partial class MainMenuScreen : Control
{
    private void OnHostButtonPressed()
    {
        GD.Print("MainMenuScreen: OnHostButtonPressed()");
    }

    private void OnJoinButtonPressed()
    {
        GD.Print("MainMenuScreen: OnJoinButtonPressed()");
    }
}
