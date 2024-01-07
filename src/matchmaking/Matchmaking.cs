using Godot;
using Godot.Collections;

public partial class Matchmaking : Node
{
    [Signal]
    public delegate void MatchmakingStartedEventHandler(long id);

    [Signal]
    public delegate void BattleStartedEventHandler(bool isServer, int port);

    public override void _Ready()
    {
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
