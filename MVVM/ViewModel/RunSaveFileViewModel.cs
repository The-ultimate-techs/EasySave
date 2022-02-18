using Caliburn.Micro;
using EasySave.MVVM.Model;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Data;
using EasySave.MVVM.ObjectsForSerialization;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Threading;

namespace EasySave.MVVM.ViewModel
{
    class RunSaveFileViewModel
    {

        public ObservableCollection<RunningSaveFile> TileList { get; set; }
        public RunningSaveFile Filetorun { get; set; }

      


        FileSaveManagement FileSaveManagement;
        LogManagement LogManagement; 






        public RelayCommand PlayCommand { get; set; }
        public RelayCommand PauseCommand { get; set; }
        public RelayCommand StopCommand { get; set; }




      





        


        public RunSaveFileViewModel()
        {
            TileList = new ObservableCollection<RunningSaveFile>();
            FileSaveManagement = new FileSaveManagement();
            LogManagement = new LogManagement();
            Filetorun = new RunningSaveFile();
            LoadContent();
            Thread RefreshThread = new Thread(refreshrate);
            RefreshThread.Name = "refreshrate";
            RefreshThread.Start();
;
            

            PlayCommand = new RelayCommand(o =>

            {
                foreach (RunningSaveFile SaveFile in TileList)
                {
                    if (o == SaveFile)
                    {
                        SaveFile.PauseState = "Visible";
                        SaveFile.PlayState = "Hidden";
                        SaveFile.StopState = false;
                        SaveFile.StopButton = true;
                        Filetorun = SaveFile;
                
                        Thread CopyThread = new Thread(StartSavefile);
                        CopyThread.Name = SaveFile.Title;
                        CopyThread.Start();
                        
                    }
                }
               

            });




            PauseCommand = new RelayCommand(o =>

            {

                foreach (RunningSaveFile SaveFile in TileList)
                {
                    if (o == SaveFile)
                    {
                        SaveFile.PauseState = "Hidden";
                        SaveFile.PlayState = "Visible";
                        SaveFile.progressionBuffer = SaveFile.progression;
                        SaveFile.StopState = true;
                        SaveFile.StopButton = true;

                    }
                }




            });



            StopCommand = new RelayCommand(o =>

            {

                foreach (RunningSaveFile SaveFile in TileList)
                {
                    if (o == SaveFile)
                    {
                        SaveFile.PauseState = "Hidden";
                        SaveFile.PlayState = "Visible";
                        SaveFile.StopState = true;
                        SaveFile.StopButton = false;
                        SaveFile.progression = 0;
                        SaveFile.TotalFile = 100;
                        SaveFile.progressionBuffer = 0;
                    }
                }




            });





        }


        public void StartSavefile()
        {

            RunningSaveFile Filetoprocess = new RunningSaveFile();
            Filetoprocess = Filetorun;


            string Jsonpath = FileSaveManagement.GetSaveFileDirectory() + Filetoprocess.Title +".Json";

            string myJsonFile = File.ReadAllText(Jsonpath);
            SaveFileJson SaveFileJson = JsonConvert.DeserializeObject<SaveFileJson>(myJsonFile);



            List<DirectorySave> DirectoryList = FileSaveManagement.GetDirectoriesOnADirectory(SaveFileJson.SourcePath, SaveFileJson.DestPath);

            List<FileSave> FileList = FileSaveManagement.GetFilesOnADirectory(SaveFileJson.SourcePath, SaveFileJson.DestPath);



            foreach (DirectorySave Directory in DirectoryList)
            {

                FileSaveManagement.CopyDirectories(Directory.SourceDirectory, Directory.DestinationDirectory);

            }

            LogManagement.BeginEndProcess();

            long filesize = 0;

            foreach (FileSave files in FileList)
            {


                filesize = filesize + files.FileSize(files.GetSourceDirectory());

            }



            int progress = 0;
            

            foreach (FileSave files in FileList)
            {

                progress = progress + 1;
             

                foreach (RunningSaveFile SaveFile in TileList)
                {
                    if (Filetoprocess == SaveFile && SaveFile.StopState == false && SaveFile.progressionBuffer < progress)
                    {
                       

                        System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.progression = progress);
                        System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.TotalFile = FileList.Count);

                       
                        LogManagement.BeginSaveFileExecution();
                        FileSaveManagement.CreateSaveFile(files.GetTitle(), files.GetSourceDirectory(), files.GetDestinationDirectory(), SaveFileJson.Type);
                        LogManagement.EndSaveFileExecution();

                        LogManagement.DailyLogGénérator(SaveFileJson.Title, files.GetSourceDirectory(), files.GetDestinationDirectory(), files.GetType_());
                        LogManagement.RunningLogGénérator(SaveFileJson.Title, files.GetSourceDirectory(), files.GetDestinationDirectory(), FileList.Count, filesize, FileList.Count - progress);

                        

                    }

                }

                  
            }

         


            foreach (RunningSaveFile SaveFile in TileList)
            {
                if (Filetoprocess == SaveFile && SaveFile.StopState == false)
                {

                    LogManagement.BeginEndProcess();
                    LogManagement.RunningLogGénérator(SaveFileJson.Title, "", "", FileList.Count, 0, 0);



                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.PauseState = "Hidden");
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.PlayState = "Visible");
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.StopState = false);
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.TotalFile = FileList.Count);
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.progression = FileList.Count);
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.progressionBuffer = 0);

                }

            }



        
        }


        public void Refresh()
        {
            
            
                CollectionViewSource.GetDefaultView(TileList).Refresh();
            
        }


        public void refreshrate()
        {
               while (true)
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(() => Refresh());
                Thread.Sleep(300);
            }
        }
      




        public void LoadContent()
        {



            List<FileSave> ListFile = new List<FileSave> { };
            ListFile = FileSaveManagement.GetFilesOnADirectory(FileSaveManagement.GetSaveFileDirectory(), "");

            foreach (FileSave FileSave in ListFile)
            {

                RunningSaveFile newob = new RunningSaveFile();
                newob.Title = FileSave.GetTitle().Replace(".Json", "");
                newob.progressionBuffer = 0;
                newob.progression = 0;
                newob.TotalFile = 100;
                newob.PauseState = "Hidden";
                newob.PlayState = "Visible";
                newob.StopState = false;
                newob.StopButton = false;
                TileList.Add(newob);
               
                


            }




        }



    }


    class RunningSaveFile 
    {
        public string Title { get; set; }
        public int progressionBuffer { get; set; }
        public int progression { get; set; }
        public int TotalFile { get; set; }
        public object PauseState { get; set; }
        public object PlayState { get; set; }
        public bool StopState { get; set; }// if true save is running 
        public bool StopButton { get; set; }

  





        public RunningSaveFile()
            {



            }



        }





}
