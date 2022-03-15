using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NEAProject
{
    class FileHandler
    {
        public void ComicImport()
        {
            string InFileName = @".\..\..\..\Comics.txt";
            StreamReader InFile = new StreamReader(InFileName);

            while (!InFile.EndOfStream)
            {
                string Line = InFile.ReadLine();
                string[] Components = Line.Split(",");
                GraphComic.AddComic(new Comic(Components[0], Components[1], Components[2], Convert.ToInt32(Components[3], Convert.ToInt32(Components[4]))));
            }
            InFile.Close();
        }
            

    }
}
