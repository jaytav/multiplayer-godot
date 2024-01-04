using Godot;

public partial class HostController : Node
{
    private ENetMultiplayerPeer _peer = new();

    public void OnMainMenuScreenHostButtonPressed()
    {
        GD.Print("HostController: HandleMainMenuHostButtonPressed()");

        _peer.CreateServer(3000);
        Multiplayer.MultiplayerPeer = _peer;
        Multiplayer.PeerConnected += OnMultiplayerPeerConnected;
    }

    private void OnMultiplayerPeerConnected(long id)
    {
        GD.Print($"HostController: OnMultiplayerPeerConnected(): Connected with id: {id}");
    }
}
