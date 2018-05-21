using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlashCards.Models
{
    class DataModel
    {
        public List<string[]> FlashCards { get; private set; }

        public DataModel(string fileName)
        {
            FlashCards = new List<string[]>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string[] line = new string[2];
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split(';');
                    if (line.Length != 2)
                        throw new InvalidDataException("Nieprawidłowe dane");
                    FlashCards.Add(line);
                }
            }
        }

        public DataModel() : this("data.txt")
        {

        }

        public void Save()
        {
            using (StreamWriter sw = new StreamWriter("data.txt"))
            {
                foreach (var item in FlashCards)
                {
                    sw.WriteLine("{0};{1}", item[0], item[1]);
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
