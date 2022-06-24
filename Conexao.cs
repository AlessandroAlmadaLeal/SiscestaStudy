using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscesta
{
    public class Conexao
    {
        MySqlConnection con = new MySqlConnection();

        string server = "localhost";
        string port = "3306";
        string user = "root";
        string passwd = "";
        string database = "siscesta";

        public Conexao()
        {
            con.ConnectionString = $"server={server};database={database}; user id={user}; " +
                $"password={passwd}; port={port}";
        }

        public MySqlConnection connect()
        {
            if(con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public void disconnect()
        {
            if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
