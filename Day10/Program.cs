using System;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            string[] lines = System.IO.File.ReadAllLines(inputFile);

            AsteroidField field = new AsteroidField(lines);
            field.CalculateVisibleAsteroids();
            field.IdentifyMonitoringStation();
            field.BuildAsteroidDictFromMonitoringStation();
            field.ZapAsteroids();

            Console.WriteLine("Highest number of visible asteroids: " + field.HighestNumberOfVisibleAsteroids);
        }
    }
}
