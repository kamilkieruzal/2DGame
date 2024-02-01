using Godot;

public partial class Key : Area2D
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

    public void Pickup()
    {
        audioStreamPlayer.Play();
        Hide();
    }

    private void FinishedPlayingAudio()
    {
        QueueFree();
    }
}
