using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave
{
    public class FileSave
    {
        //attributes
        private string Title;
        private string SourceDirectory;
        private string DestinationDirectory;
        private string Type;
        private string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ @"\EasySave\";
        private string SaveFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ @"\EasySave\SaveFiles\";
        FileInfo FileInfo ;
      


        public FileSave(string Title = null , string SourceDirectory = null , string DestinationDirectory = null, string Type = null)
        {
            SetTitle(Title);
            SetSourceDirectory(SourceDirectory);
            SetDestinationDirectory(DestinationDirectory);
            SetType(Type);

        }





        // Setter & getter for private attributes
        public void SetTitle(string Title)
        {
            this.Title = Title;
        }

        public string GetTitle()
        {
            return this.Title;
        }

        public void SetSourceDirectory(string SourceDirectory)
        {
            this.SourceDirectory = SourceDirectory;
        }

        public string GetSourceDirectory()
        {
            return this.SourceDirectory;
        }

        public void SetDestinationDirectory(string DestinationDirectory)
        {
            this.DestinationDirectory = DestinationDirectory;
        }

        public string GetDestinationDirectory()
        {
            return this.DestinationDirectory;
        }

        public void SetType(string Type)
        {
            this.Type = Type;
        }

        public string GetType_()
        {
            return this.Type;
        }


        public string GetDirectoryPath()
        {
            return this.DirectoryPath;
        }

        public string GetSaveFileDirectory()
        {
            return this.SaveFileDirectory;
        }

        public long FileSize(string Path)
        {
            FileInfo = new FileInfo(Path);
            return FileInfo.Length;
        }
        
      

    }


    public class DirectorySave
    {

        public string Title { get; set; }
        public string SourceDirectory { get; set; }
        public string DestinationDirectory { get; set; }



    }





}
