﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlashCards.Models
{
    class Data
    {
        public static void Load(string file_name)
        {
            using (StreamReader sr = new StreamReader(file_name))
            using (StreamWriter sw = new StreamWriter("data.txt"))
            {
                sw.Write(sr.ReadToEnd());
            }
        }

        public List<string[]> FlashCards { get; private set; }

        public Data()
        {
            FlashCards = new List<string[]>();

            using (StreamReader sr = new StreamReader("data.txt"))
            {
                string[] line = new string[2];
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split(';');
                    FlashCards.Add(line);
                }
            }
        }

        public void ShuffleCards()
        {
            Random rnd = new Random();
            FlashCards = FlashCards
                .Select(n => n)
                .OrderBy(s => rnd.Next())
                .ToList();
        }
    }
}
