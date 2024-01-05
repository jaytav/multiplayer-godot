using Godot;

public partial class ClientController : Node
{
    public override void _Ready()
    {
        GetNode<Button>("/root/Main/UI/MainMenuScreen/Container/JoinButton").Pressed += OnMainMenuScreenJoinButtonPressed;
    }

    public void OnMainMenuScreenJoinButtonPressed()
    {
        GD.Print("ClientController: OnMainMenuScreenJoinButtonPressed()");
        ENetMultiplayerPeer peer = new();
        peer.CreateClient("127.0.0.1", 3000);

        Multiplayer.MultiplayerPeer = peer;
        Multiplayer.ServerDisconnected += OnMultiplayerServerDisconnected;
    }

    private void OnMultiplayerServerDisconnected()
    {
        // quit client window when server disconnects
        GetTree().Quit();
    }
}
