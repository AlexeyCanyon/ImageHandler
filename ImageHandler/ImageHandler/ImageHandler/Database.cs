using System;
using System.Text;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace ImageHandler
{
    class Database
    {
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }

        public static string TryConnect()
        {
            string host = "gf.sfu-kras.ru";
            int port = 3306;
            string database = "gallery";
            string username = "root";
            string password = "root";
            MySqlConnection conn = GetDBConnection(host, port, database, username, password);
            try
            {
                conn.Open();
                return "Connection successful!";
            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
            }
        }
    }
}
