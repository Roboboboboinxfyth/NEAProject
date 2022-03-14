using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;

namespace NEAProject
{
    public class Comic
    {
        // instance properties
        //string Author;
        //string Artist;
        //string Title;
        //int PubYear;
        //int AgeRating;
        public Dictionary<string, dynamic> Atts = new Dictionary<string, dynamic>();

        // not an instance property, this is a class property
        public static string[] AttirbuteNames = { "Title", "PubYear", "AgeRating", "Artist", "Author" };

        // constructor
        public Comic(string Author, string Artist, string Title, int PubYear, int AgeRating)
        {
            //this.Author = Author;
            //this.Artist = Artist;
            //this.Title = Title;
            //this.PubYear = PubYear;
            //this.AgeRating = AgeRating;

            Atts.Add("Title", Title);
            Atts.Add("PubYear", PubYear);
            Atts.Add("AgeRating", AgeRating);
            Atts.Add("Artist", Artist);
            Atts.Add("Author", Author);
        }

        // methods


        public dynamic GetAttribute(string Attribute)
        {
            if (Atts.Keys.Contains(Attribute))
            {
                return Atts[Attribute];
            }
            else
            {
                throw new ArgumentException("Unknown attribute provided");
            }
        }

        public string GetTitle()
        {
            return Atts["Title"];
        }

        public string GetInfo()
        {
            string info = "";
            foreach (string Att in Atts.Keys)
            {
                info += $"This comic's {Att} is {Atts[Att]} \n";
            }
            return info;
        }
    }
}
