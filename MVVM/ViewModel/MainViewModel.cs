using EasySave.MVVM.Model;
using EasySave.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class MainViewModel : ObservableObject
    {


        public object Language { get; set; }
        SettingManager SettingManager ;



        //Relay Command for the different views
        public RelayCommand CreateSaveFileCommand { get; set; }
        public RelayCommand EditDeleteSaveFileCommand { get; set; }
        public RelayCommand HomePageCommand { get; set; }
        public RelayCommand RunSaveFileCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }
        public RelayCommand ChangeLanguage { get; set; }




        //objects déclartion for the views 
        public CreateSaveFileViewModel CreateSaveFileVM { get; set; }
        public EditDeleteSaveFileViewModel EditDeleteSaveFileVM { get; set; }
        public HomePageViewModel HomePageVM { get; set; }
        public RunSaveFileViewModel RunSaveFileVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }


        private object _CurrentView;

        public object CurrentView
        {
            get { return _CurrentView; }
            set { _CurrentView = value;
                OnPropertyChanged(); 
            }
        }




        //Constructor
        public MainViewModel()
        {
            
            CreateSaveFileVM = new CreateSaveFileViewModel();
            EditDeleteSaveFileVM = new EditDeleteSaveFileViewModel();
            HomePageVM = new HomePageViewModel();
            RunSaveFileVM = new RunSaveFileViewModel();
            SettingsVM = new SettingsViewModel();
            SettingManager = new SettingManager();
            CurrentView = HomePageVM;
            reload();
            


            CreateSaveFileCommand = new RelayCommand(o =>
           {
               CurrentView = CreateSaveFileVM;
           });

            EditDeleteSaveFileCommand = new RelayCommand(o =>
            {
                CurrentView = EditDeleteSaveFileVM;
            });

            HomePageCommand = new RelayCommand(o =>
            {
                CurrentView = HomePageVM;
            });

            RunSaveFileCommand = new RelayCommand(o =>
            {
                CurrentView = RunSaveFileVM;
            });

            SettingsCommand = new RelayCommand(o =>
            {

                CurrentView = SettingsVM;
               

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
        }
    }
}
