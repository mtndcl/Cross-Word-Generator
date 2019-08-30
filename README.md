# Cross-Word-Generator

Print to console crossword by given string in arraylist.

Just add new string to keys List



private static void Set_Input()
        {


            keys.Add("van");
            keys.Add("mersin");
            keys.Add("ordu");

            keys.Add("ankara");
            keys.Add("istanbul");
            keys.Add("saha");

        

            for (int i=0;i<keys.Count;i++)
            {
                Word word0 = new Word(keys[i]);
                input.Add(word0);
            }

        }.


