using System;
using System.Collections.Generic;
using System.Text;

namespace CrossWord
{



    class Cell
    {


        private char key;

        private bool used;

        private int x;

        private int y;
        public Cell(int x,int y,char key)
        {
            this.x = x;
            this.y = y;
            this.Key = key;
            this.Used = false;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public char Key { get => key; set => key = value; }
        public bool Used { get => used; set => used = value; }
    }
    class Word
    {


        private String value;

        private char direction;

        private List<Cell> cells;

        private bool added;
        public Word(String value)
        {
            Cells = new List<Cell>();
            this.Value = value;
            this.Added = false;
        }

        public string Value { get => value; set => this.value = value; }
        public char Direction { get => direction; set => direction = value; }
        public bool Added { get => added; set => added = value; }
        internal List<Cell> Cells { get => cells; set => cells = value; }
    }
}
