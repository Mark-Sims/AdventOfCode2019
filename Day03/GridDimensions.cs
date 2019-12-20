using System;
using System.Collections.Generic;
using System.Text;

namespace Day03
{
    class GridDimensions
    {
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }

        public int DimensionX { get; set; }
        public int DimensionY { get; set; }

        public override string ToString()
        {
            string val =
                   String.Format("           {0}           \n", MaxY);
            val += String.Format(" {0}                 {1} \n", MinX, MaxX);
            val += String.Format("           {0}           \n", MinY);
            val += String.Format("Total: ({0}, {1})", DimensionX, DimensionY);

            return val;
        }
    }

}
