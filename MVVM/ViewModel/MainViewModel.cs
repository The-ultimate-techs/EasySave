﻿using EasySave.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class MainViewModel : ObservableObject
    {
        //Relay Command for the different views
        public RelayCommand CreateSaveFileCommand { get; set; }
        public RelayCommand DeleteSaveFileCommand { get; set; }
        public RelayCommand HomePageCommand { get; set; }
        public RelayCommand RunSaveFileCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }
        public RelayCommand EditSaveFileCommand { get; set; }




        //objects déclartion for the views 
        public CreateSaveFileViewModel CreateSaveFileVM { get; set; }
        public DeleteSaveFileViewModel DeleteSaveFileVM { get; set; }
        public EditSaveFileViewModel EditSaveFileVM { get; set; }
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
            DeleteSaveFileVM = new DeleteSaveFileViewModel();
            HomePageVM = new HomePageViewModel();
            RunSaveFileVM = new RunSaveFileViewModel();
            SettingsVM = new SettingsViewModel();
            EditSaveFileVM = new EditSaveFileViewModel();
            CurrentView = HomePageVM;



            CreateSaveFileCommand = new RelayCommand(o =>
           {
               CurrentView = CreateSaveFileVM;
           });

            DeleteSaveFileCommand = new RelayCommand(o =>
            {
                CurrentView = DeleteSaveFileVM;
            });

            EditSaveFileCommand = new RelayCommand(o =>
            {
                CurrentView = EditSaveFileVM;
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
        }
    }
}
