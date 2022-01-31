using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Windows;

namespace NEAProject
{
    class Database
    {
        private static string ConnString = "Data Source=.\\..\\..\\..\\database.sqlite; Version=3;";

        public static void StoreSearch(int UserId, string ComicSource, List<string> FiltersActive, List<string> ComicsDest)
        {
            SQLiteConnection Conn = new SQLiteConnection(ConnString);
            Conn.Open();

            SQLiteCommand SaveSearch = new SQLiteCommand(@"INSERT INTO search (user_id, datetime, comic_source, filters_active, comics_dest)
            VALUES ($UserId, datetime('now'), $ComicSource, $FiltersActive, $ComicsDest)", Conn);
            SaveSearch.Parameters.AddWithValue("$UserId", UserId);
            SaveSearch.Parameters.AddWithValue("$ComicSource", ComicSource);
            SaveSearch.Parameters.AddWithValue("$FiltersActive", string.Join(", ", FiltersActive.ToArray()));
            SaveSearch.Parameters.AddWithValue("$ComicsDest", string.Join(", ", ComicsDest.ToArray()));

            int RowsChanged = SaveSearch.ExecuteNonQuery();

            if (RowsChanged == 1)
                MessageBox.Show("1 Row Inserted Successfuly");
            else
                MessageBox.Show("Insertion failed");

            Conn.Close();

            //SQLiteConnection Conn = new SQLiteConnection(ConnString);
            //Conn.Open();

            //SQLiteCommand QueryLogin = new SQLiteCommand(@"SELECT username, Auth_level, display_name
            //                                               FROM Users
            //                                               WHERE username = $Username
            //                                               AND password = $Password", Conn);
            //QueryLogin.Parameters.AddWithValue("$Username", Username);
            //QueryLogin.Parameters.AddWithValue("$Password", Password);

            //// other type here
            //SQLiteDataReader Reader = QueryLogin.ExecuteReader();

            //if (Reader.Read())
            //{
            //    ((App)Application.Current).stateManager.Username = Reader["display_name"].ToString();
            //    Auth_level = Convert.ToInt32(Reader["Auth_Level"]);
            //}
            //else
            //    Auth_level = -1;

            //Conn.Close();
        }


        public static void GetSearchHistoryForUser(int UserId)
        {
            
            //SQLiteCommand QueryLogin = new SQLiteCommand(@"SELECT username, Auth_level, display_name
            //                                               FROM Users
            //                                               WHERE username = $Username
            //                                               AND password = $Password", Conn);
            //QueryLogin.Parameters.AddWithValue("$Username", Username);
            //QueryLogin.Parameters.AddWithValue("$Password", Password);

            //// other type here
            //SQLiteDataReader Reader = QueryLogin.ExecuteReader();

            //if (Reader.Read())
            //{
            //    ((App)Application.Current).stateManager.Username = Reader["display_name"].ToString();
            //    Auth_level = Convert.ToInt32(Reader["Auth_Level"]);
            //}
            //else
            //    Auth_level = -1;

            //Conn.Close();
        }
    }
}
