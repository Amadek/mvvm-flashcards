using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class Database : IDatabase
    {
        private static Database _instance;

        private Database() { }

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Database();
                return _instance;
            }
        }

        public MySqlConnection Connection { get; set; }

        public void Connect()
        {
            this.Connection = new MySqlConnection(
                @"server=localhost;
                database=cards;
                uid=root;
                password=;
                sslmode=none;
                charset=utf8"
            );

            this.Connection.Open();
        }
    }
}
