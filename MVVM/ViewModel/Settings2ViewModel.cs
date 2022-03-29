
using EasySave.MVVM.Model;
using EasySave;
using EasySave.MVVM.ObjectsForSerialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Resources;
using System.Reflection;

namespace EasySave.MVVM.ViewModel
{
    class Settings2ViewModel
    {

        SettingManager SettingManager;
     


        public static ObservableCollection<string> FilesPriorityList { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> ExtensionList { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string>TotalListExetensionsSupported { get; set; } = new ObservableCollection<string>() {".AIF",".AU",".AVI",".BAT",".BMP",".JAVA",".CSV",".CVS",".DBF",".DIF",".DOCX",".DOC",".EPS",".EXE",".FM3",".GIF",".HQX",".HTML",".JPEG",".JPG" ,".MAC",".MAP",".MDB",".MID",".MOV",".MTB",".PDF",".P65",".T65",".PNG",".PPTX",".PPT" ,".PSD",".PSP",".QXD",".RA",".RTF",".SIT",".TAR",".TIF",".TXT",".WAV",".WK3",".WKS",".WPD",".XLS",".ZIP",".RAR"};

        public object ExtensionSelected { get; set; }
        public object ExtensionFilesPriority { get; set; }


        public RelayCommand Add3Command { get; set; }
        public RelayCommand Remove3Command { get; set; }


        public RelayCommand TwoSelectedCommand { get; set; }
        public RelayCommand ThreeSelectedCommand { get; set; }
        public RelayCommand FourSelectedCommand { get; set; }

        public bool TwoSelected { get; set; }
        public bool ThreeSelected { get; set; }
        public bool FourSelected { get; set; }








        public Settings2ViewModel()
        {
            

            SettingManager = new SettingManager();
            Reload();

            Add3Command = new RelayCommand(o =>
            {

                if (ExtensionSelected != null)
                {
                    FilesPriorityList.Add(ExtensionSelected.ToString());
                    ExtensionList.Remove(ExtensionSelected.ToString());
                    SettingManager.SetSettings2(FilesPriorityList, SettingManager.Getsettings().ThresholdLimit);
                }


            });

            Remove3Command = new RelayCommand(o =>
            {
                if (ExtensionFilesPriority != null)
                {
                    ExtensionList.Add(ExtensionFilesPriority.ToString());
                    FilesPriorityList.Remove(ExtensionFilesPriority.ToString());
                    SettingManager.SetSettings2(FilesPriorityList, SettingManager.Getsettings().ThresholdLimit);
                }
            });

            TwoSelectedCommand = new RelayCommand(o =>
            {

                SettingManager.SetSettings2(FilesPriorityList,2);

            });

            ThreeSelectedCommand = new RelayCommand(o =>
            {

                SettingManager.SetSettings2(FilesPriorityList, 3);

            });

            FourSelectedCommand = new RelayCommand(o =>
            {

                SettingManager.SetSettings2(FilesPriorityList, 4);

            });

        }

        public void Reload()
        {

            FilesPriorityList.Clear();
            ExtensionList.Clear();

            foreach (string element in SettingManager.Getsettings().FilesPriorityList)
            {
                FilesPriorityList.Add(element);
            }

            foreach (string Extension in TotalListExetensionsSupported)
            {

                ExtensionList.Add(Extension);

            }


            foreach (string ElementPriority in SettingManager.Getsettings().FilesPriorityList)
            {

                ExtensionList.Remove(ElementPriority);

            }

            switch (SettingManager.Getsettings().ThresholdLimit)
            {
                case 2:
                    TwoSelected = true;
                    ThreeSelected = false;
                    FourSelected = false;
                    break;

                case 3:
                    TwoSelected = false;
                    ThreeSelected = true;
                    FourSelected = false;
                    break;

                case 4:
                    TwoSelected = false;
                    ThreeSelected = false;
                    FourSelected = true;
                    break;
            }


          


        }





     
    
    
    
    }




    }

