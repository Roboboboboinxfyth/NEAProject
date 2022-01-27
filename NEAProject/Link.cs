using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;

namespace NEAProject
{
    public struct DestComicSum
    {
        public Comic SourceComic;
        public Comic DestComic;
        public List<string> Atts;
        public int Strength;
    }
    public class Link
    {
        // instance properties
        Comic Comic1;
        Comic Comic2;
        List<string> Atts;

        //constructor
        public Link(Comic Comic1, Comic Comic2, string Att)
        {
            this.Comic1 = Comic1;
            this.Comic2 = Comic2;

            Atts = new List<string>();
            Atts.Add(Att);
        }

        //methods
        
        public void AddAttribute(string Att)
        {
            Atts.Add(Att);
        }

        //public void AddStrength(string Att)
        //{
        //    if (Att == "Author")
        //        LinkStrength = LinkStrength + 4;
        //    else if (Att == "Artist")
        //        LinkStrength = LinkStrength + 3;
        //    else if (Att == "PubYear")
        //        LinkStrength = LinkStrength + 2;
        //    else if (Att == "AgeRating")
        //        LinkStrength = LinkStrength + 1;
        //}

        public int GetStrength(List<string> FilteredAtts)
        {
            int LinkStrength = 0;
            foreach (string Att in Atts)
            {
                if (!FilteredAtts.Contains(Att))
                {
                    if (Att == "Author")
                        LinkStrength = LinkStrength + 4;
                    else if (Att == "Artist")
                        LinkStrength = LinkStrength + 3;
                    else if (Att == "PubYear")
                        LinkStrength = LinkStrength + 2;
                    else if (Att == "AgeRating")
                        LinkStrength = LinkStrength + 1;
                }
            }
            
            return LinkStrength;
        }

        public string GetOtherComicName(Comic SourceComic)
        {
            if (Comic1.GetTitle() != SourceComic.GetTitle())
                return Comic1.GetTitle();
            else
                return Comic2.GetTitle();
        }

        public List<string> GetAtts()
        {
            return Atts;
        }

        

        /// <summary>
        /// Checks if the comic listed in the link is the source comic and 
        /// </summary>
        /// <param name="SourceComic"></param>
        /// <returns>
        /// Returns a response string containing the names of the comics the source comic
        /// is linked to and the attributes they are linked by.
        /// </returns>
        public string DestComicAndAttsAsString(Comic SourceComic)
        {
            if (SourceComic.GetTitle() == "Invincible")
                Console.WriteLine("dfgfgdfg");

            string Response = "";
            Manager SaladCream = ((App)Application.Current).manager; // stupid but memorable variable name, change this at some point

            if (Comic1.GetTitle() != SourceComic.GetTitle())
            {
                Response += Comic1.GetTitle() + ": ";
                foreach (string Att in Atts)
                {
                    // is Att not in FilteredAtts
                    if (!SaladCream.FilteredAtts.Contains(Att))
                        Response += Att + ", ";
                }
            }
            else if (Comic2.GetTitle() != SourceComic.GetTitle())
            {
                Response += Comic2.GetTitle() + ": ";
                foreach (string Att in Atts) // looping through all attributes of the source comic
                {
                    // is Att not in FilteredAtts
                    if (!SaladCream.FilteredAtts.Contains(Att))
                        Response += Att + ", ";
                }
            }
            Response += $"The strength of this link is: {Convert.ToString(GetStrength(SaladCream.FilteredAtts))}";

            return Response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceComic"></param>
        /// <returns> Returns the link as a struct called DestComicSum which contains 2 comics, some attributes and a strength of the link </returns>
        public DestComicSum DestComicAndAttsAsDestComicSum(Comic SourceComic)
        {
            Manager SaladCream = ((App)Application.Current).manager; // stupid but memorable variable name, change this at some point

            DestComicSum Dest = new DestComicSum();
            Dest.SourceComic = SourceComic;

            if (Comic1.GetTitle() != SourceComic.GetTitle())
            {
                Dest.DestComic = Comic1;
            }
            else if (Comic2.GetTitle() != SourceComic.GetTitle())
            {
                Dest.DestComic = Comic2;
            }

            foreach (string Att in Atts)
            {
                if (!SaladCream.FilteredAtts.Contains(Att))
                Dest.Atts.Add(Att);
            }

            Dest.Strength = GetStrength(SaladCream.FilteredAtts);
            return Dest;
        }

    }
}
