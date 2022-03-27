using EasySave.MVVM.ObjectsForSerialization;
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
        private string DefaultSettings { get; } = "{\r\n  \"Language\": \"en-US\",\r\n  \"ExtensionToEncryptlist\": [\r\n    \".DOCX\",\r\n    \".HTML\",\r\n    \".PDF\"\r\n  ],\r\n  \"SoftwarePackageList\": [\r\n  \"C:\\\\Program Files\\\\WindowsApps\\\\Microsoft.WindowsCalculator_11.2201.4.0_x64__8wekyb3d8bbwe\\\\CalculatorApp.exe\"  \r\n  ],\r\n  \"FilesPriorityList\": [\r\n    \".DOCX\",\r\n    \".HTML\",\r\n    \".PDF\"\r\n  ],\r\n\r\n  \"ThresholdLimit\": 2,\r\n\r\n  \"LogType\": \"JSON\"\r\n}\r\n";
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

        public void SetSettings(ObservableCollection<string> ExtensionToEncryptlist, ObservableCollection<string> SoftwarePackageList , string LogType)
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
            Settingjson.LogType = LogType;

            string json = JsonConvert.SerializeObject(Settingjson, Formatting.Indented);

            using (StreamWriter sw = File.CreateText(SettingJsonPath))
            {

                sw.WriteLine(json);
                sw.Close();
            }

             


        }

        public void SetSettings2(ObservableCollection<string> FilesPriorityList, int ThresholdLimit)
        {

            Settingjson Settingjson = new Settingjson();


            List<string> FilesPriorityList1 = new List<string>();

            foreach (string extension in FilesPriorityList)
            {
                FilesPriorityList1.Add(extension);
            }

           
            Settingjson = Getsettings();

            Settingjson.FilesPriorityList = FilesPriorityList1;
            Settingjson.ThresholdLimit = ThresholdLimit;

            string json = JsonConvert.SerializeObject(Settingjson, Formatting.Indented);

            using (StreamWriter sw = File.CreateText(SettingJsonPath))
            {

                sw.WriteLine(json);
                sw.Close();
            }




        }


        public void SetLanguage(string language)
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
