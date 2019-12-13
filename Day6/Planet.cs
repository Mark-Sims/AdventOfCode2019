using System;
using System.Collections.Generic;
using System.Text;

namespace Day6
{
    class Planet
    {
        public Planet(string name)
        {
            Name = name;
            ChildPlanets = new List<Planet>();
        }

        public string Name { get; set; }
        public List<Planet> ChildPlanets;
    }
}
