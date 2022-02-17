using Caliburn.Micro;
using EasySave.MVVM.Model;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Data;

namespace EasySave.MVVM.ViewModel
{
    class RunSaveFileViewModel
    {

        public ObservableCollection<RunningSaveFile> TileList { get; set; } 
        FileSaveManagement FileSaveManagement;





        public RelayCommand Test { get; set; }

        public RunSaveFileViewModel()
        {
            TileList = new ObservableCollection<RunningSaveFile>();
            FileSaveManagement = new FileSaveManagement();
            LoadContent();





            Test = new RelayCommand(o =>

            {
                foreach (RunningSaveFile SaveFile in TileList)
                {
                    if (o == SaveFile)
                    {
                        SaveFile.progression += 1;
                       

                    }
                }
                CollectionViewSource.GetDefaultView(TileList).Refresh();



            });

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




            



            public RunningSaveFile()
            {



            }



        }





}
