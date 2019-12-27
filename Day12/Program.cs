using System;
using System.Collections.Generic;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string[] planets = System.IO.File.ReadAllLines(inputFile);

            var solarSystem = new SolarSystem(planets);

            while (!solarSystem.RepeatedArrangement)
            {
                solarSystem.AdvanceTimeStep();
            }

            Console.WriteLine("Repeated arrangement after {0} steps.", solarSystem.planetArrangementDigest.Count);
        }
    }
}
