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
            string PathSource = SourceDirectory + "\\" + Title;
            string PathDestination = DestinationDirectory + "\\" + Title;

            // Inilization of variables about date and time of last modification for each files in source path and destination path
            DateTime DateTimeFileLastModifySource = File.GetLastWriteTime(PathSource);
            DateTime DateTimeFileLastModifyDestination = File.GetLastWriteTime(PathDestination);


            // If file in destination folder doesn't exist
            if (!File.Exists(PathDestination))
            {
                if (File.Exists(PathSource)) // If file in source folder exist
                {
                    File.Copy(PathSource, PathDestination); // The file is copied from the source folder to the destination folder
                }
                else
                {
                    Console.WriteLine("error file source doesn't exist or not find"); // error message
                }
            }
          
            // If file in destination folder exist
            else
            {
                if (Type == "differential") // Type of backup selected is differential
                {
                    if (!File.Exists(PathSource) && File.Exists(PathDestination)) // Verify if the file in destination folder exist while it doesn't exist in source folder
                    {
                        File.Delete(PathDestination); // The destination folder's file is deleted
                        Console.WriteLine("file source doesn't exist or not find, so saved file was deleted"); // Error message
                    }
                    else if (DateTimeFileLastModifySource != DateTimeFileLastModifyDestination) // Verify if date and time of last modification for each files in source path and destination path are different
                    {
                        File.Delete(PathDestination); // The destination folder's file is deleted
                        File.Copy(PathSource, PathDestination); // The file is copied from the source folder to the destination folder 
                    }
                }
                else if (Type == "complete")// Type of backup selected is complete
                {
                    if (File.Exists(PathSource)) // If the file exist in the source folder
                    {
                        File.Copy(PathSource, PathDestination, true); // The file in the destination folder is overwritten by the source file  
                    }
                    else
                    {
                        File.Delete(PathDestination); // The destination folder's file is deleted
                        Console.WriteLine("file source doesn't exist or not find, so saved file was deleted"); // Error message
                    }
                }
                else // This error is handled by the view
                {
                    Console.WriteLine("error"); // Error message
                }
            }
        }


        public List<FileSave> GetFilesOnADirectory(string SourceDirectory, string DestinationDirectory)
        {


            List<FileSave> ListFile = new List<FileSave> { };


            string[] files = Directory.GetFiles(@SourceDirectory); // Get all the files in the specified directory
            foreach (string file in files)
            {
                FileSave obj = new FileSave();
                obj.SetSourceDirectory(file);
                obj.SetDestinationDirectory(file.Replace(SourceDirectory, DestinationDirectory));
                string FileTitle = file;
                while (FileTitle.Contains('\\'))
                {
                    FileTitle = FileTitle.Substring(file.IndexOf("\\") - 1);
                }
                obj.SetTitle(FileTitle);

                ListFile.Add(obj);

            }

            return ListFile;





        }


        public List<DirectorySave> GetDirectoriesOnADirectory(string SourceDirectory, string DestinationDirectory)
        {

            List<DirectorySave> ListDirectory = new List<DirectorySave> { };


            string[] directories = Directory.GetDirectories(@SourceDirectory,"",SearchOption.AllDirectories); // Get all the files in the specified directory
            foreach (string directory in directories)
            {
                DirectorySave obj = new DirectorySave();
                obj.SourceDirectory = directory;
                obj.DestinationDirectory = directory.Replace(SourceDirectory, DestinationDirectory);

                string DirectoryTitle = directory;
                while (DirectoryTitle.Contains('\\'))
                {
                    DirectoryTitle = DirectoryTitle.Substring(directory.IndexOf("\\") - 1);
                }
                obj.Title = DirectoryTitle;

                ListDirectory.Add(obj);

            }

            return ListDirectory;

        }

        public void CopyDirectories(string SourceDirectory, string DestinationDirectory)
        {

            List<DirectorySave> objects = new List<DirectorySave> { };
            FileSaveManagement A = new FileSaveManagement();

            string Path;
            string[] Path_array;


            objects= A.GetDirectoriesOnADirectory(SourceDirectory, DestinationDirectory);
            
            for (int i= 0; i< objects.Count; i++)
            {
                if(!Directory.Exists(objects[i].DestinationDirectory)) // If the Destination Directory doesn't exist
                {
                    Path = objects[i].DestinationDirectory; // Get the full path of the destination folder
                    Path_array= Path.Split("\\"); // Split the path to obtain folders names in an array


                    Path = "C:\\";
                    for (int j= 1; j < Path_array.Length; j++) // Rebuild of the path to create all the missing directory
                    {
                        Path = Path + Path_array[j] + "\\";
                        Directory.CreateDirectory(Path); // create the missing directory
                    }
                }
            }
            
            
            


        }

    }
}
