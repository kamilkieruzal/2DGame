using Godot;
using System.Collections.Generic;

public partial class LevelManager : Node2D
{
    public static int CurrentLevel = 0;

    public static IList<LevelInfo> Levels = new List<LevelInfo>
    {
        new LevelInfo
        {
            LevelNumber = 0,
            LevelScene = GD.Load<PackedScene>("res://Scenes/Levels/Level1.tscn")
        },
        new LevelInfo
        {
            LevelNumber = 1,
            LevelScene = GD.Load<PackedScene>("res://Scenes/Levels/Level2.tscn")
        },
        new LevelInfo
        {
            LevelNumber = 2,
            LevelScene = GD.Load<PackedScene>("res://Scenes/Levels/Level3.tscn")
        },
    };

    public void SaveGame(int lastLevel)
    {
        using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);

        saveGame.StoreLine("{ \"lastLevel\": " + lastLevel + "}");
    }

    public void LoadGame()
    {
        if (!FileAccess.FileExists("user://savegame.save"))
        {
            return; // Error! We don't have a save to load.
        }

        using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);
        var jsonString = saveGame.GetLine();

        var json = new Json();
        var parseResult = json.Parse(jsonString);
        if (parseResult != Error.Ok)
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");

        // Get the data from the JSON object
        var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);

        CurrentLevel = (int)nodeData["lastLevel"];
    }
}
