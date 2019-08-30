
using System;
using System.Collections.Generic;



namespace CrossWord
{
    class Program
    {




        
        private static List<Word> input = new List<Word>();
        private static List<String> keys = new List<String>();
        private static int size=15;


        private static char[,] puzzle = new char[size, size];
        private static char[,] result;


        private static double inittime = GetCurrentMilli();

        private static int x_start = 0;
        private static int y_start = 0;
         private static int witdh = 0;
        private static int height = 0;


        private static List<Part> parts = new List<Part>();
        static void Main(string[] args)
        {



            Set_Input();
            init_Puzzle();
           

            Set_Root();

            Word root;
            for (int i=0; i<input.Count;i++)
            {
                root = input[i];

              //  Console.WriteLine("root is : "+root);
                for (int j=0;j<input.Count;j++)
                {
                    //   String victim = input[j].Value;
                    //       Console.Write("Victim : " + input[j] +" ");
                   // Console.Write("Victim : " + input[j].Added + " ");
                    if (root.Direction=='h' &&  input[j].Added==false)
                    {
                        ///Console.WriteLine("H  Root is : " + input[i].Value + "  victim : " + input[j].Value + " root size is : " + input[i].Cells.Count);
                        Add_New_Word_V(root, input[j]);
                       // print_Puzzle();
                    }
                    else if (root.Direction == 'v' && input[j].Added == false)
                    {

                        ///Console.WriteLine("V  Root is : " + input[i].Value + "  victim : " +input[j].Value +" root size is : "+input[i].Cells.Count + " victim statu : "+ input[j].Added);
                        Add_New_Word_H(root, input[j]);
                      
                       // print_Puzzle();

                    }
                    

                }
               // Console.WriteLine();
            }


            trim_matrix();

            double passed = GetCurrentMilli() - inittime;
            Console.WriteLine("Passed Time : " + passed);
            Console.WriteLine("--------------------This is result ----------------------------");
            print_Result(result);


           // SplitMatrix();
        }

        private static void SplitMatrix()
        {
             witdh = 3;
             height = 3;

            int remainx= result.GetLength(0)%height;
            int remainy = result.GetLength(1) % witdh;

          
            Console.WriteLine("Yorgunum  x ,Y : " + remainx + " " + remainy);
            for (int i=0; i<result.GetLength(0);i=i+height)
            {
                for (int j = 0; j<result.GetLength(1); j = j + witdh)
                {


                    if (i + height > result.GetLength(0))
                    {

                        int a=i+ remainx;
                        int b= j + remainy;
                        Console.WriteLine("height  : "+a +" witdh : "+b);

                        if (j + witdh > result.GetLength(1))
                        {
                            Part part = new Part(i, j, remainx, remainy, result);
                            parts.Add(part);
                        }
                        else
                        {
                            Part part = new Part(i, j, remainx, witdh, result);
                            parts.Add(part);
                        }
                      //  parts.Add(part);
                        //Part part0 = new Part(i, j, height, remainy, result);

                        //parts.Add(part0);
                    }else if (j + witdh > result.GetLength(1))
                    {
                        if (i + height > result.GetLength(0))
                        {
                            Part part = new Part(i, j, remainx, remainy, result);
                            parts.Add(part);
                        }
                        else
                        {
                            Part part = new Part(i, j, height, remainy, result);
                            parts.Add(part);
                        }

                    }
                    else
                    {

                        Part part = new Part(i, j, height, witdh, result);

                        parts.Add(part);

                    }

                }

            }


                Console.WriteLine(" x ,Y : " +remainx +" " +remainy);
            ////horizantal
         



            for (int i=0; i<parts.Count;i++)
            {
                Console.WriteLine("--------------------This is a part ----------------------------");
                print_Result(parts[i].Matrix);
            }

           

        }

        public static  double GetCurrentMilli()
        {
            DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
            return javaSpan.TotalMilliseconds;
        }
        private static void trim_matrix()
        {

            int x_max = -1;
            int x_min= size;
            int y_min = size;
            int y_max =-1;
           
            
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    if (puzzle[i, j] != '-')
                    {
                        //Console.WriteLine(" x " + y_min + " y " + y_max);
                        if (j>y_max)
                        {
                            y_max = j;
                            //Console.WriteLine(" char " + puzzle[i, j] +" x, y " + i +" "+j +" max --- min "+y_max +" " +y_min);
                            
                        } if (j<y_min)
                        {
                            y_min = j;
                           // Console.WriteLine(" char " + puzzle[i, j] + " x, y " + i + " " + j + " max --- min" + y_max + " " + y_min);
                            
                        }
                        if (i > x_max)
                        {
                            x_max = i;
                            //Console.WriteLine(" char " + puzzle[i, j] + " x, y " + i + " " + j + " max --- min " + y_max + " " + y_min);

                        }
                        if (i < x_min)
                        {
                            x_min = i;
                            //Console.WriteLine(" char " + puzzle[i, j] + " x, y " + i + " " + j + " max --- min" + y_max + " " + y_min);

                        }

                    }
                    
                }

            }

