using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class CardsGateway
    {
        private string _sql;
        private MySqlCommand _cmd;
        private MySqlDataReader _reader;

        public List<string[]> GetCards(int userId, string unit, IDatabase db)
        {
            _sql = $@"CALL show_cards({userId}, ""{unit}"")";
            _cmd = new MySqlCommand(_sql, db.Connection);

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

        public List<string> GetUnits(int userId, IDatabase db)
        {
            _sql = $"CALL get_units({userId})";
            _cmd = new MySqlCommand(_sql, db.Connection);

            List<string> units = new List<string>();

            using (_reader = _cmd.ExecuteReader())
            {
                while (_reader.Read())
                {
                    units.Add(_reader.GetString("unit"));
                }
            }
            return units;
        }
    }
}
