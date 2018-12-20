using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
using MessageBox = System.Windows.MessageBox;


namespace ImageHandler
{
    static class Database
    {
        static MySqlConnection conn;

        private static bool TryConnect()
        {
            string host = "gf.sfu-kras.ru";
            int port = 3306;
            string database = "gallery";
            string username = "root";
            string password = "root";
            String connString = "Server=" + host + ";Database=" + database
               + ";port=" + port + ";User Id=" + username + ";password=" + password;
            conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка подключения к базе данных: " + e.Message);
                return false;
            }
        }

        public static Picture[] GetPictures()
        {
            Picture[] pictures = new Picture[0];
            if (TryConnect())
            {
                string sql = "SELECT * FROM main WHERE razdel = 'Живопись'";
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Array.Resize(ref pictures, pictures.Length + 1);
                    pictures[pictures.Length - 1] = new Picture();
                    pictures[pictures.Length - 1].ID = reader[0].ToString();
                    pictures[pictures.Length - 1].Name = reader[1].ToString();
                    pictures[pictures.Length - 1].Author = reader[2].ToString();
                    pictures[pictures.Length - 1].YearOfCreation = reader[3].ToString();
                    pictures[pictures.Length - 1].PlaceOfCreation = reader[6].ToString();
                    pictures[pictures.Length - 1].PlaceOfStorage = reader[7].ToString();
                    pictures[pictures.Length - 1].Material = reader[8].ToString();
                    pictures[pictures.Length - 1].Size = reader[9].ToString();
                    pictures[pictures.Length - 1].Rules = reader[11].ToString();
                    pictures[pictures.Length - 1].File = "http://gf.sfu-kras.ru/" + reader[12].ToString();
                }
                reader.Close();
                conn.Close();
            }
            return pictures;
        }

        public static void ChangePicture(Picture picture)
        {
            if (TryConnect())
            {
                /*
                string sql = "Update main set name= @name, author=@author, date=@date, sozdanie=@sozdanie, hranenie=@hranenie, material=@material, pravila=@pravila where id = @id";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@name", SqlDbType.Text).Value = picture.Name;
                cmd.Parameters.Add("@empId", SqlDbType.Decimal).Value = 7369;*/
                conn.Close();
            }
        }
    }
}
