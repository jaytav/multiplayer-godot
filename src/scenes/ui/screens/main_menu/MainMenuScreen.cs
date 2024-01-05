using Godot;

public partial class MainMenuScreen : Screen
{
    private void OnButtonPressed()
    {
        ScreenController.ChangeScreen("InGameScreen");
    }
}
