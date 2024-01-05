using Godot;

public partial class ServerController : Node
{
    private PackedScene _character = GD.Load<PackedScene>("res://src/character/Character.tscn");

    public override void _Ready()
    {
        GetNode<Button>("/root/Main/UI/MainMenuScreen/Container/HostButton").Pressed += OnMainMenuScreenHostButtonPressed;
    }

    private void SpawnCharacter(long id)
    {
        Character character = _character.Instantiate<Character>();
        character.Name = id.ToString();
        GetNode("/root/Main/World/Characters").AddChild(character);
    }

    private void DespawnCharacter(long id)
    {
        GetNode($"/root/Main/World/Characters/{id}").QueueFree();
    }

    private void OnMainMenuScreenHostButtonPressed()
    {
        GD.Print("ServerController: OnMainMenuScreenHostButtonPressed()");
        ENetMultiplayerPeer peer = new();
        peer.CreateServer(3000, 3);

        Multiplayer.MultiplayerPeer = peer;
        Multiplayer.PeerConnected += OnMultiplayerPeerConnected;
        Multiplayer.PeerDisconnected += OnMultiplayerPeerDisconnected;

        // spawn host character
        SpawnCharacter(Multiplayer.GetUniqueId());
    }

    private void OnMultiplayerPeerConnected(long id)
    {
        GD.Print($"ServerController: OnMultiplayerPeerConnected(): Connected with id: {id}");

        // spawn client character
        SpawnCharacter(id);
    }

    private void OnMultiplayerPeerDisconnected(long id)
    {
        DespawnCharacter(id);
    }
}
