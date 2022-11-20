using Godot;
using System;
using MazeGenerators;
using MazeGenerators.Utils;

public class Main : Node2D
{
    private Fluent fluent;

    public override void _Ready()
    {
        var settings = new GeneratorSettings
        {
            Width = 71,
            Height = 41,
            Random = new Random(0),
        };

        fluent = Fluent
            .Build(settings)
            .GenerateField()
            .GenerateRooms()
            .GrowMaze()
            .GenerateConnectors()
            .RemoveDeadEnds()
            .BuildWalls();
        Draw();
    }

    private void Draw()
    {
        var tileMap = this.GetNode<TileMap>("./TileMap");
        for (var x = 0; x < fluent.settings.Width; x++)
        {
            for (var y = 0; y < fluent.settings.Height; y++)
            {
                var cell = fluent.result.Paths[x, y];
                if (cell == fluent.settings.EmptyTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(0, 0));
                }
                else if (cell == fluent.settings.MazeTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(1, 0));
                }
                else if (cell == fluent.settings.WallTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(0, 1));
                }
                else if (cell == fluent.settings.JunctionTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(5, 0));
                }
                else if (cell == fluent.settings.RoomTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(14, 0));
                }
            }
        }

    }
}
