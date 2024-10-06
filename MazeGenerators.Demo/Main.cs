using Godot;
using System;
using System.Collections.Generic;
using MazeGenerators;

public class Main : Node2D
{
    private Random r = new Random(0);
    private Maze m = new Maze(31, 21);

    private Dictionary<string, Action<Main>> OnClick = new Dictionary<string, Action<Main>>{
        {"Reset",(main)=>{ main.m.Reset(); }},
        {"Random",(main)=>{ main.m.Randomize(); }},
        {"Life",(main)=>{ main.m.Life(); }},
        {"Smooth",(main)=>{ main.m.Smooth(); }},
        {"Rooms",(main)=>{ main.m.TryAddRoom(); }},
        {"GrowMaze",(main)=>{ main.m.GrowMaze(); }},
        {"GrowRandomMaze",(main)=>{ main.m.GrowRandomMaze(); }},
        {"Connectors",(main)=>{ main.m.GenerateConnectors(); }},
        {"RemoveDeadEnds",(main)=>{ main.m.RemoveDeadEnds(); }},
        {"BuildWalls",(main)=>{ main.m.BuildWalls(); }},
    };
    public override void _Ready()
    {
        var container = this.GetNode<Container>("./Container");
        foreach (var a in OnClick)
        {
            var b = new Button();
            b.Text = a.Key;
            b.Connect("pressed", this, nameof(OnClickPressed), new Godot.Collections.Array { a.Key });

            container.AddChild(b);
        }
    }

    private void OnClickPressed(string name)
    {
        OnClick[name](this);
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
                var cell = maze.GetTile(new MazeGenerators.Vector2(x, y));
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
