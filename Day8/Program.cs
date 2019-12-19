using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = Environment.CurrentDirectory + "//input.txt";

            // Input file should be a single line
            string line = System.IO.File.ReadAllLines(inputFile)[0];

            List<int> flatImage = line.Select(x => int.Parse(x.ToString())).ToList();


            int imageWidth = 25;
            int imageHeight = 6;
            int layerSize = imageWidth * imageHeight;

            int numberOfLayers = flatImage.Count / layerSize;

            List<ImageLayer> multilayeredImage = new List<ImageLayer>();

            for (int l = 0; l < numberOfLayers; l++)
            {
                ImageLayer layer = new ImageLayer();

                for (int i = 0; i < imageHeight; i++)
                {
                    List<int> row = new List<int>();

                    for (int j = 0; j < imageWidth; j++)
                    {
                        int pixel = flatImage[(l * layerSize) + (i * imageWidth) + j];
                        row.Add(pixel);

                        switch (pixel)
                        {
                            case 0:
                                layer.NumberOf0s++;
                                break;
                            case 1:
                                layer.NumberOf1s++;
                                break;
                            case 2:
                                layer.NumberOf2s++;
                                break;
                        }
                    }
                    layer.AddRow(row);
                }
                multilayeredImage.Add(layer);
            }

            IEnumerable<int> zerosPerLayer = multilayeredImage.Select(x => x.NumberOf0s);
            int minimumNumberOf0s = zerosPerLayer.Min();

            ImageLayer layerNumberWithMinimumZeros = multilayeredImage.Single(x => x.NumberOf0s == minimumNumberOf0s);
            //IEnumerable<ImageLayer> layerNumberWithMinimumZeros = multilayeredImage.Where(x => x.NumberOf0s == minimumNumberOf0s);

            Console.WriteLine(layerNumberWithMinimumZeros.NumberOf1s * layerNumberWithMinimumZeros.NumberOf2s);

        }
    }
}
