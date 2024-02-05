using Godot;
using System.Linq;

public partial class Menu : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void OnExit()
    {
        GetTree().Quit();
    }

    private void OnStart()
    {
        GetTree().ChangeSceneToPacked(LevelManager.Levels.First().LevelScene);
    }

    private void OnContinue()
    {
        var lastLevel = 2;
        GetTree().ChangeSceneToPacked(LevelManager.Levels[lastLevel - 1].LevelScene);
    }
}
