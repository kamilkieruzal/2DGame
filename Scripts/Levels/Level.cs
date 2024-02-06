using Godot;

//Write 1 script for all levels and create resources to just modify TileMap of next levels
public partial class Level : Node2D
{
    [Export]
    public Timer timer;
    private AnimationPlayer animationPlayer;
    private AudioStreamPlayer backgroundMusicPlayer;
    private ColorRect textLabel;
    private Chest chest;
    private Door door;
    private Key key;
    private Player player;
    private bool finishedLevel;
    private LevelManager levelManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        timer = GetNode<Timer>("MainNode/Timer");
        animationPlayer = GetNode<AnimationPlayer>("MainNode/AnimationPlayer");
        backgroundMusicPlayer = GetNode<AudioStreamPlayer>("MainNode/Music");
        textLabel = GetNode<ColorRect>("MainNode/ColorRect");
        chest = GetNode<Chest>("MainNode/Chest");
        door = GetNode<Door>("MainNode/Door");
        key = GetNode<Key>("MainNode/Key");
        player = GetNode<Player>("MainNode/Player");
        levelManager = new LevelManager();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Exit"))
            GetTree().Quit();

        if (Input.IsActionJustPressed("RestartLevel"))
            GetTree().ReloadCurrentScene();
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
            LevelManager.CurrentLevel++;
            levelManager.SaveGame(LevelManager.CurrentLevel);
        }
    }

    private void OnTimerFinished()
    {
        animationPlayer.Play("OpenedDoor");
    }

    private void OnAnimationFinished(string animationName)
    {
        if (animationName == "OpenedDoor")
        {
            GetTree().Paused = false;
            GetTree().ChangeSceneToPacked(LevelManager.Levels[LevelManager.CurrentLevel].LevelScene);
        }
    }
}
