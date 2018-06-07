using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class DBManager
    {
        private static DBManager _child;

        private DBManager() { }

        public static DBManager GetInstance()
        {
            if (_child == null)
            {
                _child = new DBManager();
                _child._connection = new MySqlConnection(
                    @"server=localhost;
                    database=cards;
                    uid=root;
                    password=;
                    sslmode=none;
                    charset=utf8"
                );
                _child._connection.Open();
            }
            return _child;
        }

        private MySqlConnection _connection;
        private MySqlCommand _cmd;
        private string _sql;
        private MySqlDataReader _reader;

        ~DBManager()
        {
            if (this != null)
                this._connection.Close();
        }

        public void Send(string userName, string content)
        {
            _sql = $@"CALL insert_cards({userName},""{content}"")";
            _cmd = new MySqlCommand(_sql, _connection);
            _cmd.ExecuteNonQuery();
        }
        
        public int GetUserId(string userName)
        {
            _sql = $@"CALL get_user_id(""{userName}"")";
            _cmd = new MySqlCommand(_sql, _connection);

            int result;
            using (_reader = _cmd.ExecuteReader())
            {
                _reader.Read();
                result = _reader.GetInt32(0);
            }
            return result;
        }

        public List<string[]> GetCards(int userId, string unit)
        {
            _sql = $@"CALL show_cards({userId}, ""{unit}"")";
            _cmd = new MySqlCommand(_sql, _connection);

            List<string[]> fliers;
            using (_reader = _cmd.ExecuteReader())
            {
                _reader.Read();

                // kot;cat/pies;dog/...;.../
                string content = _reader.GetString("content");

                /* kot;cat  \   new string[] { kot, cat }  \
                 * pies;dog  => new string[] { pies, dog }  => ToList();
                 * ...;...  /   new string[] { ..., ...}   /
                 */
                content = content.Substring(0, content.Length - 1);
                fliers = content
                    .Split('/')
                    .Select(x => x.Split(';'))
                    .ToList();
            }
            return fliers;
        }

        // MORE...
    }
}
