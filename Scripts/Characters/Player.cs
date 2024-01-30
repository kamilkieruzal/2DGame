using Godot;

public partial class Player : CharacterBody2D
{
    private float _gravity = 1500;
    private float _runSpeed = 100;
    private float _jumpSpeed = -300;
    private bool right;
    private bool left;
    private AnimationPlayer AnimationPlayer;
    private Sprite2D Sprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Sprite = GetNode<Sprite2D>("Sprite");
        AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        AnimationPlayer.Play("Idle");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (right)
        {
            Sprite.FlipH = false;
            AnimationPlayer.Play("Walk");
        }
        else if (left)
        {
            Sprite.FlipH = true;
            AnimationPlayer.Play("Walk");
        }
        else
        {
            AnimationPlayer.Play("Idle");
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        velocity.Y += _gravity * (float)delta;

        Velocity = velocity;
        GetInput();

        MoveAndSlide();
    }

    private void GetInput()
    {
        var velocity = Velocity;
        velocity.X = 0;

        right = Input.IsActionPressed("Right");
        left = Input.IsActionPressed("Left");
        var jump = Input.IsActionPressed("Jump");

        if (IsOnFloor() && jump)
            velocity.Y = _jumpSpeed;
        if (right)
            velocity.X += _runSpeed;
        if (left)
            velocity.X -= _runSpeed;

        Velocity = velocity;
    }
}
