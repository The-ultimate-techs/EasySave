using EasySave.MVVM.Model;
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

namespace EasySave
{
    class MainViewModel : ObservableObject
    {


        public object Language { get; set; }
        SettingManager SettingManager ;
      


        //Relay Command for the different views
        public RelayCommand CreateSaveFileCommand { get; set; }
        public RelayCommand DeleteSaveFileCommand { get; set; }
        public RelayCommand HomePageCommand { get; set; }
        public RelayCommand RunSaveFileCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }
        public RelayCommand ChangeLanguage { get; set; }
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
            SettingManager = new SettingManager();
            EditSaveFileVM = new EditSaveFileViewModel();
            CurrentView = HomePageVM;
            reload();
            


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
