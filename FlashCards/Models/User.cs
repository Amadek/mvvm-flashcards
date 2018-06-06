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
                throw new NullReferenceException();
            return _child;
        }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<Flier> Fliers { get; set; }

        public static void LoadUser(string name)
        {
            _child = new User();
            _child.Name = name;
            _child.ID = DBManager.GetInstance().GetUserId(name);
        }
    }
}
