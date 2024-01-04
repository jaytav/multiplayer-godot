using Godot;

public partial class Main : Node
{
    public override void _Ready()
    {
        GD.Print("Main: _Ready()");
        ConnectSignals();
    }

    private void ConnectSignals()
    {
        // inialist main menu signals with HostController and JoinController
        MainMenuScreen mainMenuScreen = GetNode<MainMenuScreen>("UI/MainMenuScreen");
        mainMenuScreen.HostButtonPressed += GetNode<HostController>("/root/HostController").OnMainMenuScreenHostButtonPressed;
        mainMenuScreen.JoinButtonPressed += GetNode<JoinController>("/root/JoinController").OnMainMenuScreenJoinButtonPressed;
    }
}
