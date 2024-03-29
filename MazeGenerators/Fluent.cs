﻿using System;
using MazeGenerators.Utils;

namespace MazeGenerators
{
    public class Fluent
    {
        public GeneratorResult result { get; private set; }
        public GeneratorSettings settings { get; private set; }
        private Fluent()
        {
        }

        public static Fluent Build(GeneratorSettings settings)
        {
            return new Fluent
            {
                result = new GeneratorResult(),
                settings = settings
            };
        }

        public Fluent RemoveDeadEnds()
        {
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            return this;
        }

        public Fluent GenerateField()
        {
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            return this;
        }

        public Fluent Mirror(MirroringAlgorithm.MirrorDirection mirror)
        {
            MirroringAlgorithm.Mirror(result, settings, mirror);
            return this;
        }

        public Fluent GenerateConnectors(int additionalPassagesTries = 10)
        {
            RegionConnectorAlgorithm.GenerateConnectors(result, settings, additionalPassagesTries);
            return this;
        }

        public Fluent GenerateRooms(int numRoomTries = 100, int targetRoomCount = 100, bool preventOverlappedRooms = true, int minRoomSize = 2, int maxRoomSize = 5, int maxWidthHeightRoomSizeDifference = 5)
        {
            RoomGeneratorAlgorithm.GenerateRooms(result, settings, numRoomTries, targetRoomCount, preventOverlappedRooms, minRoomSize, maxRoomSize, maxWidthHeightRoomSizeDifference);
            return this;
        }

        public Fluent Parse(string mazeText)
        {
            StringParserAlgorithm.Parse(result, settings, mazeText);
            return this;
        }

        public string Stringify()
        {
            return StringParserAlgorithm.Stringify(result, settings);
        }

        public Fluent GrowMaze(int windingPercent = 50)
        {
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings, windingPercent);
            return this;
        }

        public Fluent BuildWalls()
        {
            WallSurroundingAlgorithm.BuildWalls(result, settings);
            return this;
        }

        public Fluent AddFillRectangle(Rectangle room, int tileId)
        {
            CustomDrawAlgorithm.AddFillRectangle(result, settings, room, tileId);
            return this;
        }

        public Fluent AddRectangle(Rectangle room, int tileId)
        {
            CustomDrawAlgorithm.AddRectangle(result, settings, room, tileId);
            return this;
        }

        public Fluent AddPoint(Vector2 point, int tileId)
        {
            CustomDrawAlgorithm.AddPoint(result, settings, point, tileId);
            return this;
        }

        public Fluent Life(int iterations, int liveTileId, int emptyTileId, Func<int, bool> birthCondition = null, Func<int, bool> deathCondition = null)
        {
            LifeGameAlgorithm.Life(result, settings, iterations, liveTileId, emptyTileId, birthCondition, deathCondition);
            return this;
        }
    }
}
