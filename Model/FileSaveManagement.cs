using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave
{
    class FileSaveManagement:FileSave
    {
        public void CreateSaveFile(string Title= null, string SourceDirectory= null, string DestinationDirectory= null, string Type= null)
        {
            DestinationDirectory = DestinationDirectory + "\\hidsvjk.txt";
            Console.WriteLine(DestinationDirectory);
            FileInfo fi = new FileInfo(@DestinationDirectory);

        }

    }
}
