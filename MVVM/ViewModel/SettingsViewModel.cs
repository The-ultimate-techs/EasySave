
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

        SettingManager SettingManager;


        public string Language { get; set; }
        public static ObservableCollection<string> ExtensionToEncryptlist { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> ExtensionList { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> SoftwarePackageList { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string>TotalListExetensionsSupported { get; set; } = new ObservableCollection<string>() {".AIF",".AU",".AVI",".BAT",".BMP",".JAVA",".CSV",".CVS",".DBF",".DIF",".DOCX",".DOC",".EPS",".EXE",".FM3",".GIF",".HQX",".HTML",".JPEG",".JPG" ,".MAC",".MAP",".MDB",".MID",".MOV",".MTB",".PDF",".P65",".T65",".PNG",".PPTX",".PPT" ,".PSD",".PSP",".QXD",".RA",".RTF",".SIT",".TAR",".TIF",".TXT",".WAV",".WK3",".WKS",".WPD",".XLS",".ZIP",".RAR"};

        public object ExtensionSelected { get; set; }
        public object ExtensionToEncryptSelected { get; set; }
        public string SoftwarePath { get; set; }
        public object SoftwareToRemove { get; set; }



        public RelayCommand Add1Command { get; set; }
        public RelayCommand Remove1Command { get; set; }
        public RelayCommand Add2Command { get; set; }
        public RelayCommand Remove2Command { get; set; }
     











        public SettingsViewModel()
        {

            SettingManager = new SettingManager();
            Reload();

            Add1Command = new RelayCommand(o =>
            {

                if (ExtensionSelected != null)
                {
                    ExtensionToEncryptlist.Add(ExtensionSelected.ToString());
                    ExtensionList.Remove(ExtensionSelected.ToString());
                    SettingManager.SetSettings(ExtensionToEncryptlist, SoftwarePackageList);
                }


            });

            Remove1Command = new RelayCommand(o =>
            {
                if (ExtensionToEncryptSelected != null)
                {
                    ExtensionList.Add(ExtensionToEncryptSelected.ToString());
                    ExtensionToEncryptlist.Remove(ExtensionToEncryptSelected.ToString());
                    SettingManager.SetSettings(ExtensionToEncryptlist, SoftwarePackageList);
                }
            });

            Add2Command = new RelayCommand(o =>
            {
                if (SoftwarePath != null && SoftwarePath.Contains(".exe") == true)
                {

                    bool Isthere = false; 

                    foreach (string path in SoftwarePackageList)
                    {
                        if (SoftwarePath == path)
                        {
                            Isthere = true;
                        }
                    }


                    if (Isthere == false)
                    {
                        SoftwarePackageList.Add(SoftwarePath.ToString());
                        SettingManager.SetSettings(ExtensionToEncryptlist, SoftwarePackageList);
                    }
                }

            });

            Remove2Command = new RelayCommand(o =>
            {
                if (SoftwareToRemove != null)
                {
                    SoftwarePackageList.Remove(SoftwareToRemove.ToString());
                    SettingManager.SetSettings(ExtensionToEncryptlist, SoftwarePackageList);
                }
            });


        }

        public void Reload()
        {
            
            ExtensionToEncryptlist.Clear();
            SoftwarePackageList.Clear();
            ExtensionList.Clear();

            foreach (string element in SettingManager.Getsettings().SoftwarePackageList)
            {
                SoftwarePackageList.Add(element);
            }



            foreach (string ElementToEncrypt in SettingManager.Getsettings().ExtensionToEncryptlist)
            {

                ExtensionToEncryptlist.Add(ElementToEncrypt);

            }



            foreach (string Extension in TotalListExetensionsSupported)
            {

                ExtensionList.Add(Extension);

            }


            foreach (string ElementToEncrypt in SettingManager.Getsettings().ExtensionToEncryptlist)
            {

                ExtensionList.Remove(ElementToEncrypt);

            }
        }





     
    
    
    
    }




    }

