using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class AsteroidField
    {
        private List<Asteroid> _asteroids;
        public int HighestNumberOfVisibleAsteroids { get; set; }

        public AsteroidField(string[] lines)
        {
            _asteroids = new List<Asteroid>();
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

        public static double CalculateSlopeBetweenAsteroids(Asteroid a, Asteroid b)
        {
            // Hack to account for when the slope is undefined (divide by 0)
            if (a.X == b.X)
            {
                return double.NaN;
            }

            if (a.Y == b.Y)
            {
                if (a.X > b.X)
                {
                    return double.MaxValue;
                }
                if (a.X < b.X)
                {
                    return double.MinValue;
                }
            }

            return (double)(a.Y - b.Y) / (a.X - b.X);
        }

        public void CalculateVisibleAsteroids()
        {
            foreach (var asteroid_1 in _asteroids)
            {
                HashSet<SlopeAndRelativePosition> uniqueSlopesToOtherAsteroids = new HashSet<SlopeAndRelativePosition>();

                foreach (var asteroid_2 in _asteroids)
                {
                    if (asteroid_1 != asteroid_2)
                    {
                        var slope = CalculateSlopeBetweenAsteroids(asteroid_1, asteroid_2);

                        if (asteroid_1.Y < asteroid_2.Y)
                        {
                            uniqueSlopesToOtherAsteroids.Add(
                                new SlopeAndRelativePosition
                                {
                                    Slope = slope,
                                    IsAbove = true
                                }
                            );
                        }
                        else
                        {
                            uniqueSlopesToOtherAsteroids.Add(
                                new SlopeAndRelativePosition
                                {
                                    Slope = slope,
                                    IsAbove = false
                                }
                            );
                        }
                    }
                }

                asteroid_1.NumberOfVisibleAsteroids = uniqueSlopesToOtherAsteroids.Count;
            }

            HighestNumberOfVisibleAsteroids = _asteroids.Select(x => x.NumberOfVisibleAsteroids).Max();
        }
    }
}
