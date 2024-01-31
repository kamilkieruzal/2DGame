using Godot;

public partial class Level1 : Node2D
{
    private Timer timer;
    private AnimationPlayer animationPlayer;
    private ColorRect textLabel;
    private Chest chest;
    private Doors doors;
    private Key key;
    private Player player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        timer = GetNode<Timer>("Timer");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        textLabel = GetNode<ColorRect>("ColorRect");
        chest = GetNode<Chest>("Chest");
        doors = GetNode<Doors>("Doors");
        key = GetNode<Key>("Key");
        player = GetNode<Player>("Player");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Exit"))
            GetTree().Quit();
    }

    private void OnBodyEnteredChestKey(Node2D body)
    {
        if (body is Player)
        {
            key.Pickup();
            player.PickupChestKey();
        }
    }

    private void OnBodyEnteredChest(Node2D body)
    {
        if (body is Player player && player.HasChestKey)
        {
            chest.Open();
            player.PickupDoorKey();
        }
    }

    private void OnBodyEnteredDoors(Node2D body)
    {
        if (body is Player player && player.HasDoorKey)
        {
            doors.Open();
            player.PickupDoorKey();
        }
    }
}
