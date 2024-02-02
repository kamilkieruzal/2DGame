using Godot;

public partial class Player : CharacterBody2D
{
    private float gravity = 1500;
    private float runSpeed = 100;
    private float jumpSpeed = -300;
    private bool right;
    private bool left;
    private AnimationPlayer animationPlayer;
    private Sprite2D sprite;
    private AudioStreamPlayer walkAudio;
    private AudioStreamPlayer jumpAudio;

    public bool HasChestKey { get; private set; }
    public bool HasDoorKey { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite");
        walkAudio = GetNode<AudioStreamPlayer>("Walk");
        jumpAudio = GetNode<AudioStreamPlayer>("Jump");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("Idle");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (right)
        {
            sprite.FlipH = false;
            animationPlayer.Play("Walk");
        }
        else if (left)
        {
            sprite.FlipH = true;
            animationPlayer.Play("Walk");
        }
        else
        {
            animationPlayer.Play("Idle");
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        velocity.Y += gravity * (float)delta;

        Velocity = velocity;
        GetInput();

        MoveAndSlide();

        for (var i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);
            if (collision.GetCollider() is RigidBody2D rigidBody2D)
            {
                rigidBody2D.ApplyCentralImpulse(new Vector2(-collision.GetNormal().X * 100f, 0));
            }
        }
    }

    private void GetInput()
    {
        var velocity = Velocity;
        velocity.X = 0;

        right = Input.IsActionPressed("Right");
        left = Input.IsActionPressed("Left");
        var jump = Input.IsActionJustPressed("Jump");

        if (IsOnFloor() && jump)
        {
            velocity.Y = jumpSpeed;
            jumpAudio.Play(0.15f);
        }

        if ((right || left) && !walkAudio.Playing)
            walkAudio.Play();
        else if (!right && !left)
            walkAudio.Stop();

        if (right)
            velocity.X += runSpeed;
        if (left)
            velocity.X -= runSpeed;

        Velocity = velocity;
    }

    public void PickupChestKey()
    {
        HasChestKey = true;
    }

    public void PickupDoorKey()
    {
        HasDoorKey = true;
    }
}
