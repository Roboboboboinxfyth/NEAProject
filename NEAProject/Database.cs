using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Windows;
using System.Linq;

namespace NEAProject
{

    public struct History
    {
        public DateTime Date_Time;
        public Comic Comic_Source;
        public List<string> Active_Filters;
        public List<Comic> Dest_Comics;
    }

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
        }


        public static History GetSearchHistoryForUser() //work in a parameter for userID
        {
            Manager manager = ((App)Application.Current).manager;

            List<string> FiltersAsList = new List<string>();
            //List<string> Output = new List<string>();
            History Output = new History();
            SQLiteConnection Conn = new SQLiteConnection(ConnString);
            Conn.Open();

            SQLiteCommand QueryHistory = new SQLiteCommand(@"SELECT datetime, comic_source, filters_active, comics_dest
                                                           FROM search", Conn);

            SQLiteDataReader DBReader = QueryHistory.ExecuteReader();

            //while (DBReader.Read())
            //{
            //    Output.Add(DBReader["datetime"].ToString());
            //    Output.Add(DBReader["comic_source"].ToString());
            //    Output.Add(DBReader["filters_active"].ToString());
            //    Output.Add(DBReader["comics_dest"].ToString());
            //}
            while (DBReader.Read())
            {
                Output.Date_Time = Convert.ToDateTime(DBReader["datetime"].ToString());
                Output.Comic_Source = manager.GraphComic.Graph[DBReader["comic_source"].ToString()].thisComic;
                string FiltersAsString = DBReader["filters_active"].ToString();
                string[] FilterAsArray = FiltersAsString.Split(',');
                FiltersAsList = FilterAsArray.ToList();
                Output.Active_Filters = FiltersAsList;
                Output.Dest_Comics = new List<Comic>();
                string Comics_DestAsString = DBReader["comics_dest"].ToString();
                string[] Com_DestAsStrArr = Comics_DestAsString.Split(',');
                foreach (string Com_Dest in Com_DestAsStrArr)
                {
                    Output.Dest_Comics.Add(manager.GraphComic.Graph[Com_Dest].thisComic);
                }

                //Now allow this info to be accessed by the user
            }


            Conn.Close();

            return Output;
            //// other type here
            //SQLiteDataReader Reader = QueryLogin.ExecuteReader();

            //if (Reader.Read())
            //{
            //    ((App)Application.Current).stateManager.Username = Reader["display_name"].ToString();
            //    Auth_level = Convert.ToInt32(Reader["Auth_Level"]);
            //}
            //else
            //    Auth_level = -1;


        }
    }
}
