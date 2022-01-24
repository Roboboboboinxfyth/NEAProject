using System;
using System.Collections.Generic;
using System.Text;

namespace NEAProject
{
    public class Manager
    {
        public string Username { get; set; }
        public AdjList GraphComic = new AdjList();
        public List<string> FilteredAtts = new List<string>();

        public Manager()
        {
            // create some comics - temp!!
            GraphComic.AddComic(new Comic("Tsugumi Ohba", "Takeshi Obata", "Death Note", 2004, 16));
            GraphComic.AddComic(new Comic("Greg Rucka", "Michael Lark", "Lazarus", 2020, 18));
            GraphComic.AddComic(new Comic("Robert Kirkman", "Charlie Adlard", "The Walking Dead", 2011, 18));
            GraphComic.AddComic(new Comic("Patrick Renault", "Charlie Adlard", "Vampire State Building", 2019, 18));
            GraphComic.AddComic(new Comic("Robert Kirkman", "Ryan Ottley", "Invincible", 2003, 16));
            GraphComic.AddComic(new Comic("Yumi Hotta", "Takeshi Obata", "Hikaru no Go", 1998, 12));
            GraphComic.AddComic(new Comic("Philip Kennedy Johnson", "Salvador Larroca", "Alien (2021)", 2021, 16));
            

            GraphComic.AddLinkInAdjList(GraphComic.Graph["The Walking Dead"].thisComic, GraphComic.Graph["Invincible"].thisComic, "Author");
            GraphComic.AddLinkInAdjList(GraphComic.Graph["The Walking Dead"].thisComic, GraphComic.Graph["Invincible"].thisComic, "PubYear");
            GraphComic.AddLinkInAdjList(GraphComic.Graph["Death Note"].thisComic, GraphComic.Graph["The Walking Dead"].thisComic, "AgeRating");

            //GraphComic.Graph["Invincible"].WhatAmILinkedTo();
            //GraphComic.Graph["Death Note"].WhatAmILinkedTo();

            GraphComic.FillAdjList();
            //GraphComic.Graph["The Walking Dead"].WhatAmILinkedTo();
        }




    }
}
