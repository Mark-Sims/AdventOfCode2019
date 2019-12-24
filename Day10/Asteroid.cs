namespace Day10
{
    class Asteroid
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int NumberOfVisibleAsteroids { get; set; }
        public double DistanceFromMonitoringStation { get; set; }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", X, Y);
        }
    }

}
