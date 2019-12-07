using System;
using System.Collections.Generic;
using System.Text;

namespace Day3
{
    // A class representing a single cell on the grid, which can be occupied by any number of wires.
    class Cell
    {
        public List<int> Wires { get; set; }

        public Cell()
        {
            Wires = new List<int>();
        }

        public override string ToString()
        {
            if (Wires.Contains(1) && Wires.Contains(2))
            {
                return "+";
            }
            if (Wires.Contains(1))
            {
                return "1";
            }
            if (Wires.Contains(2))
            {
                return "2";
            }
            else
            {
                throw new Exception();
            }
        }
    }
}