using Godot;

public partial class ScreenController : Node
{
    private string _activeScreen;
    private string _initialScreen = "MainMenuScreen";

    public override async void _Ready()
    {
        await ToSignal(GetTree().Root, "ready");

        foreach (Screen screen in GetNode("/root/Main/UI").GetChildren())
        {
            screen.Hide();
        }

        ChangeScreen(_initialScreen);
    }

    public void ChangeScreen(string screenName)
    {
        if (_activeScreen != null)
        {
            GetNode<Screen>($"/root/Main/UI/{_activeScreen}").Hide();
        }

        _activeScreen = screenName;
        GetNode<Screen>($"/root/Main/UI/{screenName}").Show();
    }
}
