using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Day3
{
    class GridManager
    {
        public Dictionary<Point, HashSet<int>> Cells { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }

        public GridManager(int xDimension, int yDimension, int centerX, int centerY)
        {
            CenterX = centerX;
            CenterY = centerY;
            Cells = new Dictionary<Point, HashSet<int>>();
        }

        public Point WalkUp(Point startPoint, int distance, int wireNumber)
        {
            Point p = new Point();
            p.X = startPoint.X;
            for (int i = 0; i < distance; i++)
            {
                p.Y = startPoint.Y + i;
                if (!Cells.ContainsKey(p))
                {
                    Cells[p] = new HashSet<int> { wireNumber };
                }
                else
                {
                    Cells[p].Add(wireNumber);
                }
            }
            return new Point(startPoint.X, startPoint.Y + distance);
        }

        public Point WalkRight(Point startPoint, int distance, int wireNumber)
        {
            Point p = new Point();
            p.Y = startPoint.Y;
            for (int i = 0; i < distance; i++)
            {
                p.X = startPoint.X + i;
                if (!Cells.ContainsKey(p))
                {
                    Cells[p] = new HashSet<int> { wireNumber };
                }
                else
                {
                    Cells[p].Add(wireNumber);
                }
            }
            return new Point(startPoint.X + distance, startPoint.Y);
        }

        public Point WalkDown(Point startPoint, int distance, int wireNumber)
        {
            Point p = new Point();
            p.X = startPoint.X;
            for (int i = 0; i < distance; i++)
            {
                p.Y = startPoint.Y - i;
                if (!Cells.ContainsKey(p))
                {
                    Cells[p] = new HashSet<int> { wireNumber };
                }
                else
                {
                    Cells[p].Add(wireNumber);
                }
            }
            return new Point(startPoint.X, startPoint.Y - distance);
        }

        public Point WalkLeft(Point startPoint, int distance, int wireNumber)
        {
            Point p = new Point();
            p.Y = startPoint.Y;
            for (int i = 0; i < distance; i++)
            {
                p.X = startPoint.X - i;
                if (!Cells.ContainsKey(p))
                {
                    Cells[p] = new HashSet<int> { wireNumber };
                }
                else
                {
                    Cells[p].Add(wireNumber);
                }
            }
            return new Point(startPoint.X - distance, startPoint.Y);
        }
    }
}
