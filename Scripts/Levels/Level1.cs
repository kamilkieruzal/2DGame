using Godot;

//Write 1 script for all levels and create resources to just modify TileMap of next levels
public partial class Level1 : Node2D
{
    private Timer timer;
    private AnimationPlayer animationPlayer;
    private AudioStreamPlayer backgroundMusicPlayer;
    private ColorRect textLabel;
    private Chest chest;
    private Door door;
    private Key key;
    private Player player;
    private bool finishedLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        timer = GetNode<Timer>("Timer");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        backgroundMusicPlayer = GetNode<AudioStreamPlayer>("Music");
        textLabel = GetNode<ColorRect>("ColorRect");
        chest = GetNode<Chest>("Chest");
        door = GetNode<Door>("Door");
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
        if (body is Player player && !player.HasChestKey)
        {
            key.Pickup();
            player.PickupChestKey();
            animationPlayer.Play("PickupChestKey");
        }
    }

    private void OnBodyEnteredChest(Node2D body)
    {
        if (body is Player player && player.HasChestKey && !player.HasDoorKey)
        {
            chest.Open();
            player.PickupDoorKey();
            animationPlayer.Play("PickupDoorKey");
        }
    }

    private void OnBodyEnteredDoor(Node2D body)
    {
        if (body is Player player && player.HasDoorKey && !finishedLevel)
        {
            finishedLevel = true;
            GetTree().Paused = true;
            timer.Start();
            backgroundMusicPlayer.Stop();
            door.Open();
        }
    }

    private void OnTimerFinished()
    {
        animationPlayer.Play("OpenedDoor");
    }

    private void OnAnimationFinished(string animationName)
    {
        if (animationName == "OpenedDoor")
            GetTree().Paused = false;
    }
}
