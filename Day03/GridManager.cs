using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Day03
{
    class GridManager
    {
        public Dictionary<Point, HashSet<Wire>> Cells { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }

        public GridManager(int xDimension, int yDimension, int centerX, int centerY)
        {
            CenterX = centerX;
            CenterY = centerY;
            Cells = new Dictionary<Point, HashSet<Wire>>();
        }

        public void AddCellWithSteps(Point p, int wireNumber, int steps)
        {

            if (!Cells.ContainsKey(p))
            {
                Cells[p] = new HashSet<Wire>();
                Cells[p].Add(
                    new Wire
                    {
                        WireNumber = wireNumber,
                        Steps = steps
                    }
                );
            }
            else
            {
                // If this wire already has a record for having been in this cell.
                // This is filtering out wires that self-intersect. Removing this check
                // means that self intersections will be identified later on, when we check
                // the number of wires passing through a given cell.
                if (Cells[p].Select(x => x.WireNumber).Contains(wireNumber))
                {
                    return;
                }
                Cells[p].Add(
                    new Wire
                    {
                        WireNumber = wireNumber,
                        Steps = steps
                    }
                );
            }
        }

        public Point WalkUp(Point startPoint, int distance, int wireNumber, int steps)
        {
            Point p = new Point();
            p.X = startPoint.X;
            for (int i = 0; i < distance; i++)
            {
                p.Y = startPoint.Y + i;
                AddCellWithSteps(p, wireNumber, steps + i);
            }
            return new Point(startPoint.X, startPoint.Y + distance);
        }

        public Point WalkRight(Point startPoint, int distance, int wireNumber, int steps)
        {
            Point p = new Point();
            p.Y = startPoint.Y;
            for (int i = 0; i < distance; i++)
            {
                p.X = startPoint.X + i;
                AddCellWithSteps(p, wireNumber, steps + i);
            }
            return new Point(startPoint.X + distance, startPoint.Y);
        }

        public Point WalkDown(Point startPoint, int distance, int wireNumber, int steps)
        {
            Point p = new Point();
            p.X = startPoint.X;
            for (int i = 0; i < distance; i++)
            {
                p.Y = startPoint.Y - i;
                AddCellWithSteps(p, wireNumber, steps + i);
            }
            return new Point(startPoint.X, startPoint.Y - distance);
        }

        public Point WalkLeft(Point startPoint, int distance, int wireNumber, int steps)
        {
            Point p = new Point();
            p.Y = startPoint.Y;
            for (int i = 0; i < distance; i++)
            {
                p.X = startPoint.X - i;
                AddCellWithSteps(p, wireNumber, steps + i);
            }
            return new Point(startPoint.X - distance, startPoint.Y);
        }
    }

    class Wire
    {
        public int WireNumber { get; set; }
        public int Steps { get; set; }
    }
}
