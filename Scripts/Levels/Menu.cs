using Godot;
using System.Linq;

public partial class Menu : Control
{
    LevelManager levelManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelManager = new LevelManager();
        levelManager.LoadGame();
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }

    private void OnExit()
    {
        GetTree().Quit();
    }

    private void OnStart()
    {
        LevelManager.CurrentLevel = 0;
        GetTree().ChangeSceneToPacked(LevelManager.Levels.First().LevelScene);
    }

    private void OnContinue()
    {
        GetTree().ChangeSceneToPacked(LevelManager.Levels[LevelManager.CurrentLevel].LevelScene);
    }
}
