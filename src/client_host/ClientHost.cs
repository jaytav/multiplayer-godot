using Godot;

public partial class ClientHost : Node
{
    private LineEdit _ipAddress;
    private LineEdit _port;

    public override void _Ready()
    {
        _ipAddress = GetNode<LineEdit>("UI/Container/IPAddress");
        _port = GetNode<LineEdit>("UI/Container/Port");
    }

    private void OnHostButtonPressed()
    {
        GD.Print("Host button pressed");
        ENetMultiplayerPeer host = new();
        host.CreateServer(_port.Text.ToInt());
        Multiplayer.MultiplayerPeer = host;
        Multiplayer.PeerConnected += OnMultiplayerPeerConnected;
    }

    private void OnJoinButtonPressed()
    {
        GD.Print("Join button pressed");
        ENetMultiplayerPeer client = new();
        client.CreateClient(_ipAddress.Text, _port.Text.ToInt());
        Multiplayer.MultiplayerPeer = client;
    }

    private void OnMultiplayerPeerConnected(long id)
    {
        GD.Print($"Multiplayer peer connected with id: {id}");
    }
}
