using Godot;
using System;
using MazeGenerators;
using MazeGenerators.Utils;

public class Main : Node2D
{
    private Fluent fluent;

    private GeneratorSettings settings = new GeneratorSettings
    {
        Width = 31,
        Height = 21,
        Random = new Random(0),
    };

    public override void _Ready()
    {
        this.GetNode<Button>("./Container/Base").Connect("pressed", this, nameof(BasePressed));
        this.GetNode<Button>("./Container/Tree").Connect("pressed", this, nameof(TreePressed));
        this.GetNode<Button>("./Container/Life").Connect("pressed", this, nameof(LifePressed));
    }

    private void BasePressed()
    {
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

    private void TreePressed()
    {
        fluent = Fluent
            .Build(settings)
            .GenerateField()
            .GrowMaze()
            .GenerateConnectors()
            .BuildWalls();
        Draw();
    }

    private void LifePressed()
    {
        fluent = Fluent
            .Build(settings)
            .GenerateField()
            .GrowMaze()
            .Life(10, settings.EmptyTileId, settings.MazeTileId)
            .GenerateConnectors()
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
