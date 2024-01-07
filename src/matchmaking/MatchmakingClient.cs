using Godot;

public partial class MatchmakingClient : Matchmaking
{
    private Button _playButton;
    private Label _centerLabel;

    private ENetMultiplayerPeer _matchmakingClient = new();
    private ENetMultiplayerPeer _battleClient = new();

    private PackedScene _character = GD.Load<PackedScene>("res://src/matchmaking/MatchmakingClientCharacter.tscn");

    public override void _Ready()
    {
        base._Ready();
        _playButton = GetNode<Button>("UI/Container/PlayButton");
        _playButton.Disabled = true;
        _centerLabel = GetNode<Label>("UI/Container/CenterLabel");

        ENetMultiplayerPeer _matchmakingClient = new();
        _matchmakingClient.CreateClient("127.0.0.1", 3000);
        Multiplayer.MultiplayerPeer = _matchmakingClient;
        Multiplayer.ConnectedToServer += OnMultiplayerConnectedToServer;

        BattleStarted += OnBattleStarted;
    }

    private void SpawnCharacter(long id)
    {
        MatchmakingClientCharacter character = _character.Instantiate<MatchmakingClientCharacter>();
        character.Name = id.ToString();
        GetNode("World/Characters").AddChild(character);
    }

    private void OnPlayButtonPressed()
    {
        _playButton.Hide();
        _centerLabel.Text = "Searching...";

        // call Matchmaking.StartMatchmaking function on server with this clients peer id
        Rpc("StartMatchmaking", Multiplayer.GetUniqueId());
    }

    private void OnMultiplayerConnectedToServer()
    {
        _playButton.Disabled = false;
    }

    private void OnBattleStarted(bool isServer, int port)
    {
        _centerLabel.Text = "Match found!!!";

        if (isServer)
        {
            _battleClient.CreateServer(port);
            Multiplayer.MultiplayerPeer = _battleClient;
            Multiplayer.PeerConnected += OnMultiplayerPeerConnected;
            SpawnCharacter(Multiplayer.GetUniqueId());
        }
        else
        {
            _battleClient.CreateClient("127.0.0.1", port);
            Multiplayer.MultiplayerPeer = _battleClient;
        }

        _centerLabel.Hide();
    }

    private void OnMultiplayerPeerConnected(long id)
    {
        SpawnCharacter(id);
    }
}
