using System;
using System.Collections.Generic;
using System.Linq;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            string[] orbits = System.IO.File.ReadAllLines(inputFile);

            Dictionary<string, Planet> solarSystem = BuildSolarSystem(orbits);

            // Part 1
            Console.WriteLine(DFS(solarSystem["COM"], 0));

            // Part 2
            DFSForPlanet(solarSystem["YOU"], null, "SAN", 0);

            Console.WriteLine("Press any key...");
            Console.ReadLine();

        }

        private static int DFS(Planet planet, int currentDepth)
        {
            // Console.WriteLine("Planet: " + planet.Name + " - Depth Of: " + currentDepth);
            var depthOfAllChildren = 0;
            foreach (var child in planet.ChildPlanets)
            {
                depthOfAllChildren += DFS(child, currentDepth + 1);
            }

            int ret = depthOfAllChildren + currentDepth;
            // Console.WriteLine("Planet: " + planet.Name + " - Returning: " + ret);
            return ret;
        }

        private static void DFSForPlanet(Planet currentPlanet, string previousPlanetName, string targetPlanet, int currentDepth)
        {
            List<Planet> adjacentPlanets = currentPlanet.ChildPlanets.Where(x => x.Name != previousPlanetName).ToList();
            adjacentPlanets.Add(currentPlanet.Parent);

            if (currentPlanet.Name == targetPlanet)
            {
                Console.WriteLine("Found planet '{0}'. Distance is {1}.", targetPlanet, currentDepth);
            }

            foreach (var child in adjacentPlanets)
            {
                if (child != null && child.Name != previousPlanetName)
                {
                    DFSForPlanet(child, currentPlanet.Name, targetPlanet, currentDepth + 1);
                }
            }
        }

        private static Dictionary<string, Planet> BuildSolarSystem(string[] orbits)
        {
            Dictionary<string, Planet> planets = new Dictionary<string, Planet>();

            foreach (var orbit in orbits)
            {
                string parentName = GetParentName(orbit);
                string childName = GetChildName(orbit);

                Planet parent;
                Planet child;

                if (!planets.ContainsKey(parentName))
                {
                    // Console.WriteLine("Creating parent " + parentName);
                    parent = new Planet(parentName, null);
                    planets.Add(parentName, parent);
                }
                else
                {
                    parent = planets[parentName];
                }

                if (!planets.ContainsKey(childName))
                {
                    child = new Planet(childName, parent);
                    planets.Add(childName, child);
                }
                else
                {
                    child = planets[childName];
                }

                parent.ChildPlanets.Add(child);

                if (child.Parent == null)
                {
                    child.Parent = parent;
                }
            }

            return planets;
        }

        private static string GetParentName(string orbit)
        {
            return orbit.Split(")")[0];
        }

        private static string GetChildName(string orbit)
        {
            return orbit.Split(")")[1];
        }
    }
}
