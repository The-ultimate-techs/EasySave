
using EasySave.MVVM.Model;
using EasySave.MVVM.JsonObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;


namespace EasySave.MVVM.ViewModel
{
    class SettingsViewModel
    {

        


        public string Language { get; set; }
        public static ObservableCollection<string> ExtensionToEncryptlist { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> ExtensionList { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> SoftwarePackageList { get; set; } = new ObservableCollection<string>();

        




        public SettingsViewModel()
        {
           



        }




    }
}
