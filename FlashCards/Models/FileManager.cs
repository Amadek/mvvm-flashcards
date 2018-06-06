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
        private StreamReader _sr;
        private StreamWriter _sw;

        private FileManager() { }

        public static FileManager GetInstance()
        {
            if (_child == null)
                _child = new FileManager();
            return _child;
        }

        public List<Flier> GetData(string fileName = "data.txt")
        {
            Flier flier;
            List<Flier> fliers = new List<Flier>();

            using (_sr = new StreamReader(fileName))
            {
                if (_sr.EndOfStream)
                    throw new InvalidDataException("Plik nie może byćpusty.");

                string[] line = new string[2];
                while (!_sr.EndOfStream)
                {
                    line = _sr.ReadLine().Split(';');
                    if (line.Length != 2)
                        throw new InvalidDataException("Nieprawidłowe dane.");

                    flier = new Flier()
                    {
                        Polish = line[0],
                        English = line[1]
                    };

                    fliers.Add(flier);
                }

                return fliers;
            }
        }

        public void Save(List<Flier> fliers)
        {
            using (_sw = new StreamWriter("data.txt"))
            {
                foreach (var item in fliers)
                {
                    _sw.WriteLine("{0};{1}", item.Polish, item.English);
                }
            }
        }
    }
}
