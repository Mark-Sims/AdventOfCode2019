using System;
using System.Collections.Generic;
using System.Text;

namespace Day12
{
    class Planet
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }

        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
        public int VelocityZ { get; set; }

        public Planet() { }

        // Copy constructor
        public Planet(Planet p)
        {
            PositionX = p.PositionX;
            PositionY = p.PositionY;
            PositionZ = p.PositionZ;
            VelocityX = p.VelocityX;
            VelocityY = p.VelocityY;
            VelocityZ = p.VelocityZ;
        }

        public override string ToString()
        {
            return string.Format("pos=<x={0}, y={1}, z={2}>, vel=<x={3}, y={4}, z={5}>",
                PositionX,
                PositionY,
                PositionZ,
                VelocityX,
                VelocityY,
                VelocityZ);
        }
    }
}
