using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Day15
{
    class IntcodeArcadeGame
    {
        public List<(int, int, int)> Tiles;
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public int LastBallX { get; set; }
        public int LastPaddleX { get; set; }

        public char[][] Map;

        public IntcodeArcadeGame()
        {
            Tiles = new List<(int, int, int)>();
        }

        public void AddTile(int x, int y, int tileId)
        {
            Tiles.Add((x, y, tileId));

            if (x > MaxX) { MaxX = x; }
            if (y > MaxY) { MaxY = y; }
        }

        public (int, int) GetScreenSize()
        {
            return (MaxX, MaxY);
        }

        public void InitializeMap()
        {
            Map = new char[MaxY + 1][];
            for (int i = 0; i < Map.Length; i++)
            {
                Map[i] = new char[MaxX + 1];
            }

            foreach (var tile in Tiles)
            {
                Map[tile.Item2][tile.Item1] = RepresentTile(tile.Item3);
            }
        }

        private char RepresentTile(int tileId)
        {
            switch (tileId)
            {
                // Empty
                case 0:
                    return ' ';

                // Wall
                case 1:
                    return '#';

                // Block
                case 2:
                    return '.';

                // Horizontal Paddle
                case 3:
                    return '-';

                // Ball
                case 4:
                    return 'o';
            }

            throw new Exception(string.Format("Invalid TileId: {0}", tileId));
        }

        public void PrintGame()
        {
            Console.SetCursorPosition(0, 0);
            // Slow down the drawing so that it's slow enough for a human to see what's going on.
            Thread.Sleep(5);

            if (Map == null)
            {
                throw new Exception("Uninitialized game map.");
            }

            bool foundBall = false;
            bool foundPaddle = false;

            foreach (var row in Map)
            {
                string rowPrinter = "";
                foreach (var c in row)
                {
                    rowPrinter += c;
                    if (c == 'o')
                    {
                        foundBall = true;
                    }
                    else if (c == '-')
                    {
                        foundPaddle = true;
                    }
                }
                Console.WriteLine(rowPrinter);
            }

            // Ensure we never draw the screen at an inappropriate time.
            // (ie. when we are mid-game tick, and either the ball, or paddle do not exist in the map.)
            if (foundBall == false)
            {
                throw new Exception("Ball not found!");
            }

            if (foundPaddle == false)
            {
                throw new Exception("Paddle not found!");
            }

        }

        internal void UpdateTile(int x, int y, int tileId)
        {
            Map[y][x] = RepresentTile(tileId);

            if (tileId == 4)
            {
                LastBallX = x;
            }
            else if (tileId == 3)
            {
                LastPaddleX = x;
            }
        }
    }
}
