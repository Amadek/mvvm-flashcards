using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class User
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<string[]> Cards { get; private set; }

        public User(string name)
        {
            UsersGateway gateway = new UsersGateway();
            Name = name;
            ID = gateway.GetUserId(name, Database.Instance);
        }

        public void LoadCardsDB(string unitName)
        {
            CardsGateway gateway = new CardsGateway();
            Cards = gateway.GetCards(ID, unitName, Database.Instance);
        }

        public void LoadCardsLocal(string fileName = "data.txt")
        {
            FileManager manager = new FileManager();
            Cards = manager.GetCards(fileName);
        }

        public void Save()
        {
            FileManager manager = new FileManager();
            manager.Save(Cards);
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
