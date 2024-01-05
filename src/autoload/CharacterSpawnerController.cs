using Godot;

public partial class CharacterSpawnerController : MultiplayerSpawner
{
    private PackedScene _character = GD.Load<PackedScene>("res://src/character/Character.tscn");

    public override void _Ready()
    {
        SpawnPath = "/root/Main/World/Characters";
        AddSpawnableScene("res://src/character/Character.tscn");
    }

    public void SpawnCharacter(long id)
    {
        Character character = _character.Instantiate<Character>();
        character.Name = id.ToString();

        GetNode(SpawnPath).AddChild(character);
    }

    public void DespawnCharacter(long id)
    {
        GetNode($"{SpawnPath}/{id}").QueueFree();
    }
}
