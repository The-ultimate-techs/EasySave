using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    public abstract class FileSave
    {
        //attributes
        private string Title;
        private string SourceDirectory;
        private string DestinationDirectory;
        private string Type;



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






    }
}