           // Console.WriteLine("  Y min " + y_min + " Y max " + y_max);
                // Console.WriteLine(" X min " + x_min + " X max " + x_max);


           int witdh = y_max - y_min;
           int height = x_max - x_min;

            result = new char[height+1,witdh+1];

            for (int i=0;i<input.Count;i++)
            {
                for (int j=0;j<input[i].Cells.Count;j++)
                {
                    result[input[i].Cells[j].X-x_min, input[i].Cells[j].Y-y_min] = input[i].Cells[j].Key;
                }

            }

            
            // result = new char[x_max-x_min, y_max-y_min];

        }
        private static void Add_New_Word_H(Word root, Word victim)
        {
            Cell intercell;

            List<Option> options = new List<Option>();
            for (int i = 0; i < root.Value.Length; i++)
            {
                for (int j = 0; j < victim.Value.Length; j++)
                {
                    if (root.Value[i] == victim.Value[j] && victim.Added==false)
                    {
                       
                        intercell = root.Cells[i];

                        // Console.WriteLine("eslenik : " + root.Value + " " + victim.Value + " the key : " + root.Value[i] + " = " + victim.Value[j] +" = " + intercell.Key);
                        if (Can_Add_H(root, victim, intercell, j) && victim.Added == false)
                        {
                         ///Console.WriteLine("save place, root is : " +root.Value +" Victim : "+victim.Value);     
                            Option option = new Option(intercell, j);
                            options.Add(option);
                           // Console.WriteLine(" here : " + root.Value + " " + victim.Value);
                        }
                    }
                }
            }
            if (options.Count > 0)
            {
                Random r = new Random();
                int index = r.Next(options.Count);
                Add_Horizantal(root, victim, options[index].Cell, options[index].Victimpoint);

                options.Clear();

            }


        }
        private static void Add_Horizantal(Word root, Word victim, Cell intercell, int victimpoint)
        {

            String front = victim.Value.Substring(0, victimpoint);
            String back = victim.Value.Substring(victimpoint + 1, victim.Value.Length - victimpoint - 1);

            int x = intercell.X;
            int y = intercell.Y;
            /// Console.WriteLine("intercell : "+intercell.X +" " +intercell.Y +" key : " +intercell.Key );
            /// 
            front = Reverse(front);
            for (int i = 0; i < front.Length; i++)
            {
                y = y - i - 1;
                puzzle[x, y] = front[i];

                Cell cell = new Cell(x, intercell.Y-front.Length+i, front[front.Length - i - 1]);
                victim.Cells.Add(cell);
                y = intercell.Y;
            }
            x = intercell.X;
            y = intercell.Y;

            victim.Cells.Add(intercell);
            for (int i = 0; i < back.Length; i++)
            {
                y = y + i + 1;
                puzzle[x, y] = back[i];
                Cell cell = new Cell(x, y, back[i]);
                victim.Cells.Add(cell);
                y = intercell.Y;
            }

            victim.Added = true;
            victim.Direction = 'h';
            //Console.WriteLine("value : " + victim.Value + "  Addedd : " );
            //print_Puzzle();
             ///Console.WriteLine("value : " +victim.Value+"  ------------------------cells size : "+victim.Cells.Count);
            //for (int a = 0; a < victim.Cells.Count; a++)
            // {
            //Console.WriteLine(victim.Cells[a].Key + " " + " X ,  Y : "+ victim.Cells[a].X +"  "+ victim.Cells[a].Y);
            // }
            // Console.WriteLine();
            //Console.WriteLine("-------------------------");
        }
        private static bool Can_Add_H(Word root, Word victim, Cell intercell, int victimpoint)
        {
            // Console.WriteLine("intercell key : " + intercell.Key + " Location X , Y :" + intercell.X + " " + intercell.Y);
           // Console.WriteLine(" Herer -------------------------------------------------------*");
            List<Location> locations = new List<Location>();

            String front = victim.Value.Substring(0, victimpoint);
            String back = victim.Value.Substring(victimpoint + 1, victim.Value.Length - victimpoint - 1);

         //   Console.WriteLine("front : " + front + "  back : " + back);

            int x = intercell.X;
            int y = intercell.Y;
            for (int i = 0; i < front.Length; i++)
            {
                y = y - i - 1;
                Location location = new Location(x, y);
                locations.Add(location);
             //   Console.WriteLine("X ,  Y : " + x + " " + y);
                y = intercell.Y;
            }
            x = intercell.X;
            y = intercell.Y;
            for (int i = 0; i < back.Length; i++)
            {
                y = y + i + 1;
                Location location = new Location(x, y);
                locations.Add(location);
               // Console.WriteLine("X ,  Y : " + x + " " + y);
                y = intercell.Y;
            }

          
            char  interchar =puzzle[intercell.X, intercell.Y];
            puzzle[intercell.X, intercell.Y] = '-';
            for (int i = 0; i < locations.Count; i++)
            {


                ////Check the location in or out matrix
                if (locations[i].X <= 0 || locations[i].X >= size - 1 || locations[i].Y <= 0 || locations[i].Y >= size - 1)
                {
                    puzzle[intercell.X, intercell.Y] = interchar;
                    return false;
                }


                /////Check the matrix is avaible for the this word or not
                if (puzzle[locations[i].X + 1, locations[i].Y] != '-' || puzzle[locations[i].X - 1, locations[i].Y] != '-'
                    || puzzle[locations[i].X, locations[i].Y + 1] != '-' || puzzle[locations[i].X, locations[i].Y - 1] != '-')
                {
                    puzzle[intercell.X, intercell.Y] = interchar;
                    return false;

                }


                if (puzzle[intercell.X,intercell.Y+1] !='-' || puzzle[intercell.X, intercell.Y - 1] != '-')
                {
                    puzzle[intercell.X, intercell.Y] = interchar;
                    return false;
                }

               

                

                
                
            }
            //Console.WriteLine("intercall  : x ,y " + intercell.X + "  " + intercell.Y +" the word : " +root.Value +"  " +victim.Value +" " +intercell.Key );
            puzzle[intercell.X, intercell.Y] = interchar;
           
            locations.Clear();
            return true;
        }
        private static void Add_New_Word_V(Word root, Word victim)
        {

            Cell intercell;

            List<Option> options = new List<Option>();
            for (int i = 0; i < root.Value.Length; i++)
            {
                 for (int j = 0; j < victim.Value.Length; j++)
                  {
                      if (root.Value[i] == victim.Value[j]  &&  victim.Added==false )
                      {

                          intercell = root.Cells[i];
                          if (Can_Add_V(root, victim, intercell,j) &&  victim.Added==false)
                          {
                            ////Add_Vertical(root, victim, intercell,j);

                            Option option = new Option(intercell,j);

                            options.Add(option);
                            //Console.WriteLine(" here : " + root.Value + " " + victim.Value);
                          }
                      }
                  }
             }

            if (options.Count>0)
            {
                Random r = new Random();
                int index = r.Next(options.Count);
                Add_Vertical(root, victim, options[index].Cell, options[index].Victimpoint);

                options.Clear();

            }
           

        }
        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        private static void Add_Vertical(Word root, Word victim, Cell intercell, int victimpoint)
        {



            String front = victim.Value.Substring(0, victimpoint);
            String back = victim.Value.Substring(victimpoint + 1, victim.Value.Length - victimpoint - 1);

            int x = intercell.X;
            int y = intercell.Y;

            front = Reverse(front);
           
            for (int i = 0; i < front.Length; i++)
            {
                x = x - i - 1;
                puzzle[x, y] = front[i];
               
                

                Cell cell = new Cell(intercell.X - front.Length+i, y, front[front.Length-i-1]);

                
                victim.Cells.Add(cell);
                x = intercell.X;
            }

           

            x = intercell.X;
            y = intercell.Y;
            victim.Cells.Add(intercell);
            for (int i = 0; i < back.Length; i++)
            {
                x = x + i + 1;
                puzzle[x, y] = back[i];
               
                Cell cell = new Cell(x, y, back[i]);
                victim.Cells.Add(cell);
                x = intercell.X;
            }
          // Console.WriteLine("value : " +victim.Value+"  ------------------------cells size : "+victim.Cells.Count);
            //for (int a = 0; a < victim.Cells.Count; a++)
            //{
              //  Console.WriteLine(victim.Cells[a].Key + " " + " X ,  Y : "+ victim.Cells[a].X +"  "+ victim.Cells[a].Y);
            //}
            //Console.WriteLine();
            //Console.WriteLine("-------------------------");
            victim.Added = true;
            victim.Direction = 'v';
        }
        private static bool Can_Add_V(Word root, Word victim,Cell intercell,int victimpoint)
        {

            //Console.WriteLine("intercell key : " +intercell.Key +" Location X , Y :" + intercell.X  +" " +intercell.Y);
           
            List<Location> locations = new List<Location>();

            String front = victim.Value.Substring(0, victimpoint);
            String back = victim.Value.Substring(victimpoint+1, victim.Value.Length-victimpoint-1);

            //Console.WriteLine("front : " +front + "  back : " + back);

            int x = intercell.X;
            int y = intercell.Y;
            for (int i=0;i<front.Length;i++)
            {
                x = x - i - 1;
                Location location = new Location(x,y);
                locations.Add(location);
              //  Console.WriteLine("X ,  Y : " + x+ " " +y);
                x = intercell.X;
            }
             x = intercell.X;
             y = intercell.Y;
            for (int i = 0; i < back.Length; i++)
            {
                x = x + i + 1;
                Location location = new Location(x, y);
                locations.Add(location);
                //Console.WriteLine("X ,  Y : " + x + " " + y);
                x = intercell.X;
            }

            char interchar = puzzle[intercell.X, intercell.Y];
            puzzle[intercell.X, intercell.Y] = '-';
            for (int i=0;i<locations.Count;i++)
            {

                ////Check the location in or out matrix
                if (locations[i].X<=0 || locations[i].X>=size-1 || locations[i].Y <= 0 || locations[i].Y >= size-1)
                {
                    puzzle[intercell.X, intercell.Y] = interchar;
                    return false;
                }

                
                /////Check the matrix is avaible for the this word or not
                if (puzzle[locations[i].X+1,locations[i].Y]!='-' || puzzle[locations[i].X - 1, locations[i].Y] != '-'
                    || puzzle[locations[i].X , locations[i].Y + 1] != '-' || puzzle[locations[i].X , locations[i].Y - 1] != '-')
                {
                    puzzle[intercell.X, intercell.Y] = interchar;
                    return false;

                }

                if (puzzle[intercell.X+1, intercell.Y] != '-' || puzzle[intercell.X-1, intercell.Y] != '-')
                {
                    puzzle[intercell.X, intercell.Y] = interchar;
                    return false;
                }
            }
            puzzle[intercell.X, intercell.Y] = interchar;

            locations.Clear();
            return true;
        }
        private static void Set_Root()
        {
            int x = puzzle.GetLength(0) / 2;    
            int y = (puzzle.GetLength(1) - input[0].Value.Length) / 2;


            input[0].Direction = 'h';
            input[0].Added = true;
            for (int i=0; i<input[0].Value.Length;i++)
            {
                puzzle[x, y] = input[0].Value[i];
               
                Cell cell = new Cell(x,y, input[0].Value[i]);
                input[0].Cells.Add(cell);


                y++;
            }

            
        }
        private static void Set_Input()
        {


            






            keys.Add("hasan");
            keys.Add("sana");
            keys.Add("asılı");

            keys.Add("nasa");
            keys.Add("has");
            keys.Add("saha");

            keys.Add("sah");
            keys.Add("nas");
            keys.Add("san");



            for (int i=0;i<keys.Count;i++)
            {
                Word word0 = new Word(keys[i]);
                input.Add(word0);
            }
            
           
        }
        private static void print_Puzzle()
        {
             
            Console.Write("    ");
            for (int j = 0; j < puzzle.GetLength(1); j++)
            {

                Console.Write(j+ "   ");
            }
            Console.WriteLine();
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {

                Console.Write(i + "   ");
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {

                    Console.Write(puzzle[i, j] + "   ");
                }
                Console.WriteLine();
            }
        }
        private static void print_Result(Char[,]  m)
        {

            Console.Write("    ");
            for (int j = 0; j < m.GetLength(1); j++)
            {

                Console.Write(j + "   ");
            }
            Console.WriteLine();
            for (int i = 0; i < m.GetLength(0); i++)
            {

                Console.Write(i + "   ");
                for (int j = 0; j < m.GetLength(1); j++)
                {

                    Console.Write(m[i, j] + "   ");
                }
                Console.WriteLine();
            }
        }
        private static void init_Puzzle()
        {
            for (int i=0; i<puzzle.GetLength(0);i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {

                    puzzle[i, j] = '-';
                }

            }
        }   

       
    }

    class Part
    {

        private Char[,] matrix;
        

       public  Part(int x_start,int y_start,int height, int witdh, char[,]  result)
        {

            Console.WriteLine("x_start : "+ x_start + " y_start "+ y_start + " witdh " + witdh + " height " + height);
            Matrix = new char[height, witdh];


            for (int i=0;i < height; i++)
            {
                for (int j = 0; j < witdh; j++)
                {
                    Matrix[i, j] = result[x_start + i, y_start + j];
                   // Console.WriteLine("the char : "+ result[x_start + i, y_start + j]);
                }
            }
        }

        public char[,] Matrix { get => matrix; set => matrix = value; }
    }
}
