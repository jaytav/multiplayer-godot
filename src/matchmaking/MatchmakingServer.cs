using Godot;
using Godot.Collections;

public partial class MatchmakingServer : Node
{
    private PackedScene _clientItem = GD.Load<PackedScene>("res://src/matchmaking/MatchmakingServerClientItem.tscn");
    private VBoxContainer _clientItems;
    private Array<long> _peersMatchmaking = new();
    private int _currentPort = 3000;

    public override void _Ready()
    {
        _clientItems = GetNode<VBoxContainer>("UI/Container/Container/Container/ClientItems");

        GD.Print("Running matchmaking server...");
        ENetMultiplayerPeer server = new();
        server.CreateServer(_currentPort);
        Multiplayer.MultiplayerPeer = server;
        Multiplayer.PeerConnected += OnMultiplayerPeerConnected;
        Multiplayer.PeerDisconnected += OnMultiplayerPeerDisconnected;

        // clear any existing client items
        foreach (Node clientItem in _clientItems.GetChildren())
        {
            clientItem.QueueFree();
        }

        Matchmaking matchmaking = GetParent<Matchmaking>();
        matchmaking.MatchmakingStarted += OnMatchmakingStarted;
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

    private void OnMatchmakingStarted(long id)
    {
        GD.Print($"Multiplayer peer started matchmaking with id: {id}");
        _peersMatchmaking.Add(id);

        // set status to yellow (matchmaking)
        string clientItemPath = $"UI/Container/Container/Container/ClientItems/";
        GetNode<TextureRect>($"{clientItemPath}/{id}/Status").Modulate = Colors.Yellow;

        // at least 2 peers are matchmaking, start battle
        if (_peersMatchmaking.Count > 1)
        {
            // add to port to avoid conflict with multiple matchmaking games
            _currentPort += 1;

            // start first as server
            long peerId1 = _peersMatchmaking[0];
            GetNode<TextureRect>($"{clientItemPath}/{peerId1}/Status").Modulate = Colors.Green;
            GetParent().RpcId(peerId1, "StartBattle", true, _currentPort);

            // start other as client
            long peerId2 = _peersMatchmaking[1];
            GetNode<TextureRect>($"{clientItemPath}/{peerId2}/Status").Modulate = Colors.Green;
            GetParent().RpcId(peerId2, "StartBattle", false, _currentPort);

            _peersMatchmaking.Remove(peerId1);
            _peersMatchmaking.Remove(peerId2);
        }
    }
}
