using Godot;

public partial class MatchmakingServerClientItem : Control
{
    public long PeerId;

    private Label _peerId;

    public override void _Ready()
    {
        if (PeerId == 0)
        {
            return;
        }

        _peerId = GetNode<Label>("PeerId");
        _peerId.Text = PeerId.ToString();
        Name = PeerId.ToString();
    }
}
