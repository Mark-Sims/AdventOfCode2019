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

            // Part 1
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("After {0} steps.", i);
                foreach (Planet p in solarSystem.planets)
                {
                    Console.WriteLine(p);
                }
                solarSystem.AdvanceTimeStep();
            }

            Console.WriteLine("Total energy in the system: {0}", solarSystem.CalculateTotalEnergy());

        }
    }

    class SolarSystem
    {

        public List<Planet> planets;
        public SolarSystem(string[] planetPositions)
        {
            planets = new List<Planet>();

            foreach (var planetPosition in planetPositions)
            {
                var xStart = planetPosition.IndexOf('=') + 1;
                var xLength = planetPosition.IndexOf(',', xStart) - xStart;

                var yStart = planetPosition.IndexOf('=', xStart) + 1;
                var yLength = planetPosition.IndexOf(',', yStart) - yStart;

                var zStart = planetPosition.IndexOf('=', yStart) + 1;
                var zLength = planetPosition.IndexOf('>', zStart) - zStart;

                planets.Add(new Planet
                {
                    PositionX = int.Parse(planetPosition.Substring(xStart, xLength)),
                    PositionY = int.Parse(planetPosition.Substring(yStart, yLength)),
                    PositionZ = int.Parse(planetPosition.Substring(zStart, zLength)),
                    VelocityX = 0,
                    VelocityY = 0,
                    VelocityZ = 0
                });

            }
        }

        public void AdvanceTimeStep()
        {
            // Make a copy of all planets, since the position/velocity changes should only be
            // applied once all calculations have been made. We do not want to apply changes while
            // we still need to use the original values for calculations on other planets.
            List<Planet> planetsAfterCalculations = new List<Planet>();

            foreach (var original in planets)
            {
                Planet p = new Planet(original);

                foreach (var comparisonPlanet in planets)
                {

                    if (original == comparisonPlanet)
                    {
                        continue;
                    }

                    ApplyGravity(p, original, comparisonPlanet);
                }

                ApplyVelocity(p);
                planetsAfterCalculations.Add(p);
            }

            // Overwrite the list of planets with the new list of planets that contains re-calculated values
            planets = planetsAfterCalculations;
        }

        public void ApplyGravity(Planet newPlanet, Planet original, Planet comparisonPlanet)
        {
            if (original.PositionX > comparisonPlanet.PositionX)
            {
                newPlanet.VelocityX -= 1;
            }
            else if (original.PositionX < comparisonPlanet.PositionX)
            {
                newPlanet.VelocityX += 1;
            }

            if (original.PositionY > comparisonPlanet.PositionY)
            {
                newPlanet.VelocityY -= 1;
            }
            else if (original.PositionY < comparisonPlanet.PositionY)
            {
                newPlanet.VelocityY += 1;
            }

            if (original.PositionZ > comparisonPlanet.PositionZ)
            {
                newPlanet.VelocityZ -= 1;
            }
            else if (original.PositionZ < comparisonPlanet.PositionZ)
            {
                newPlanet.VelocityZ += 1;
            }
        }

        public void ApplyVelocity(Planet p)
        {
            p.PositionX += p.VelocityX;
            p.PositionY += p.VelocityY;
            p.PositionZ += p.VelocityZ;

        }

        public int CalculateTotalEnergy()
        {
            int totalEnergy = 0;
            foreach (var planet in planets)
            {
                int potentialEnergy =
                    Math.Abs(planet.PositionX) +
                    Math.Abs(planet.PositionY) +
                    Math.Abs(planet.PositionZ);

                int kineticEnergy =
                    Math.Abs(planet.VelocityX) +
                    Math.Abs(planet.VelocityY) +
                    Math.Abs(planet.VelocityZ);

                totalEnergy += potentialEnergy * kineticEnergy;
            }

            return totalEnergy;
        }
    }

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
