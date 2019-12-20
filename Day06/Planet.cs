using System;
using System.Collections.Generic;
using System.Text;

namespace Day06
{
    class Planet
    {
        public Planet(string name, Planet parent)
        {
            Name = name;
            Parent = parent;
            ChildPlanets = new List<Planet>();
        }

        public string Name { get; set; }
        public Planet Parent;
        public List<Planet> ChildPlanets;
    }
}
