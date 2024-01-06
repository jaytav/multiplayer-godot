using Godot;

public partial class Matchmaking : Node
{
    public Button _playButton;

    public override void _Ready()
    {
        _playButton = GetNode<Button>("UI/Container/PlayButton");
        _playButton.Disabled = true;

        ENetMultiplayerPeer client = new();
        client.CreateClient("127.0.0.1", 3000);
        Multiplayer.MultiplayerPeer = client;
        Multiplayer.ConnectedToServer += OnMultiplayerConnectedToServer;
    }

    private void OnPlayButtonPressed()
    {
        GD.Print("Play button pressed");
        _playButton.Hide();

        // start matchmaking here
    }

    private void OnMultiplayerConnectedToServer()
    {
        _playButton.Disabled = false;
    }
}
