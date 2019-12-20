using System;
using System.Collections.Generic;
using System.Text;

namespace Day08
{
    class ImageLayer
    {
        public int Width
        {
            get
            {
                if (Image.Count == 0 || Image[0] == null)
                {
                    return 0;
                }
                return Image[0].Count;
            }
        }

        public int Height
        {
            get
            {
                return Image.Count;
            }
        }

        public List<List<int>> Image { get; set; }

        public int NumberOf0s { get; set; }
        public int NumberOf1s { get; set; }
        public int NumberOf2s { get; set; }

        public ImageLayer()
        {
            Image = new List<List<int>>();
            NumberOf0s = 0;
            NumberOf1s = 0;
            NumberOf2s = 0;
        }

        public void AddRow(List<int> row)
        {
            Image.Add(row);
        }
    }
}
