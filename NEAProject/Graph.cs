using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows;

namespace NEAProject
{
    public class AdjList
    {
        public Dictionary<string, ComicAndLinks> Graph; //this is here because the data type key of a dictionary should not be ab;e to be edited

        public AdjList()
        {
            Graph = new Dictionary<string, ComicAndLinks>();
        }

        public List<string> GetTitles()
        {
            List<string> Titles = new List<string>();
            foreach (string Title in Graph.Keys)
            {
                Titles.Add(Title);
            }

            return Titles;
        }


        public void AddComic(Comic theComic) // should this be here?
        {
            Graph.Add(theComic.GetTitle(), new ComicAndLinks(theComic));
        }

        public void AddLinkInAdjList(Comic Comic1, Comic Comic2, string Att)
        {
            Graph[Comic1.GetTitle()].AddLink(Comic2, Att);
            Graph[Comic2.GetTitle()].AddLink(Comic1, Att);
        }

        public List<Comic> GetComics()
        {
            List<Comic> Comics = new List<Comic>();
            foreach (ComicAndLinks comicandlink in Graph.Values)
            {
                Comics.Add(comicandlink.thisComic);
            }
            return Comics;
        }

        public void FillAdjList()
        {
            // list of all comics as Comic objects
            List<Comic> comiclist = GetComics();

            // loop through all comics that exist
            foreach (Comic comic in comiclist)
            {
                // loop through all comics again!
                foreach (Comic othercomic in comiclist)
                {
                    // not the same comic
                    if (comic.GetTitle() != othercomic.GetTitle())
                    {
                        // compare all the attributes for both comics
                        foreach (string attname in Comic.AttirbuteNames)
                        {
                            if (comic.Atts[attname] == othercomic.Atts[attname])
                            {
                                Trace.WriteLine($"Common att found {comic.GetTitle()}, {othercomic.GetTitle()}, {attname}");
                                AddLinkInAdjList(comic, othercomic, attname);
                            }
                        }
                    }
                    
                }
            }

            //foreach (ComicAndLinks ComAndLin in Graph.Values)
            //{
            //    Dictionary<string, dynamic> Atts1 = ComAndLin.thisComic.Atts;
            //    for (int i = 0; i < ComAndLin.thisComic.Atts.Count; i++)
            //    {
            //        foreach (ComicAndLinks OtherCom in Graph.Values)
            //        {
            //            Dictionary<string, dynamic> Atts2 = OtherCom.thisComic.Atts;
            //            for (int j = 0; j < OtherCom.thisComic.Atts.Count; j++)
            //            {
            //                if (Atts1.Keys[i] == OtherAttName && ComAndLin.thisComic.Atts.Values)
            //            }
            //        }
            //    }
            //    // Add stuff here to find all the links possible
            //}
        }
    }

    public class ComicAndLinks
    {
        public Comic thisComic { get; set; }
        public List<Link> Links;

        public ComicAndLinks(Comic theComic)
        {
            this.thisComic = theComic;
            Links = new List<Link>();
        }


        public void AddLink(Comic DestinationComic, string Att)
        {
            // check if these comics are already linked?
            // if they are add to the attribute list
            // if not, add a new link

            bool DoesALinkAlreadyExist = false;
            bool ExactLink = false;
            int LinkIndex = -1;

            for (int i = 0; i < Links.Count; i++)
            {
                if (Links[i].GetOtherComicName(thisComic) == DestinationComic.GetTitle())
                {
                    foreach (string LinkAtt in Links[i].GetAtts())
                    {
                        if (LinkAtt == Att)
                        {
                            ExactLink = true;
                        }
                    }
                    DoesALinkAlreadyExist = true;
                    LinkIndex = i;
                }
            }

            if (DoesALinkAlreadyExist == false)
            {
                Links.Add(new Link(thisComic, DestinationComic, Att));
                Links[Links.Count - 1].AddStrength(Att); 
            }
            else if (ExactLink == false)
            {
                Links[LinkIndex].AddAttribute(Att);
                Links[LinkIndex].AddStrength(Att);
            }

        }

        /// <summary>
        /// For the current comic gets all links and returns as string. Example:
        /// Comic1, Comic2, Comic3
        /// </summary>
        /// <returns>String list of linked comics</returns>
        public string WhatAmILinkedTo()
        {
            string Response = "";

            foreach (Link theLink in Links)
            {
                Response += theLink.DestComicAndAttsAsString(thisComic) + "\n";
            }

            Trace.WriteLine($"Links to {thisComic.GetTitle()} are = {Response}");
            return Response;
        }

        public List<Link> GetAllConnections()
        {
            return Links;
        }

        public List<Link> OrderLinks(ComicAndLinks SearchedComicAndLinks)
        {
            Link NextLink = null;
            int HighestStrength = -1;
            List<Link> UnorderedLinks = SearchedComicAndLinks.Links;
            List<Link> OrderedLinks = new List<Link>();

            while (UnorderedLinks.Count > 0)
            {
                HighestStrength = -1;
                foreach (Link link in UnorderedLinks)
                {
                    if (link.GetStrength() >= HighestStrength)
                    {
                        HighestStrength = link.GetStrength();
                        NextLink = link;
                    }
                }
                OrderedLinks.Add(NextLink);
                UnorderedLinks.Remove(NextLink);
            }
            return OrderedLinks;
        }
        
    }
}
