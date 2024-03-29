using Godot;

public partial class Door : Area2D
{
    private AudioStreamPlayer audioStreamPlayer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void Open()
    {
        audioStreamPlayer.Play();
    }

    public void Close()
    {
        GetNode<Sprite2D>("Opened").Hide();
        GetNode<Sprite2D>("Closed").Show();
    }

    private void OnDoorOpened()
    {
        GetNode<Sprite2D>("Opened").Show();
        GetNode<Sprite2D>("Closed").Hide();
    }
}
