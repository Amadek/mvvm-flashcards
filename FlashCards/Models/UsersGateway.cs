using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FlashCards.Models
{
    public class UsersGateway
    {
        private string _sql;
        private MySqlCommand _cmd;
        private MySqlDataReader _reader;

        public int GetUserId(string userName, IDatabase db)
        {
            _sql = $@"CALL get_user_id(""{userName}"")";
            _cmd = new MySqlCommand(_sql, db.Connection);

            int result;
            using (_reader = _cmd.ExecuteReader())
            {
                _reader.Read();
                result = _reader.GetInt32(0);
            }
            return result;
        }

        public bool IsValid(string nick, string pass, IDatabase db)
        {
            _sql = $@"CALL is_valid(""{nick}"",""{pass}"")";
            _cmd = new MySqlCommand(_sql, db.Connection);
            using (_reader = _cmd.ExecuteReader())
            {
                if (!_reader.HasRows)
                    return false;
                else
                    return true;
            }
        }

        public bool IsInDB(string nick, IDatabase db)
        {
            _sql = $@"CALL get_user(""{nick}"")";
            _cmd = new MySqlCommand(_sql, db.Connection);
            using (_reader = _cmd.ExecuteReader())
            {
                if (!_reader.HasRows)
                    return false;
                else
                    return true;
            }
        }
    }
}
