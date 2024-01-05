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
    }

    private void OnMultiplayerPeerConnected(long id)
    {
        GD.Print($"JoinController: OnMultiplayerPeerConnected(): Connected with id: {id}");
    }
}
