﻿namespace MazeGenerators.Utils
{
    public class DefaultDirections
    {
        public static readonly Vector2[] CardinalDirs = {
            new Vector2( 1, 0 ),
            new Vector2( 0, -1 ),
            new Vector2( -1, 0 ),
            new Vector2( 0, 1 ),
        };

        public static readonly Vector2[] CompassDirs = {
            new Vector2( 1, 0 ),
            new Vector2( 1, -1 ),
            new Vector2( 0, -1 ),
            new Vector2( -1, -1 ),
            new Vector2( -1, 0 ),
            new Vector2( -1, 1 ),
            new Vector2( 0, 1 ),
            new Vector2( 1, 1 ),
        };
    }
}