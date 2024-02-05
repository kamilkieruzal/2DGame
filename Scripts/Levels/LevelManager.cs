using Godot;
using System.Collections.Generic;

public static class LevelManager
{
    public static IList<LevelInfo> Levels = new List<LevelInfo>
    {
        new LevelInfo
        {
            LevelNumber = 1,
            LevelScene = GD.Load<PackedScene>("res://Scenes/Levels/Level1.tscn")
        },
        new LevelInfo
        {
            LevelNumber = 2,
            LevelScene = GD.Load<PackedScene>("res://Scenes/Levels/Level2.tscn")
        },
    };
}
