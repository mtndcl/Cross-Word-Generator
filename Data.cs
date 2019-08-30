using System;
using System.Collections.Generic;

namespace CrossWord
{
    class Data
    {
    }

    class Location
    {
        private int x;
        private int y;
        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;

        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }

    class Option
    {


        private Cell cell;

        private int victimpoint;

        public Option(Cell cell,int victimpoint)
        {
            this.Cell = cell;
            this.Victimpoint = victimpoint;

        }

        public int Victimpoint { get => victimpoint; set => victimpoint = value; }
        internal Cell Cell { get => cell; set => cell = value; }
    }
}
