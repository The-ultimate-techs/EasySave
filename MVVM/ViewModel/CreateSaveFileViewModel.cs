using System;
using System.Collections.Generic;
using System.Text;
using EasySave.MVVM.Model;
using EasySave.MVVM.ObjectsForSerialization;
using Newtonsoft.Json;
using System.IO;

namespace EasySave.MVVM.ViewModel
{
    class CreateSaveFileViewModel
    {
        FileSaveManagement FileSaveManagement;


        public string Title { get; set; }
        public object SourcePath { get; set; }
        public object DestinationPath { get; set; }
        public bool TypeComplete { get; set; }
        public bool TypeDifferencial { get; set; }








        public RelayCommand CreateCommand { get; set; }



        public CreateSaveFileViewModel()
        {
            FileSaveManagement = new FileSaveManagement(); 
            Clean();



            CreateCommand = new RelayCommand(o =>
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


        public void Clean()
        {

            Title = "";
            SourcePath = "";
            DestinationPath = "";
            TypeComplete = false;
            TypeDifferencial = false;
        }
    }
}
