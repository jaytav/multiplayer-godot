using Godot;

public partial class ClientController : Node
{
    private ENetMultiplayerPeer _peer = new();

    public override void _Ready()
    {
        GetNode<MainMenuScreen>("/root/Main/UI/MainMenuScreen").JoinButtonPressed += OnMainMenuScreenJoinButtonPressed;
    }

    public void OnMainMenuScreenJoinButtonPressed()
    {
        GD.Print("ClientController: OnMainMenuScreenJoinButtonPressed()");

        _peer.CreateClient("127.0.0.1", 3000);
        Multiplayer.MultiplayerPeer = _peer;
        Multiplayer.ServerDisconnected += OnMultiplayerServerDisconnected;
    }

    private void OnMultiplayerServerDisconnected()
    {
        // quit client window when server disconnects
        GetTree().Quit();
    }
}
