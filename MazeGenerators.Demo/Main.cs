using Godot;
using System;
using MazeGenerators;

public class Main : Node2D
{
    private Random r = new Random(0);
    private Maze m = new Maze(31,21);

    public override void _Ready()
    {
        this.GetNode<Button>("./Container/Base").Connect("pressed", this, nameof(BasePressed));
        this.GetNode<Button>("./Container/Tree").Connect("pressed", this, nameof(TreePressed));
        this.GetNode<Button>("./Container/Life").Connect("pressed", this, nameof(LifePressed));
        this.GetNode<Button>("./Container/Cave").Connect("pressed", this, nameof(CavePressed));
    }

    private void BasePressed()
    {
        m.Clear()
            .GenerateRooms((max) => r.Next(max))
            .GrowMaze((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .RemoveDeadEnds(DefaultDirections.CardinalDirs)
            .BuildWalls();
        Draw();
    }

    private void TreePressed()
    {
        m.Clear()
            .GrowMaze((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .BuildWalls();
        Draw();
    }

    private void LifePressed()
    {
        m.Clear()
            .Randomize((max) => r.Next(max), 50)
            .Life(10, Tile.EmptyTileId, Tile.MazeTileId)
            .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .BuildWalls();
        Draw();
    }

    private void CavePressed()
    {
        m.Clear()
            .Randomize((max) => r.Next(max), 50)
            .Smooth()
            .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs)
            .BuildWalls();
        Draw();
    }

    private void Draw()
    {
        var maze = m;
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
                    if (maze.Junctions.Contains(new MazeGenerators.Vector2(x, y)))
                    {
                        tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(5, 0));
                    }
                    else
                    // if (cell == Tile.RoomTileId)
                    // {
                    //     tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(14, 0));
                    // } else
                    {
                        tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(1, 0));
                    }
                }
                else if (cell == Tile.WallTileId)
                {
                    tileMap.SetCellv(new Godot.Vector2(x, y), 0, autotileCoord: new Godot.Vector2(0, 1));
                }
            }
        }

    }
}
