using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public interface IDatabase
    {
        MySqlConnection Connection { get; set; }
    }
}
