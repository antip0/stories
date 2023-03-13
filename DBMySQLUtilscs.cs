using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace stories
{
    class DBMySQLUtilscs
    {
        public static MySqlConnection GetDBConnection(string host, int port, string database, string user, string password)
        {
            string connString = "Server=" + host + ";port=" + port.ToString() + ";database=" + database + ";user=" + user + ";password=" + password + ";";
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }
    }
}
