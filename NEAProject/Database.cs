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
        private static string ConnString = "Data Source=.\\..\\..\\..\\database.sqlite; Version=3;"; //sets the connection string to the location of the database

        public static void StoreSearch(int UserId, string ComicSource, List<string> FiltersActive, List<string> ComicsDest) //stores a search made
        {
            SQLiteConnection Conn = new SQLiteConnection(ConnString);
            Conn.Open(); //opens a connection to the database

            SQLiteCommand SaveSearch = new SQLiteCommand(@"INSERT INTO search (user_id, datetime, comic_source, filters_active, comics_dest)
            VALUES ($UserId, datetime('now'), $ComicSource, $FiltersActive, $ComicsDest)", Conn); //SQLite statement is executed here, this statement
            SaveSearch.Parameters.AddWithValue("$UserId", UserId);                                //writes a new entry into the database, with 
            SaveSearch.Parameters.AddWithValue("$ComicSource", ComicSource);
            SaveSearch.Parameters.AddWithValue("$FiltersActive", string.Join(",", FiltersActive.ToArray()));
            SaveSearch.Parameters.AddWithValue("$ComicsDest", string.Join(",", ComicsDest.ToArray()));

            int RowsChanged = SaveSearch.ExecuteNonQuery();

            if (RowsChanged == 1) //tests whether or not the data was succesfukky entered
                MessageBox.Show("1 Row Inserted Successfuly");
            else
                MessageBox.Show("Insertion failed");

            Conn.Close();
        }

        public static List<History> GetSearchHistoryForUserV2() //this version of the GetSearchHistory method gets all of the histories from the database, but has not yet been
        {                                                       //implemented into the program
            Manager manager = ((App)Application.Current).manager;

            List<string> FiltersAsList = new List<string>();
            //List<string> Output = new List<string>();
            List<History> Output = new List<History>();
            SQLiteConnection Conn = new SQLiteConnection(ConnString);
            Conn.Open();

            SQLiteCommand QueryHistory = new SQLiteCommand(@"SELECT datetime, comic_source, filters_active, comics_dest
                                                             FROM search", Conn);

            SQLiteDataReader DBReader = QueryHistory.ExecuteReader();

            while (DBReader.Read())
            {
                History tempHistory = new History();
                tempHistory.Date_Time = Convert.ToDateTime(DBReader["datetime"].ToString());
                tempHistory.Comic_Source = manager.GraphComic.Graph[DBReader["comic_source"].ToString()].thisComic;
                string FiltersAsString = DBReader["filters_active"].ToString();
                string[] FilterAsArray = FiltersAsString.Split(',');
                FiltersAsList = FilterAsArray.ToList();
                tempHistory.Active_Filters = FiltersAsList;
                tempHistory.Dest_Comics = new List<Comic>();
                string Comics_DestAsString = DBReader["comics_dest"].ToString();
                string[] Com_DestAsStrArr = Comics_DestAsString.Split(',');
                foreach (string Com_Dest in Com_DestAsStrArr)
                {
                    tempHistory.Dest_Comics.Add(manager.GraphComic.Graph[Com_Dest].thisComic);
                }

                Output.Add(tempHistory);

                //Now allow this info to be accessed by the user
            }


            Conn.Close();

            return Output;
        }


        public static History GetSearchHistoryForUser() //work in a parameter for userID
        {
            Manager manager = ((App)Application.Current).manager;

            List<string> FiltersAsList = new List<string>();
            //List<string> Output = new List<string>();
            History Output = new History();
            SQLiteConnection Conn = new SQLiteConnection(ConnString);
            Conn.Open(); //opens a connection to the database

            SQLiteCommand QueryHistory = new SQLiteCommand(@"SELECT datetime, comic_source, filters_active, comics_dest
                                                             FROM search
                                                             WHERE id = (SELECT 
                                                             MAX (search.id)
                                                             FROM search)", Conn); //SQLite statement is executed here, this statement
                                                                                   //retrieves the last stored row from the database
            SQLiteDataReader DBReader = QueryHistory.ExecuteReader();

            while (DBReader.Read())
            {
                Output.Date_Time = Convert.ToDateTime(DBReader["datetime"].ToString()); //Selects the DateTime from the Database Reader
                Output.Comic_Source = manager.GraphComic.Graph[DBReader["comic_source"].ToString()].thisComic;
                string FiltersAsString = DBReader["filters_active"].ToString(); //Selects the Active Filters
                string[] FilterAsArray = FiltersAsString.Split(',');
                FiltersAsList = FilterAsArray.ToList(); //converts them to a list
                Output.Active_Filters = FiltersAsList; //and adds them to the History struvt, Output
                Output.Dest_Comics = new List<Comic>();
                string Comics_DestAsString = DBReader["comics_dest"].ToString();  //Selects the destination comic titles from the Database Reader
                string[] Com_DestAsStrArr = Comics_DestAsString.Split(','); // splits them into a list
                foreach (string Com_Dest in Com_DestAsStrArr) //for each title in the list
                {
                    Output.Dest_Comics.Add(manager.GraphComic.Graph[Com_Dest].thisComic); //finds the instance of the comic class under the name given and adds it to the list
                }

                //Now allow this info to be accessed by the user through Show_History
            }


            Conn.Close();

            return Output;

        }
    }
}
