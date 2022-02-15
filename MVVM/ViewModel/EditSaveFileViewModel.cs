using EasySave.MVVM.Model;
using EasySave.MVVM.ObjectsForSerialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace EasySave.MVVM.ViewModel
{
    class EditSaveFileViewModel 
    
    {

        FileSaveManagement FileSaveManagement;

        public ObservableCollection<string> TitleList { get; set; } = new ObservableCollection<string>();
        public string Title { get; set; }
        public object SourcePath { get; set; }
        public object DestinationPath { get; set; }
        public bool TypeComplete { get; set; }
        public bool TypeDifferencial { get; set; }





        public RelayCommand Edit { get; set; }


        public EditSaveFileViewModel()
        {
            FileSaveManagement = new FileSaveManagement();
            Clean();
            FullFillList();

            Edit = new RelayCommand(o =>

            {

                SaveFileJson SaveFileJson = new SaveFileJson();
                SaveFileJson.Title = Title;
                SaveFileJson.SourcePath = SourcePath.ToString();
                SaveFileJson.DestPath = DestinationPath.ToString();
                SaveFileJson.Type = (TypeComplete == true) ? "COMPLETE" : "PARTIAL"; ;

                string JsonPath = FileSaveManagement.GetSaveFileDirectory() + Title + ".Json";


                string json = JsonConvert.SerializeObject(SaveFileJson, Formatting.Indented);

                using (StreamWriter sw = File.CreateText(JsonPath))
                {

                    sw.WriteLine(json);
                    sw.Close();

                }

            });







        }


        public void FullFillList()
        {

            List<FileSave> ListFile = new List<FileSave> { };
            ListFile = FileSaveManagement.GetFilesOnADirectory(FileSaveManagement.GetSaveFileDirectory(), "");

            foreach (FileSave FileSave in ListFile)
            {

                TitleList.Add(FileSave.GetTitle().Replace(".Json", ""));
               
            }
                
        }







        public void Clean()
        {
            TitleList.Clear();
            Title = "";
            SourcePath = "";
            DestinationPath = "";
            TypeComplete = false;
            TypeDifferencial = false;
        }
    }
}
