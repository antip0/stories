using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace stories
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "sstoriess";
            string user = "root";
            string password = "4076";
            return DBMySQLUtilscs.GetDBConnection(host, port, database, user, password);
        }
    }
}
