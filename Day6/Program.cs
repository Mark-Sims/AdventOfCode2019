using System;
using System.Collections.Generic;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            string[] orbits = System.IO.File.ReadAllLines(inputFile);

            Planet COM = BuildSolarSystem(orbits);

            Console.WriteLine(DFS(COM, 0));
            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }

        private static int DFS(Planet planet, int currentDepth)
        {
            Console.WriteLine("Planet: " + planet.Name + " - Depth Of: " + currentDepth);

            var depthOfAllChildren = 0;
            foreach (var child in planet.ChildPlanets)
            {
                depthOfAllChildren += DFS(child, currentDepth + 1);
            }

            int ret = depthOfAllChildren + currentDepth;
            Console.WriteLine("Planet: " + planet.Name + " - Returning: " + ret);
            return ret;
        }

        private static Planet BuildSolarSystem(string[] orbits)
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
                    parent = new Planet(parentName);
                    planets.Add(parentName, parent);
                }
                else
                {
                    parent = planets[parentName];
                }

                if (!planets.ContainsKey(childName))
                {
                    child = new Planet(childName);
                    planets.Add(childName, child);
                }
                else
                {
                    child = planets[childName];
                }

                parent.ChildPlanets.Add(child);
            }

            return planets["COM"];
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
