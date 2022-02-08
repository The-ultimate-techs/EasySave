using EasySave.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class MainViewModel : ObservableObject
    {
        //Relay Command for the different views
        public RelayCommand CreateSafeFileCommand { get; set; }
        public RelayCommand EditDeleteSafeFileCommand { get; set; }
        public RelayCommand RunSaveFileCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }




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
            CurrentView = HomePageVM;

            CreateSafeFileCommand = new RelayCommand(o =>
           {
               CurrentView = CreateSaveFileVM;
           });

            EditDeleteSafeFileCommand = new RelayCommand(o =>
            {
                CurrentView = EditDeleteSaveFileVM;
            });

            RunSaveFileCommand = new RelayCommand(o =>
            {
                CurrentView = RunSaveFileVM;
            });

            SettingsCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });
        }
    }
}
