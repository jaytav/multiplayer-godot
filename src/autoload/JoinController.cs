using Godot;

public partial class JoinController : Node
{
    private ENetMultiplayerPeer _peer = new();

    public void OnMainMenuScreenJoinButtonPressed()
    {
        GD.Print("JoinController: OnMainMenuScreenJoinButtonPressed()");

        _peer.CreateClient("127.0.0.1", 3000);
        Multiplayer.MultiplayerPeer = _peer;
        Multiplayer.PeerConnected += OnMultiplayerPeerConnected;
        Multiplayer.ServerDisconnected += OnMultiplayerServerDisconnected;
    }

    private void OnMultiplayerPeerConnected(long id)
    {
        GD.Print($"JoinController: OnMultiplayerPeerConnected(): Connected with id: {id}");
    }

    private void OnMultiplayerServerDisconnected()
    {
        // quit client window when server disconnects
        GetTree().Quit();
    }
}
