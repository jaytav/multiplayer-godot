using Godot;

public partial class HostController : Node
{
    private ENetMultiplayerPeer _peer = new();
    private CharacterSpawnerController characterSpawnerController;

    public override void _Ready()
    {
        characterSpawnerController = GetNode<CharacterSpawnerController>("/root/CharacterSpawnerController");
    }

    public void OnMainMenuScreenHostButtonPressed()
    {
        GD.Print("HostController: OnMainMenuScreenHostButtonPressed()");

        _peer.CreateServer(3000);
        Multiplayer.MultiplayerPeer = _peer;
        Multiplayer.PeerConnected += OnMultiplayerPeerConnected;
        Multiplayer.PeerDisconnected += OnMultiplayerPeerDisconnected;

        // spawn host character
        characterSpawnerController.SpawnCharacter(Multiplayer.GetUniqueId());
    }

    private void OnMultiplayerPeerConnected(long id)
    {
        GD.Print($"HostController: OnMultiplayerPeerConnected(): Connected with id: {id}");
        characterSpawnerController.SpawnCharacter(id);
    }

    private void OnMultiplayerPeerDisconnected(long id)
    {
        characterSpawnerController.DespawnCharacter(id);
    }
}
