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
            // Inilization of variables about path of the destination and path of the source
            string PathSource = SourceDirectory + "\\" + Title + ".txt";
            string PathDestination = DestinationDirectory + "\\" + Title + ".txt";

            // Inilization of variables about date and time of last modification for each files in source path and destination path
            DateTime DateTimeFileLastModifySource = File.GetLastWriteTime(PathSource);
            DateTime DateTimeFileLastModifyDestination = File.GetLastWriteTime(PathDestination);


            // If file in destination folder doesn't exist
            if (!File.Exists(PathDestination))
            {
                if (File.Exists(PathSource))
                {
                    File.Copy(PathSource, PathDestination);
                }
                else
                {
                    Console.WriteLine("error file source doesn't exist or not find");
                }
            }
          
            // If file in destination folder exist
            else
            {
                if (Type == "differential") // Type of backup selected is differential
                {
                    if (!File.Exists(PathSource) && File.Exists(PathDestination)) // Verify if the file in destination folder exist while it doesn't exist in source folder
                    {
                        File.Delete(PathDestination);
                        Console.WriteLine("file source doesn't exist or not find, so saved file was deleted");
                    }
                    else if (DateTimeFileLastModifySource != DateTimeFileLastModifyDestination) // Verify if date and time of last modification for each files in source path and destination path are different
                    {
                        File.Delete(PathDestination);
                        File.Copy(PathSource, PathDestination);
                    }
                }
                else if (Type == "complete")// Type of backup selected is complete
                {
                    if (File.Exists(PathSource))
                    {
                        File.Copy(PathSource, PathDestination, true);
                    }
                    else
                    {
                        File.Delete(PathDestination);
                        Console.WriteLine("file source doesn't exist or not find, so saved file was deleted");
                    }
                }
                else //
                {
                    Console.WriteLine("error");
                }
            }
        }

    }
}
