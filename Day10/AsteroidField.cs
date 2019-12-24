using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class AsteroidField
    {
        private List<Asteroid> _asteroids;
        public int HighestNumberOfVisibleAsteroids { get; set; }

        // A Dict where the key is an asteroid's angle (in radians) relative to the monitoring station.
        // And the value is the list of asteroids that are at that same angle.
        public Dictionary<double, SortedList> AsteroidsByAngle;

        public Asteroid MonitoringStationLocation;

        public AsteroidField(string[] lines)
        {
            _asteroids = new List<Asteroid>();
            AsteroidsByAngle = new Dictionary<double, SortedList>();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '.')
                    {
                        continue;
                    }
                    else
                    {
                        _asteroids.Add(
                            new Asteroid
                            {
                                X = x,
                                Y = y
                            });
                    }
                }
            }
        }

        public static double CalculateRadiansBetweenAsteroids(Asteroid a, Asteroid b)
        {
            return Math.Atan2(-1 * (b.Y - a.Y), b.X - a.X);
        }

        public static double CalculateEuclideanDistance(Asteroid a, Asteroid b)
        {
            return Math.Sqrt(
                Math.Pow(a.X - b.X, 2)
                +
                Math.Pow(a.Y - b.Y, 2)
            );
        }

        public void CalculateVisibleAsteroids()
        {
            foreach (var asteroid_1 in _asteroids)
            {
                HashSet<double> uniqueSlopesToOtherAsteroids = new HashSet<double>();

                foreach (var asteroid_2 in _asteroids)
                {
                    if (asteroid_1 != asteroid_2)
                    {
                        uniqueSlopesToOtherAsteroids.Add(CalculateRadiansBetweenAsteroids(asteroid_1, asteroid_2));
                    }
                }

                asteroid_1.NumberOfVisibleAsteroids = uniqueSlopesToOtherAsteroids.Count;
            }

        }

        public void IdentifyMonitoringStation()
        {
            HighestNumberOfVisibleAsteroids = _asteroids.Select(x => x.NumberOfVisibleAsteroids).Max();
            MonitoringStationLocation = _asteroids.Single(x => x.NumberOfVisibleAsteroids == HighestNumberOfVisibleAsteroids);
        }

        public void BuildAsteroidDictFromMonitoringStation()
        {
            foreach (var asteroid in _asteroids)
            {
                var angle = CalculateRadiansBetweenAsteroids(MonitoringStationLocation, asteroid);

                if (!AsteroidsByAngle.ContainsKey(angle))
                {
                    AsteroidsByAngle[angle] = new SortedList();
                }

                Console.WriteLine("Found Asteroid {0} at angle {1}", asteroid, angle);
                asteroid.DistanceFromMonitoringStation = CalculateEuclideanDistance(MonitoringStationLocation, asteroid);
                if (asteroid != MonitoringStationLocation)
                {
                    AsteroidsByAngle[angle].Add(asteroid.DistanceFromMonitoringStation, asteroid);
                }
            }
        }

        public void ZapAsteroids()
        {
            int asteroidCounter = 1;
            List<double> asteroidAngles = AsteroidsByAngle.Keys.ToList();

            // Quadrant 1
            List<double> quadrant1Asteroids = asteroidAngles.Where(x => x <= (Math.PI / 2) && x > 0).ToList();
            List<double> quadrant4Asteroids = asteroidAngles.Where(x => x <= 0 && x > (-1 * Math.PI / 2)).ToList();
            List<double> quadrant3Asteroids = asteroidAngles.Where(x => x <= (-1 * Math.PI / 2)).ToList();
            List<double> quadrant2Asteroids = asteroidAngles.Where(x => x > (Math.PI / 2)).ToList();

            quadrant1Asteroids.Sort();
            quadrant1Asteroids.Reverse();

            quadrant4Asteroids.Sort();
            quadrant4Asteroids.Reverse();

            quadrant3Asteroids.Sort();
            quadrant3Asteroids.Reverse();

            quadrant2Asteroids.Sort();
            quadrant2Asteroids.Reverse();

            var allAsteroidAnglesInSweepOrder = new List<double>();
            allAsteroidAnglesInSweepOrder = allAsteroidAnglesInSweepOrder.Concat(quadrant1Asteroids).ToList();
            allAsteroidAnglesInSweepOrder = allAsteroidAnglesInSweepOrder.Concat(quadrant4Asteroids).ToList();
            allAsteroidAnglesInSweepOrder = allAsteroidAnglesInSweepOrder.Concat(quadrant3Asteroids).ToList();
            allAsteroidAnglesInSweepOrder = allAsteroidAnglesInSweepOrder.Concat(quadrant2Asteroids).ToList();

            while (AsteroidsByAngle.Count > 0)
            {
                for (int i = 0; i < allAsteroidAnglesInSweepOrder.Count; i++)
                {
                    var currentAngle = allAsteroidAnglesInSweepOrder[i];
                    if (AsteroidsByAngle.ContainsKey(currentAngle) && AsteroidsByAngle[currentAngle].Count > 0)
                    {
                        Console.WriteLine("Zapping asteroid #{0} at {1}", asteroidCounter, AsteroidsByAngle[currentAngle].GetByIndex(0));
                        AsteroidsByAngle[currentAngle].RemoveAt(0);
                        asteroidCounter++;

                        // If this was the final asteroid at this angle
                        if (AsteroidsByAngle[currentAngle].Count == 0)
                        {
                            AsteroidsByAngle.Remove(currentAngle);
                        }
                    }
                }
            }
        }
    }
}
