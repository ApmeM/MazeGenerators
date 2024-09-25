using Godot;
using System;
using MazeGenerators;

public class Main : Node2D
{
    private Random r = new Random(0);

    public override void _Ready()
    {
        this.GetNode<Button>("./Container/Base").Connect("pressed", this, nameof(BasePressed));
        this.GetNode<Button>("./Container/Tree").Connect("pressed", this, nameof(TreePressed));
        this.GetNode<Button>("./Container/Life").Connect("pressed", this, nameof(LifePressed));
    }

    private void BasePressed()
    {
        Draw(new Maze(31, 21)
            .GenerateRooms((max) => r.Next(max))
            .GrowMaze((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .RemoveDeadEnds(DefaultDirections.CardinalDirs)
            .BuildWalls()
        );
    }

    private void TreePressed()
    {
        Draw(new Maze(31, 21)
            .GrowMaze((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .BuildWalls()
        );
    }

    private void LifePressed()
    {
        Draw(new Maze(31, 21)
            .GrowMaze((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .Life(10, Tile.EmptyTileId, Tile.MazeTileId)
            .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .BuildWalls()
        );
    }

    private void Draw(Maze maze)
    {
        var tileMap = this.GetNode<TileMap>("./TileMap");
        for (var x = 0; x < maze.Width; x++)
        {
            for (var y = 0; y < maze.Height; y++)
            {
                var cell = maze.Paths[x, y];
                if (cell == Tile.EmptyTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(0, 0));
                }
                else if (cell == Tile.MazeTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(1, 0));
                }
                else if (cell == Tile.WallTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(0, 1));
                }
                else if (cell == Tile.JunctionTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(5, 0));
                }
                else if (cell == Tile.RoomTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(14, 0));
                }
            }
        }

    }
}
