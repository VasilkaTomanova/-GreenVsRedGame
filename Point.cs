using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRedGame
{
   public class Point
    {
        public Point(int value, int row, int cow)
        {
            this.Value = value;
            this.RowCoordinate = row;
            this.CowCoordinate = cow;
        }

        public int Value { get; set; }
        public int RowCoordinate { get; set; }
        public int CowCoordinate { get; set; }
    }
}
