using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlashCards.Models
{
    public class FileManager
    {
        private static FileManager _child;

        private FileManager() { }

        public static FileManager GetInstance()
        {
            if (_child == null)
                _child = new FileManager();
            return _child;
        }

        private StreamReader _sr;
        private StreamWriter _sw;

        public List<string[]> GetCards(string fileName)
        {
            List<string[]> fliers = new List<string[]>();

            using (_sr = new StreamReader(fileName))
            {
                if (_sr.EndOfStream)
                    throw new InvalidDataException("Plik nie może być pusty.");

                string[] line = new string[2];
                while (!_sr.EndOfStream)
                {
                    line = _sr.ReadLine().Split(';');
                    if (line.Length != 2)
                        throw new InvalidDataException("Nieprawidłowe dane.");
                    
                    fliers.Add(line);
                }

                return fliers;
            }
        }

        public void Save(List<string[]> fliers)
        {
            using (_sw = new StreamWriter("data.txt"))
            {
                foreach (var item in fliers)
                {
                    _sw.WriteLine("{0};{1}", item[0], item[1]);
                }
            }
        }
    }
}
