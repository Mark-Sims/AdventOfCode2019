using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Day3
{
    // A class representing a single cell on the grid, which can be occupied by any number of wires.
    class Cell
    {
        public List<int> Wires { get; set; }
        public Point Coords { get; set; }

        public Cell()
        {
            Wires = new List<int>();
        }
    }
}