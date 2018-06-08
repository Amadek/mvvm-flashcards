using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class User
    {
        private static User _child;

        private User() { }

        public static User GetInstance()
        {
            if (_child == null)
                throw new NullReferenceException("Tip: Run LoadUser() first.");
            return _child;
        }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<string[]> Cards { get; private set; }

        public static void LoadUser(string name)
        {
            _child = new User();
            _child.Name = name;
            _child.ID = DBManager.GetInstance().GetUserId(name);
        }

        public void LoadCardsDB(string unitName)
        {
            Cards = DBManager.GetInstance().GetCards(ID, unitName);
        }

        public void LoadCardsLocal(string fileName = "data.txt")
        {
            Cards = FileManager.GetInstance().GetCards(fileName);
        }

        public void Save()
        {
            FileManager.GetInstance().Save(Cards);
        }
        
        public bool IsCardsEmpty()
        {
            return Cards == null || Cards.Count == 0;
        }

        public void MoveCardToEnd()
        {
            Cards.Add(Cards[0]);
            Cards.RemoveAt(0);
        }

        public void ShuffleCards()
        {
            Random random = new Random();
            Cards = Cards
                .Select(x => x)
                .OrderBy(s => random.Next())
                .ToList();
        }
    }
}
