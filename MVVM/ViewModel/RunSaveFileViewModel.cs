using Caliburn.Micro;
using EasySave.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EasySave.MVVM.ViewModel
{
    class RunSaveFileViewModel
    {

        public BindableCollection<RunningSaveFile> TileList { get; set; } 
        FileSaveManagement FileSaveManagement;

        public RunSaveFileViewModel()
        {
            TileList = new BindableCollection<RunningSaveFile>();
            FileSaveManagement = new FileSaveManagement();
            LoadContent();
        

        }


        public void LoadContent()
        {



            List<FileSave> ListFile = new List<FileSave> { };
            ListFile = FileSaveManagement.GetFilesOnADirectory(FileSaveManagement.GetSaveFileDirectory(), "");

            foreach (FileSave FileSave in ListFile)
            {

                RunningSaveFile newob = new RunningSaveFile();
                newob.Title = FileSave.GetTitle().Replace(".Json", "");
                newob.PlayPause = false;
                newob.progression = 0;
                TileList.Add(newob);


            }




        }



    }


    class RunningSaveFile 
    {
        public string Title { get; set; }
        public bool PlayPause { get; set; }  // if PlayPause == false  ==>  // pause  if PlayPause == true  ==> play  
        public int progression { get; set; } 




            public RelayCommand Test { get; set; }



            public RunningSaveFile()
            {

                Test = new RelayCommand(o =>

                {
                    progression += 1;

                });

            }



        }





}
