using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Day3
{
    class GridManager
    {
        public Cell[,] Grid { get; set; }
        private int _centerX { get; set; }
        private int _centerY { get; set; }

        public GridManager(int xDimension, int yDimension, int centerX, int centerY)
        {
            Grid = new Cell[xDimension, yDimension];
            _centerX = centerX;
            _centerY = centerY;
        }

        public Point WalkUp(Point startPoint, int distance, int wireNumber)
        {
            int x = startPoint.X;
            int y;
            for (int i = 0; i < distance; i++)
            {
                y = startPoint.Y + i;
                if (Grid[x, y] == null)
                {
                    Grid[x, y] = new Cell();
                    Grid[x, y].Wires.Add(wireNumber);
                }
                else if (!Grid[x, y].Wires.Contains(wireNumber))
                {
                    Grid[x, y].Wires.Add(wireNumber);
                }
            }
            return new Point(startPoint.X, startPoint.Y + distance);
        }

        public Point WalkRight(Point startPoint, int distance, int wireNumber)
        {
            int x;
            int y = startPoint.Y;
            for (int i = 0; i < distance; i++)
            {
                x = startPoint.X + i;
                if (Grid[x, y] == null)
                {
                    Grid[x, y] = new Cell();
                    Grid[x, y].Wires.Add(wireNumber);
                }
                else if (!Grid[x, y].Wires.Contains(wireNumber))
                {
                    Grid[x, y].Wires.Add(wireNumber);
                }
            }
            return new Point(startPoint.X + distance, startPoint.Y);
        }

        public Point WalkDown(Point startPoint, int distance, int wireNumber)
        {
            int x = startPoint.X;
            int y;
            for (int i = 0; i < distance; i++)
            {
                y = startPoint.Y - i;
                if (Grid[x, y] == null)
                {
                    Grid[x, y] = new Cell();
                    Grid[x, y].Wires.Add(wireNumber);
                }
                else if (!Grid[x, y].Wires.Contains(wireNumber))
                {
                    Grid[x, y].Wires.Add(wireNumber);
                }
            }
            return new Point(startPoint.X, startPoint.Y - distance);
        }

        public Point WalkLeft(Point startPoint, int distance, int wireNumber)
        {
            int x;
            int y = startPoint.Y;
            for (int i = 0; i < distance; i++)
            {
                x = startPoint.X - i;
                if (Grid[x, y] == null)
                {
                    Grid[x, y] = new Cell();
                    Grid[x, y].Wires.Add(wireNumber);
                }
                else if (!Grid[x, y].Wires.Contains(wireNumber))
                {
                    Grid[x, y].Wires.Add(wireNumber);
                }
            }
            return new Point(startPoint.X - distance, startPoint.Y);
        }

        public override string ToString()
        {
            string ret = "";
            foreach (var a in Grid)
            {
                ret += a;
            }

            return ret;
        }

        //public string PrintGrid()
        //{
        //    int rowLength = Grid.GetLength(0);
        //    int colLength = Grid.GetLength(1);

        //    string ret = "";

        //    for (int i = 0; i < rowLength; i++)
        //    {
        //        Console.WriteLine("{0}/{1}", i, rowLength);
        //        for (int j = 0; j < colLength; j++)
        //        {
        //            if (Grid[i, j] == null)
        //            {
        //                ret += " ";
        //            }
        //            else
        //            {
        //                ret += string.Format("{0} ", Grid[i, j]);
        //            }
        //        }
        //        ret += Environment.NewLine + Environment.NewLine;
        //    }

        //    return ret;
        //}
    }
}
