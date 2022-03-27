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
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;

namespace EasySave.MVVM.ViewModel
{
    class RunSaveFileViewModel
    {

        public ObservableCollection<RunningSaveFile>TileList { get; set; }
        public RunningSaveFile Filetorun { get; set; }
        


        FileSaveManagement FileSaveManagement;
        LogManagement LogManagement;
        SettingManager SettingManager;
        SocketHandler SocketHandler = SocketHandler.Instance;


        public RelayCommand PlayCommand { get; set; }
        public RelayCommand PauseCommand { get; set; }
        public RelayCommand StopCommand { get; set; }
        public RelayCommand StopAllCommand { get; set; }







        public RunSaveFileViewModel()
        {

            TileList = new ObservableCollection<RunningSaveFile>();
            FileSaveManagement = new FileSaveManagement();
            LogManagement = new LogManagement();
            SettingManager = new SettingManager();
            Filetorun = new RunningSaveFile();
            
            LoadContent2();



            Thread RefreshThread = new Thread(refreshrate);
            RefreshThread.Name = "refreshrate";
            RefreshThread.Start();

            SocketHandler.Receive = true;
            Thread RemoteIHMThread = new Thread(RemoteIHMCommandHandler);
            RemoteIHMThread.Name = "RemoteIHMThread";
            RemoteIHMThread.Start();


            SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);



            PlayCommand = new RelayCommand(o =>

            {
               
                foreach (RunningSaveFile SaveFile in TileList)
                {
                    if (o == SaveFile)
                    {


                        if (SaveFile.CurrentAction == "READY")
                        {
                            SaveFile.PauseState = "Visible";
                            SaveFile.PlayState = "Hidden";
                            SaveFile.StopState = false;
                            SaveFile.StopButton = false;
                            SaveFile.CurrentAction = "RUNNING";
                            Filetorun = SaveFile;
                        
                            Thread CopyThread = new Thread(StartSavefile);
                            CopyThread.Name = SaveFile.Title;
                            CopyThread.Start();

                        }
                        
                    }
                }
               

            });




            PauseCommand = new RelayCommand(o =>

            {
                
                foreach (RunningSaveFile SaveFile in TileList)
                {
                    if (o == SaveFile)
                    {

                        if (SaveFile.CurrentAction == "RUNNING")
                        { 
                        
                        
                            SaveFile.CurrentAction = "PAUSED";
                            SaveFile.progressionBuffer = SaveFile.progression;
                            SaveFile.StopState = true;
                            SaveFile.StopButton = true;
                            SaveFile.PauseState = "Hidden";
                            SaveFile.PlayState = "Hidden";
                            SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);

                        }






                    }
                }




            });



