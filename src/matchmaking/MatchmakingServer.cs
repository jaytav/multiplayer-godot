using Godot;

public partial class MatchmakingServer : Node
{
    private PackedScene _clientItem = GD.Load<PackedScene>("res://src/matchmaking/MatchmakingServerClientItem.tscn");
    private VBoxContainer _clientItems;

    public override void _Ready()
    {
        _clientItems = GetNode<VBoxContainer>("UI/Container/Container/Container/ClientItems");

        GD.Print("Running matchmaking server...");
        ENetMultiplayerPeer server = new();
        server.CreateServer(3000);
        Multiplayer.MultiplayerPeer = server;
        Multiplayer.PeerConnected += OnMultiplayerPeerConnected;
        Multiplayer.PeerDisconnected += OnMultiplayerPeerDisconnected;

        // clear any existing client items
        foreach (Node clientItem in _clientItems.GetChildren())
        {
            clientItem.QueueFree();
        }
    }

    private void OnMultiplayerPeerConnected(long id)
    {
        GD.Print($"Multiplayer peer connected with id: {id}");
        MatchmakingServerClientItem clientItem = _clientItem.Instantiate<MatchmakingServerClientItem>();
        clientItem.PeerId = id;
        _clientItems.AddChild(clientItem);
    }

    private void OnMultiplayerPeerDisconnected(long id)
    {
        GD.Print($"Multiplayer peer disconnected with id: {id}");
        _clientItems.GetNode(id.ToString()).QueueFree();
    }
}
