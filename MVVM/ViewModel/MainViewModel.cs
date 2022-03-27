using EasySave.MVVM.Model;
using EasySave.MVVM.ObjectsForSerialization;
using EasySave.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Data;

namespace EasySave
{
    class MainViewModel : ObservableObject
    {


        public object Language { get; set; }
        public string BufferTitle { get; set; }
        SettingManager SettingManager;
        SocketHandler SocketHandler = SocketHandler.Instance;



        LogManagement LogManagement = new LogManagement();
        List<RunningLog> ListRunningLog = new List<RunningLog>();

        //Relay Command for the different views
        public RelayCommand CreateSaveFileCommand { get; set; }
        public RelayCommand DeleteSaveFileCommand { get; set; }
        public RelayCommand HomePageCommand { get; set; }
        public RelayCommand RunSaveFileCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }
        public RelayCommand ChangeLanguage { get; set; }
        public RelayCommand EditSaveFileCommand { get; set; }
        public RelayCommand SettingsCommand2 { get; set; }
        public RelayCommand CloseCommand { get; set; }




        //objects déclartion for the views 
        public CreateSaveFileViewModel CreateSaveFileVM { get; set; }
        public DeleteSaveFileViewModel DeleteSaveFileVM { get; set; }
        public EditSaveFileViewModel EditSaveFileVM { get; set; }
        public HomePageViewModel HomePageVM { get; set; }
        public RunSaveFileViewModel RunSaveFileVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public Settings2ViewModel Settings2VM { get; set; }




        private object _CurrentView;

        public object CurrentView
        {
            get { return _CurrentView; }
            set
            {
                _CurrentView = value;
                OnPropertyChanged();
            }
        }




