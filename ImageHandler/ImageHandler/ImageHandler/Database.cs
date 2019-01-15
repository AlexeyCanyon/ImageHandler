using System;
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
            string username = "ImageHandler";
            string password = "ImageHandler";
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
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
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
                    pictures[pictures.Length - 1].YearMap = Convert.ToInt32(reader[16]);
                    pictures[pictures.Length - 1].Height = Convert.ToSingle(Math.Round(Convert.ToDouble(reader[17]), 2));
                    pictures[pictures.Length - 1].Width = Convert.ToSingle(Math.Round(Convert.ToDouble(reader[18]), 2));
                    pictures[pictures.Length - 1].LongitudeCreation = Convert.ToSingle(reader[19]);
                    pictures[pictures.Length - 1].LatitudeCreation = Convert.ToSingle(reader[20]);
                    pictures[pictures.Length - 1].LongitudeStorage = Convert.ToSingle(reader[21]);
                    pictures[pictures.Length - 1].LatitudeStorage = Convert.ToSingle(reader[22]);
                    pictures[pictures.Length - 1].PercentOfRed = Math.Round(Convert.ToDouble(reader[23]), 2);
                    pictures[pictures.Length - 1].PercentOfGreen = Math.Round(Convert.ToDouble(reader[24]), 2);
                    pictures[pictures.Length - 1].PercentOfBlue = Math.Round(Convert.ToDouble(reader[25]), 2);
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
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                string sql = "Update main set name= '"+ picture.Name + "', author='" + picture.Author + "', date='"+ picture.YearOfCreation + 
                    "', sozdanie='" + picture.PlaceOfCreation + "', hranenie='" + picture.PlaceOfStorage + "', material='" + picture.Material + 
                    "', pravila='"+ picture.Rules + "', map_date=" + picture.YearMap + ", height=" + picture.Height + ", width=" + picture.Width +
                    ", longitude_creation=" + picture.LongitudeCreation + ", latitude_creation=" + picture.LatitudeCreation +
                    ", longitude_storage=" + picture.LongitudeStorage + ", latitude_storage=" + picture.LatitudeStorage + " where id = " + picture.ID;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void SetSaturation(Picture picture)
        {
            if (TryConnect())
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                string sql = "Update main set percent_red=" + picture.PercentOfRed + ", percent_green=" + picture.PercentOfGreen + ", percent_blue=" + picture.PercentOfBlue + " where id = " + picture.ID;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