            StopCommand = new RelayCommand(o =>

            {

                foreach (RunningSaveFile SaveFile in TileList)
                {
                    if (o == SaveFile)
                    {

                        if (SaveFile.CurrentAction == "READY")
                        {
                            SaveFile.CurrentAction = "STOPPED";
                            SaveFile.StopState = true;
                            SaveFile.StopButton = false;
                            SaveFile.PauseState = "Hidden";
                            SaveFile.PlayState = "Visible";
                            SaveFile.progression = 0;
                            SaveFile.TotalFile = 100;
                            SaveFile.progressionBuffer = 0;




                            Filetorun = SaveFile;

                            Thread StopThread = new Thread(StopCopy);
                            StopThread.Name = SaveFile.Title;
                            StopThread.Start();
                            SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);
                        }
                    }
                }




            });








            StopAllCommand = new RelayCommand(o =>

            {

                foreach (RunningSaveFile SaveFile in TileList)
                {
                    if (SaveFile.CurrentAction == "READY")
                    {
                        SaveFile.CurrentAction = "STOPPED";
                        SaveFile.StopState = true;
                        SaveFile.StopButton = false;
                        SaveFile.PauseState = "Hidden";
                        SaveFile.PlayState = "Visible";
                        SaveFile.progression = 0;
                        SaveFile.TotalFile = 100;
                        SaveFile.progressionBuffer = 0;

                        Filetorun = SaveFile;
                        StopCopy();
                        SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);
                    }

                }




            });


        }


        public void RemoteIHMCommandHandler()
        {



            while (true)
            {
                if (SocketHandler.Sending == false)
                {
                    break;
                }



                if (SocketHandler.ReceiveData() != null)
                {

                    RunningSaveFile RunningSaveFile = JsonConvert.DeserializeObject<RunningSaveFile>(SocketHandler.ReceiveData());
                    SocketHandler.Receive = false;

                    switch (RunningSaveFile.CurrentAction)

                    {
                        case "REMOTEPLAYED":

                            foreach (RunningSaveFile SaveFile in TileList)
                            {
                                if (RunningSaveFile.Title == SaveFile.Title)
                                {


                                    if (SaveFile.CurrentAction == "READY")
                                    {
                                        SaveFile.PauseState = "Visible";
                                        SaveFile.PlayState = "Hidden";
                                        SaveFile.StopState = false;
                                        SaveFile.StopButton = false;
                                        SaveFile.CurrentAction = "RUNNING";
                                        Filetorun = SaveFile;

                                        Thread CopyThread = new Thread(StartSavefile);
                                        CopyThread.Name = SaveFile.Title;
                                        CopyThread.Start();

                                    }
                                }
                            }
                            Thread.Sleep(1500);
                            SocketHandler.Receive = true;
                            break;

                        case "REMOTEPAUSED":

                            foreach (RunningSaveFile SaveFile in TileList)
                            {
                                if (RunningSaveFile.Title == SaveFile.Title)
                                {

                                    if (SaveFile.CurrentAction == "RUNNING")
                                    {


                                        SaveFile.CurrentAction = "PAUSED";
                                        SaveFile.progressionBuffer = SaveFile.progression;
                                        SaveFile.StopState = true;
                                        SaveFile.StopButton = true;
                                        SaveFile.PauseState = "Hidden";
                                        SaveFile.PlayState = "Hidden";
                                        SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);

                                    }






                                }
                            }
                            Thread.Sleep(1500);
                            SocketHandler.Receive = true;
                            break;

                        case "REMOTESTOPPED":

                            foreach (RunningSaveFile SaveFile in TileList)
                            {
                                if (RunningSaveFile.Title == SaveFile.Title)
                                {
                                    if (SaveFile.CurrentAction == "READY")
                                    {
                                        SaveFile.CurrentAction = "STOPPED";
                                        SaveFile.StopState = true;
                                        SaveFile.StopButton = false;
                                        SaveFile.PauseState = "Hidden";
                                        SaveFile.PlayState = "Visible";
                                        SaveFile.progression = 0;
                                        SaveFile.TotalFile = 100;
                                        SaveFile.progressionBuffer = 0;




                                        Filetorun = SaveFile;

                                        Thread StopThread = new Thread(StopCopy);
                                        StopThread.Name = SaveFile.Title;
                                        StopThread.Start();
                                        SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);
                                    }

                                }
                            }

                            Thread.Sleep(1500);
                            SocketHandler.Receive = true;
                            break;
                    }
                }


                    

                




            }


        }






        public void StartSavefile()
        {

            RunningSaveFile Filetoprocess = new RunningSaveFile();
            Filetoprocess = Filetorun;

            ObservableCollection<RunningSaveFile> TileListSaved = new ObservableCollection<RunningSaveFile>();
            TileListSaved = TileList;


            string Jsonpath = FileSaveManagement.GetSaveFileDirectory() + Filetoprocess.Title +".Json";

            string myJsonFile = File.ReadAllText(Jsonpath);
            SaveFileJson SaveFileJson = JsonConvert.DeserializeObject<SaveFileJson>(myJsonFile);



            List<DirectorySave> DirectoryList = FileSaveManagement.GetDirectoriesOnADirectory(SaveFileJson.SourcePath, SaveFileJson.DestPath);

            List<FileSave> FileList = FileSaveManagement.GetFilesOnADirectory(SaveFileJson.SourcePath, SaveFileJson.DestPath);


            Settingjson Settingjson = new Settingjson();
            Settingjson = SettingManager.Getsettings();

            LogManagement DailyLogManagement = new LogManagement();


            foreach (DirectorySave Directory in DirectoryList)
            {

                FileSaveManagement.CopyDirectories(Directory.SourceDirectory, Directory.DestinationDirectory);

            }

           
            long filesize = 0;

            foreach (FileSave files in FileList)
            {


                filesize = filesize + files.FileSize(files.GetSourceDirectory());

            }



            int progress = 0;
            

            foreach (FileSave files in FileList)
            {

                progress = progress + 1;

        

                foreach (RunningSaveFile SaveFile in TileListSaved)


                {

                    while (IsProcessRunning(Settingjson) == true)
                    {

                        Thread.Sleep(200);

                    }


                    bool process = false;
                    
                   
                    if (Filetoprocess == SaveFile && SaveFile.StopState == false && SaveFile.progressionBuffer < progress)
                    {

                                            
                        System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.progression = progress);
                        System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.TotalFile = FileList.Count);


                        DailyLogManagement.BeginSaveFileExecution();


                        foreach (string extension in Settingjson.ExtensionToEncryptlist)
                        {
                            if (extension.ToLower() == Path.GetExtension(files.GetSourceDirectory().ToLower()))
                            {

                                ProcessStartInfo startInfo = new ProcessStartInfo(@"../../../Cryptosoft/Cryptosoft.exe");        // exe file
                               

                                //here you add your arguments
                                startInfo.ArgumentList.Add(files.GetSourceDirectory());       // First argument          
                                startInfo.ArgumentList.Add(files.GetDestinationDirectory());       // second argument

                                Process Process = Process.Start(startInfo);

                                Process.WaitForExit();
                                int result = Process.ExitCode;



                                DailyLogManagement.EndSaveFileExecution();
                                DailyLogManagement.DailyLogGénérator(SaveFileJson.Title, files.GetSourceDirectory(), files.GetDestinationDirectory(), files.GetType_());
                            }
                            else
                            {
                                FileSaveManagement.CreateSaveFile(files.GetTitle(), files.GetSourceDirectory(), files.GetDestinationDirectory(), SaveFileJson.Type);
                                DailyLogManagement.EndSaveFileExecution();
                                DailyLogManagement.DailyLogGénérator(SaveFileJson.Title, files.GetSourceDirectory(), files.GetDestinationDirectory(), files.GetType_());
                            }

                        }




                        if (Monitor.TryEnter(LogManagement, 3600000))
                        {
                            process = true;
                            LogManagement.RunningLogGénérator(SaveFileJson.Title, files.GetSourceDirectory(), files.GetDestinationDirectory(), FileList.Count, filesize, FileList.Count - progress,true);
                        }

                        if (process == true)
                        {

                            Monitor.Exit(LogManagement);
                        }

                        SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);

                    }





                }



                


            }

         


            foreach (RunningSaveFile SaveFile in TileListSaved)
            {
                if (Filetoprocess == SaveFile && SaveFile.StopState == false && SaveFile.progression == FileList.Count)
                {



                    bool process = false;

                    if (Monitor.TryEnter(LogManagement, 3600000))
                    {
                        process = true;
                        LogManagement.RunningLogGénérator(SaveFileJson.Title, "", "", FileList.Count, 0, 0, false);
                    }

                    if (process == true)
                    {

                        Monitor.Exit(LogManagement);
                    }


                   



                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.PauseState = "Hidden");
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.PlayState = "Visible");
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.StopState = false);
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.TotalFile = FileList.Count);
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.progression = FileList.Count);
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.progressionBuffer = 0);

                    SaveFile.CurrentAction = "READY";
                    for (int i =0; i < 20; i++)
                    {
                        Thread.Sleep(25);
                        SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);
                    }

                }

                if (SaveFile.CurrentAction == "PAUSED")
                {

                
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.StopButton = true);
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.PauseState = "Hidden");
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(() => SaveFile.PlayState = "Visible");

                    for (int i =0; i < 20; i++)
                    {
                        Thread.Sleep(25);
                        SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);
                    }
                    
                    SaveFile.CurrentAction = "READY";
                    break;
                }


            }



        
        }


        public void StopCopy()
        {

            RunningSaveFile Filetoprocess = new RunningSaveFile();
            Filetoprocess = Filetorun;


            string Jsonpath = FileSaveManagement.GetSaveFileDirectory() + Filetoprocess.Title + ".Json";

            string myJsonFile = File.ReadAllText(Jsonpath);
            SaveFileJson SaveFileJson = JsonConvert.DeserializeObject<SaveFileJson>(myJsonFile);


            List<FileSave> FileList = FileSaveManagement.GetFilesOnADirectory(SaveFileJson.SourcePath, SaveFileJson.DestPath);


            foreach (RunningSaveFile SaveFile in TileList)
            {
                if (Filetoprocess == SaveFile && SaveFile.StopState == true )
                {



                    bool process = false;

                    if (Monitor.TryEnter(LogManagement, 3600000))
                    {
                        process = true;
                        LogManagement.RunningLogGénérator(SaveFileJson.Title, "", "", FileList.Count, 0, FileList.Count, false);
                    }

                    if (process == true)
                    {

                        Monitor.Exit(LogManagement);
                    }

                }
                SocketHandler.Data2Send = JsonConvert.SerializeObject(TileList);
                SaveFile.CurrentAction = "READY";
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
                Thread.Sleep(500);
                                               
                System.Windows.Application.Current.Dispatcher.BeginInvoke(() => Refresh());

                if (SocketHandler.Sending == false)
                {
                    break;
                }
            }
        }



        public void LoadContent2()
        {
            LogManagement RefreshUI = new LogManagement();
            
   
            TileList.Clear();
            foreach (RunningLog RunningLog in RefreshUI.LogReader())
            {
                if (RunningLog.State != "DELETED")
                {

                RunningSaveFile newob = new RunningSaveFile();
                newob.Title = RunningLog.Name;
                newob.progressionBuffer = RunningLog.TotalFilesToCopy - RunningLog.TotalFilesToCopy;
                newob.progression = RunningLog.TotalFilesToCopy - RunningLog.TotalFilesToCopy;
                newob.TotalFile = Convert.ToInt64(RunningLog.TotalFilesToCopy);
                newob.PauseState = (RunningLog.State == "ACTIVE") ? "Visible" : "Hidden";
                newob.PlayState = (RunningLog.State == "ACTIVE") ? "Hidden" : "Visible";
                newob.StopState = false;
                newob.StopButton = (RunningLog.State == "ACTIVE") ? true : false;
                newob.CurrentAction = "READY";

                TileList.Add(newob);


                }
              
            }


            Refresh();

            
        }


        public bool IsProcessRunning(Settingjson Settingjson)
        {
            bool IsprocessRunning = false;

            foreach (string ProcessPath in Settingjson.SoftwarePackageList)
            {
                

                string fileName = Path.GetFileName(ProcessPath);
                // Get the precess that already running as per the exe file name.
                Process[] processName = Process.GetProcessesByName(fileName.Substring(0, fileName.LastIndexOf('.')));
                if (processName.Length > 0)
                {
                    IsprocessRunning = true;
                }




            }


            return IsprocessRunning;
        }


    }


    class RunningSaveFile 
    {
        public string Title { get; set; }
        public int progressionBuffer { get; set; }
        public int progression { get; set; }
        public long TotalFile { get; set; }
        public object PauseState { get; set; }
        public object PlayState { get; set; }
        public bool StopState { get; set; }// if true save is running 
        public bool StopButton { get; set; } // if false button is not clickable
        public string CurrentAction { get; set; }








        public RunningSaveFile()
        {



        }



        }





}