        //Constructor
        public MainViewModel()
        {








            CreateSaveFileVM = new CreateSaveFileViewModel();
            DeleteSaveFileVM = new DeleteSaveFileViewModel();
            HomePageVM = new HomePageViewModel();
            RunSaveFileVM = new RunSaveFileViewModel();
            SettingsVM = new SettingsViewModel();
            EditSaveFileVM = new EditSaveFileViewModel();
            SettingManager = new SettingManager();
            Settings2VM = new Settings2ViewModel();
            CurrentView = HomePageVM;
            reload();
            SocketHandler.Sending = false;



            CreateSaveFileCommand = new RelayCommand(o =>
            {

                ListRunningLog = LogManagement.LogReader();

                bool Iscopyrunning = false; 

                foreach (RunningLog RunningLog in ListRunningLog)
                {
                    if (RunningLog.State == "ACTIVE")

                    {
                        Iscopyrunning = true;
                        

                    }
                }


                if (Iscopyrunning == false)
                {
                    if (CurrentView == RunSaveFileVM)
                    {
                       
                        SocketHandler.Sending = false;
                    }
                    CurrentView = CreateSaveFileVM;
                }


            });

            DeleteSaveFileCommand = new RelayCommand(o =>
            {



                ListRunningLog = LogManagement.LogReader();

                bool Iscopyrunning = false;

                foreach (RunningLog RunningLog in ListRunningLog)
                {
                    if (RunningLog.State == "ACTIVE")

                    {
                        Iscopyrunning = true;


                    }
                }


                if (Iscopyrunning == false)
                {
                    if (CurrentView == RunSaveFileVM)
                    {
                        SocketHandler.Sending = false;
                    }
                    if (o == null)
                    {
                        EditSaveFileVM.Title = null;
                        CurrentView = DeleteSaveFileVM;
                        BufferTitle = null;
                    }
                    else
                    {
                        RunningSaveFile RunningSaveFile = new RunningSaveFile();
                        RunningSaveFile = o as RunningSaveFile;
                        BufferTitle = RunningSaveFile.Title;

                        CurrentView = DeleteSaveFileVM;


                    }
                  
                }





            });

            EditSaveFileCommand = new RelayCommand(o =>
            {

                ListRunningLog = LogManagement.LogReader();

                bool Iscopyrunning = false;

                foreach (RunningLog RunningLog in ListRunningLog)
                {
                    if (RunningLog.State == "ACTIVE")

                    {
                        Iscopyrunning = true;


                    }
                }


                if (Iscopyrunning == false)
                {
                    if (CurrentView == RunSaveFileVM)
                    {
                        SocketHandler.Sending = false;
                    }

                    if (o == null)
                    {
                        EditSaveFileVM.Title = null;
                        CurrentView = EditSaveFileVM;
                        BufferTitle = null;
                    }
                    else
                    {
                        RunningSaveFile RunningSaveFile = new RunningSaveFile();
                        RunningSaveFile = o as RunningSaveFile;
                        BufferTitle = RunningSaveFile.Title;

                        CurrentView = EditSaveFileVM;


                    }
                }






            });

            HomePageCommand = new RelayCommand(o =>

            {

                ListRunningLog = LogManagement.LogReader();

                bool Iscopyrunning = false;

                foreach (RunningLog RunningLog in ListRunningLog)
                {
                    if (RunningLog.State == "ACTIVE")

                    {
                        Iscopyrunning = true;


                    }
                }


                if (Iscopyrunning == false)
                {
                    if (CurrentView == RunSaveFileVM)
                    {
                        SocketHandler.Sending = false;
                    }
                    CurrentView = HomePageVM;

                }
            });

            RunSaveFileCommand = new RelayCommand(o =>
            {
                ListRunningLog = LogManagement.LogReader();

                bool Iscopyrunning = false;

                foreach (RunningLog RunningLog in ListRunningLog)
                {
                    if (RunningLog.State == "ACTIVE")

                    {
                        Iscopyrunning = true;


                    }
                }


                if (Iscopyrunning == false)
                {
                    SocketHandler.Sending = true;
                    CurrentView = RunSaveFileVM;
                
                }
            });

            SettingsCommand = new RelayCommand(o =>
            {

                ListRunningLog = LogManagement.LogReader();

                bool Iscopyrunning = false;

                foreach (RunningLog RunningLog in ListRunningLog)
                {
                    if (RunningLog.State == "ACTIVE")

                    {
                        Iscopyrunning = true;


                    }
                }


                if (Iscopyrunning == false)
                {
                    if (CurrentView == RunSaveFileVM)
                    {
                        SocketHandler.Sending = false;
                    }
                    CurrentView = SettingsVM;

                }


            });
            
            SettingsCommand2 = new RelayCommand(o =>
            {

                CurrentView = Settings2VM;


            });


            CloseCommand = new RelayCommand(o =>
            {



                ListRunningLog = LogManagement.LogReader();

                bool Iscopyrunning = false;

                foreach (RunningLog RunningLog in ListRunningLog)
                {
                    if (RunningLog.State == "ACTIVE")

                    {
                        Iscopyrunning = true;


                    }
                }


                if (Iscopyrunning == false)
                {
                    SocketHandler.Close();
                    if (CurrentView == RunSaveFileVM)
                    {
                        
                        SocketHandler.Sending = false;
                    }
                    Environment.Exit(0);

                }
            });


            ChangeLanguage = new RelayCommand(o =>
            {

                if (Language.ToString() == "en-US")
                {
                    SettingManager.SetLanguage("fr-FR");
                }

                if (Language.ToString() == "fr-FR")
                {
                    SettingManager.SetLanguage("en-US");
                }


                reload();
            });
        }
        public void reload()
        {
            Language = SettingManager.Getsettings().Language;
            LoadStringResource(Language.ToString());




        }

        private void LoadStringResource(string locale)
        {
            var resources = new ResourceDictionary();

            resources.Source = new Uri("pack://application:,,,/Languages/Language_" + locale + ".xaml", UriKind.Absolute);

            var current = Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                             m => m.Source.OriginalString.EndsWith("Strings.xaml"));


            if (current != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(current);
            }

            Application.Current.Resources.MergedDictionaries.Add(resources);
        }

    }
}
