﻿using EasySave.MVVM.JsonObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace EasySave.MVVM.Model
{
    class SettingManager : FileSave
    {
        private string DefaultSettings { get; } = "{\r\n  \"Language\": \"en-US\",\r\n  \"ExtensionToEncryptlist\": [\r\n    \".PDF\",\r\n    \".DOCX\",\r\n    \".HTML\"\r\n  ],\r\n  \"SoftwarePackageList\": [\r\n    \"C:\\\\Windows\\\\System32\\\\calc.exe\"\r\n  ]\r\n}";
            public string SettingJsonPath { get; set; }
        public SettingManager()
        {
            SettingJsonPath = GetDirectoryPath() + @"\Setting.json";
            if (!File.Exists(SettingJsonPath))
            {

                using (StreamWriter sw = File.CreateText(SettingJsonPath))
                {
                    sw.Write(DefaultSettings);
                    sw.Close();

                }


            }
        }

        public Settingjson Getsettings()
        {

            string myJsonFile = File.ReadAllText(SettingJsonPath);
            return JsonConvert.DeserializeObject<Settingjson>(myJsonFile);

        }
        public void SetSettings(ObservableCollection<string> ExtensionToEncryptlist, ObservableCollection<string> SoftwarePackageList)
        {

            Settingjson Settingjson = new Settingjson();


            List<string> ExtensionToEncryptlist1 = new List<string>();
            List<string> SoftwarePackageList1 = new List<string>();

            foreach (string extension in ExtensionToEncryptlist)
            {
                ExtensionToEncryptlist1.Add(extension);
            }

            foreach (string Software in SoftwarePackageList)
            {
                SoftwarePackageList1.Add(Software); 
            }

            Settingjson = Getsettings();

            Settingjson.ExtensionToEncryptlist = ExtensionToEncryptlist1;
            Settingjson.SoftwarePackageList = SoftwarePackageList1;

            string json = JsonConvert.SerializeObject(Settingjson, Formatting.Indented);

            using (StreamWriter sw = File.CreateText(SettingJsonPath))
            {

                sw.WriteLine(json);
                sw.Close();
            }




        }


        public void SetLanguage( string language)
        {
            Settingjson Settingjson = new Settingjson();

                       
            Settingjson = Getsettings();

            Settingjson.Language = language;
            

            string json = JsonConvert.SerializeObject(Settingjson, Formatting.Indented);

            using (StreamWriter sw = File.CreateText(SettingJsonPath))
            {

                sw.WriteLine(json);
                sw.Close();
            }
        }
    }
}
