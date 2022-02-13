using System;
using System.Collections.Generic;
using System.Text;
using EasySave.MVVM.Model;

namespace EasySave.MVVM.ViewModel
{
    class CreateSaveFileViewModel
    {
        FileSaveManagement FileSaveManagement;


        public object Title { get; set; }
        public object SourcePath { get; set; }
        public object DestinationPath { get; set; }
        public bool TypeComplete { get; set; }
        public bool TypeSequential { get; set; }








        public RelayCommand CreateCommand { get; set; }



        public CreateSaveFileViewModel()
        {
            FileSaveManagement = new FileSaveManagement(); 
            Clean();



            CreateCommand = new RelayCommand(o =>
            {

                


            });


        }


        public void Clean()
        {
            Title = "";
            SourcePath = "";
            DestinationPath = "";
            TypeComplete = false;
            TypeSequential = false;
        }
    }
}
