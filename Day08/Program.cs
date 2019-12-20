using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08
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

            // Part 1
            IEnumerable<int> zerosPerLayer = multilayeredImage.Select(x => x.NumberOf0s);
            int minimumNumberOf0s = zerosPerLayer.Min();

            ImageLayer layerNumberWithMinimumZeros = multilayeredImage.Single(x => x.NumberOf0s == minimumNumberOf0s);

            Console.WriteLine(layerNumberWithMinimumZeros.NumberOf1s * layerNumberWithMinimumZeros.NumberOf2s);

            // Part 2
            ImageLayer finalImage = new ImageLayer();

            // Initialize image with values -1
            //finalImage.Image = Enumerable.Repeat(Enumerable.Repeat(-1, imageWidth).ToList(), imageHeight).ToList();


            for (int i = 0; i < imageHeight; i++)
            {
                finalImage.Image.Add(new List<int>());
                for (int j = 0; j < imageWidth; j++)
                {
                    finalImage.Image[i].Add(-1);
                    for (int l = 0; l < multilayeredImage.Count; l++)
                    {
                        var layer = multilayeredImage[l];
                        if (layer.Image[i][j] == 2)
                        {
                            continue;
                        }
                        //Console.WriteLine("Setting value at {0}, {1} to {2}. Taken from layer {3}", i, j, layer.Image[i][j], l);
                        finalImage.Image[i][j] = layer.Image[i][j];
                        break;
                    }
                }
            }

            PrettyPrintLayer(finalImage);
        }

        public static void PrettyPrintLayer(ImageLayer layer)
        {
            foreach (var row in layer.Image)
            {
                string line = "";
                foreach (var pixel in row)
                {
                    if (pixel == 0)
                    {
                        line += " ";
                    }
                    else
                    {
                        line += pixel.ToString();
                    }
                }
                Console.WriteLine(line);
            }

        }
    }
}
