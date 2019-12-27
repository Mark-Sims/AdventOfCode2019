using System;
using System.Collections.Generic;
using System.Text;

namespace Day13
{
    class IntcodeArcadeGame
    {
        public List<(int, int, int)> Tiles;
        public int MaxX { get; set; }
        public int MaxY { get; set; }

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
    }
}
