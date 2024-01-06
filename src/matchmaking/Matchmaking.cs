using Godot;
using Godot.Collections;

public partial class Matchmaking : Node
{
    [Signal]
    public delegate void MatchmakingStartedEventHandler(long id);

    [Signal]
    public delegate void BattleStartedEventHandler(bool isServer, int port);

    private PackedScene _client = GD.Load<PackedScene>("res://src/matchmaking/MatchmakingClient.tscn");
    private PackedScene _server = GD.Load<PackedScene>("res://src/matchmaking/MatchmakingServer.tscn");

    public override void _Ready()
    {
        // is server
        if (System.Array.Exists(OS.GetCmdlineArgs(), arg => arg == "--s"))
        {
            AddChild(_server.Instantiate());
        }
        else
        {
            AddChild(_client.Instantiate());
        }

        RpcConfig("StartMatchmaking", new Dictionary() {{"rpc_mode", 1}});
        RpcConfig("StartBattle", new Dictionary() {{"rpc_mode", 1}});
    }

    private void StartMatchmaking(long id)
    {
        EmitSignal(nameof(MatchmakingStarted), id);
    }

    private void StartBattle(bool isServer, int port)
    {
        EmitSignal(nameof(BattleStarted), isServer, port);
    }
}
