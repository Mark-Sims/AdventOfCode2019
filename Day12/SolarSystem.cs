using System;
using System.Collections.Generic;
using System.Text;

namespace Day12
{
    class SolarSystem
    {
        public List<Planet> planets;

        public HashSet<string> planetArrangementDigest;
        public static int[] Primes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173 };
        public bool RepeatedArrangement = false;

        public SolarSystem(string[] planetPositions)
        {
            planets = new List<Planet>();
            planetArrangementDigest = new HashSet<string>();

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

            SaveSolarSystemStateDigest();
            // Overwrite the list of planets with the new list of planets that contains re-calculated values
            planets = planetsAfterCalculations;
        }

        public void SaveSolarSystemStateDigest()
        {
            string digest = "";
            for (int i = 0; i < planets.Count; i++)
            {
                digest += planets[i].PositionX;
                digest += ",";
                digest += planets[i].PositionY;
                digest += ",";
                digest += planets[i].PositionZ;
                digest += ",";
                digest += planets[i].VelocityX;
                digest += ",";
                digest += planets[i].VelocityY;
                digest += ",";
                digest += planets[i].VelocityZ;
                digest += ",";
            }

            if (planetArrangementDigest.Contains(digest))
            {
                RepeatedArrangement = true;
                foreach (var planet in planets)
                {
                    Console.WriteLine(planet);
                }
            }

            planetArrangementDigest.Add(digest);
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
}
