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

        

        //public List<string> GetAllAttributeNames()
        //{
        //    List<string> Attributes = new List<string>();
        //    foreach (string Key in Atts.Keys)
        //    {
        //        Attributes.Add(Key);
        //    }
        //    return Attributes;
        //}


        // this selection statement will get very repetitive!!!
        // it will need to be refactored at some point in the future
        //public List<Link> GetLinkedComicsByAttribute()
        //{
        //    List<Link> CommonComics = new List<Link>();

        //    Manager manager = ((App)Application.Current).manager;


        //    //look at property
        //    foreach (Comic comic in manager.ComicsTemp)
        //    {
        //        //if (Author == comic.GetAttribute(Attribute) && Attribute == "Author" && Title != comic.GetTitle())
        //        //{
        //        //    CommonComics.Add(new Link(Title, comic.GetTitle(), Attribute));
        //        //}
        //        foreach (String Attribute in comic.GetAllAttributeNames())
        //        if (Atts[Attribute] == comic.GetAttribute(Attribute) && Atts["Title"] != comic.GetAttribute("Title"))
        //        {
        //            CommonComics.Add(new Link(Atts["Title"], comic.GetAttribute("Title"), Attribute));
        //        }
        //    }

        //    return CommonComics;
        //}

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

        //public string GetAuthor()
        //{
        //    return Author;
        //}

        //public string GetArtist()
        //{
        //    return Artist;
        //}

        //public int GetPubYear()
        //{
        //    return PubYear;
        //}

        //public int GetAgeRating()
        //{
        //    return AgeRating;
        //}

        //public string GetCreators()
        //{
        //    if (Author == Artist)
        //    {
        //        return $"{Title} is written and illustrated by {Author}";
        //    }
        //    else
        //        return $"{Title} is written by {Author} and illustrated by {Artist}";
        //}
    }
}
