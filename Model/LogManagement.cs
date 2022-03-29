using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace EasySave
{
    class LogManagement : FileSave
    {

        private DateTime DateTime;
        private System.Diagnostics.Stopwatch Stopwatch;
        private long TimeDuration;
        private bool ProcessRunning;


        

        public LogManagement()
        {
            Stopwatch = new System.Diagnostics.Stopwatch();
            SetProcessRunning(false);

            if (!Directory.Exists(GetDirectoryPath()))
            {
                Directory.CreateDirectory(GetDirectoryPath());
            }
            if (!Directory.Exists(GetDirectoryPath()+ "SaveFilesLogs\\"))
            {
                Directory.CreateDirectory(GetDirectoryPath() + "SaveFilesLogs\\");
            }
            if (!Directory.Exists(GetDirectoryPath() + "SaveFilesLogs\\DailyLog\\"))
            {
                Directory.CreateDirectory(GetDirectoryPath() + "SaveFilesLogs\\DailyLog\\");
            }
        }

        public void DailyLogGénérator(string Title, string SourceDirectory, string DestinationDirectory, string Type)
        {
            if(GetLogFileTypeSetting() == "JSON")
            {
                DailyLogGénératorJSON(Title, SourceDirectory, DestinationDirectory, Type);
            }
            else
            {
                DailyLogGénératorXML(Title, SourceDirectory, DestinationDirectory, Type);
            }
        }


        public void DailyLogGénératorJSON(string Title, string SourceDirectory, string DestinationDirectory, string Type)
        {
            SetTitle(Title);
            SetSourceDirectory(SourceDirectory.Replace("\\", "\\\\"));
            SetDestinationDirectory(DestinationDirectory.Replace("\\", "\\\\"));
            SetType(Type);




            string path = GetDirectoryPath() + "SaveFilesLogs\\DailyLog\\" + GetTitle() + ".Json";


            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {

                    sw.WriteLine("[\n { \n   \"Name\": \"" + GetTitle() + "\",\n   \"FileSource\": \"" + GetSourceDirectory() + "\",\n   \"FileTarget\": \"" + GetDestinationDirectory() + "\",\n   \"destPath\": \"\",\n   \"FileSize\": " + FileSize(SourceDirectory) + ",\n   \"FileTransferTime\": " + GetTimeDuration() + ",\n   \"time\": \"" + DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss") + "\"\n }\n]");

                }
            }
            else
            {
                // This text is always added, making the file longer over time
                // if it is not deleted.

                StreamReader reader = new StreamReader(File.OpenRead(path));
                string fileContent = reader.ReadToEnd();
                reader.Close();

                fileContent = fileContent.Replace("}\n]", "},");
                StreamWriter writer = new StreamWriter(File.OpenWrite(path));
                writer.Write(fileContent);
                writer.Close();


                using (StreamWriter sw = File.AppendText(path))
                {


                    sw.WriteLine(" { \n   \"Name\": \"" + GetTitle() + "\",\n   \"FileSource\": \"" + GetSourceDirectory() + "\",\n   \"FileTarget\": \"" + GetDestinationDirectory() + "\",\n   \"destPath\": \"\",\n   \"FileSize\": " + FileSize(SourceDirectory) + ",\n   \"FileTransferTime\": " + GetTimeDuration() + ",\n   \"time\": \"" + DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss") + "\"\n }\n]");

                    sw.Close();

                }
            }

        }

        public void DailyLogGénératorXML(string Title, string SourceDirectory, string DestinationDirectory, string Type)
        {
            SetTitle(Title);
            SetSourceDirectory(SourceDirectory.Replace("\\", "\\\\"));
            SetDestinationDirectory(DestinationDirectory.Replace("\\", "\\\\"));
            SetType(Type);

            string path = GetDirectoryPath() + "SaveFilesLogs\\DailyLog\\" + GetTitle() + ".xml";

            StreamWriter sw = File.AppendText(path); // Write the file, or create it if it does not exit

            if (new FileInfo(path).Length == 0)
            {
                sw.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<DailyLog>\n</DailyLog>");
            }
            sw.Close();

            List<string> ListeLines;
            string[] Lines = File.ReadAllLines(path);

            ListeLines = Lines.ToList();
            ListeLines.Remove("</DailyLog>"); // delete the last line "</Statelog>"
            ListeLines.Add("   <Save>");
            ListeLines.Add("      <Name>" + GetTitle() + "</Name>");
            ListeLines.Add("      <FileSource>" + GetSourceDirectory() + "</FileSource>");
            ListeLines.Add("      <FileTarget>" + GetDestinationDirectory() + "</FileTarget>");
            ListeLines.Add("     <DestPath />");
            ListeLines.Add("      <FileSize>" + FileSize(SourceDirectory) + "</FileSize>");
            ListeLines.Add("      <FileTransferTime>" + GetTimeDuration() + "</FileTransferTime>");
            ListeLines.Add("      <Time>" + DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss") + "</Time>");
            ListeLines.Add("   </Save>");
            ListeLines.Add("</DailyLog>");

            Lines = ListeLines.ToArray();
            File.WriteAllLines(path, Lines);
        }


        public void RunningLogGénérator(string Title, string SourceDirectory, string DestinationDirectory, int TotalFilesToCopy, long TotalFilesSize, int NbFilesLeftToDo, bool state)

        {
            if(GetLogFileTypeSetting() == "JSON")
            {
                RunningLogGénératorJSON(Title, SourceDirectory, DestinationDirectory, TotalFilesToCopy, TotalFilesSize, NbFilesLeftToDo, state);
            }
            else
            {
                RunningLogGénératorXML(Title, SourceDirectory, DestinationDirectory, TotalFilesToCopy, TotalFilesSize, NbFilesLeftToDo, state);
            }


        }


        public void RunningLogGénératorJSON(string Title, string SourceDirectory, string DestinationDirectory, int TotalFilesToCopy, long TotalFilesSize, int NbFilesLeftToDo, bool state)

        {

            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.Json";


            string myJsonFile = File.ReadAllText(path);
            var myJsonList = JsonConvert.DeserializeObject<List<RunningLogJson>>(myJsonFile);
            bool trigger = false;
            foreach (RunningLogJson obj in myJsonList)
            {
                if (obj.Name == Title && trigger == false)
                {
                    obj.SourceFilePath = SourceDirectory;
                    obj.TargetFilePath = DestinationDirectory;
                    obj.State = (state == true) ? "ACTIVE" : "END";
                    obj.TotalFilesToCopy = TotalFilesToCopy;
                    obj.TotalFilesSize = TotalFilesSize;
                    obj.NbFilesLeftToDo = NbFilesLeftToDo;
                    obj.Progression = ((TotalFilesToCopy - NbFilesLeftToDo) * 100 / TotalFilesToCopy);
                    trigger = true;
                }
            }
            if (trigger == false)
            {
                RunningLogJson Newobj = new RunningLogJson();
                Newobj.Name = Title;
                Newobj.SourceFilePath = SourceDirectory;
                Newobj.TargetFilePath = DestinationDirectory;
                Newobj.State = (state == true) ? "ACTIVE" : "END";
                Newobj.TotalFilesToCopy = TotalFilesToCopy;
                Newobj.TotalFilesSize = TotalFilesSize;
                Newobj.NbFilesLeftToDo = NbFilesLeftToDo;
                Newobj.Progression = ((TotalFilesToCopy - NbFilesLeftToDo) * 100 / TotalFilesToCopy);
                trigger = true;

                myJsonList.Add(Newobj);
            }


            string json = JsonConvert.SerializeObject(myJsonList, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(json);
                sw.Close();
            }
        }

        public void RunningLogGénératorXML(string Title, string SourceDirectory, string DestinationDirectory, int TotalFilesToCopy, long TotalFilesSize, int NbFilesLeftToDo, bool state)
        {
            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.xml";

            int Progression;
            string Title_XML = "      <Title>" + Title + "</Title>";


            StreamWriter sw = File.AppendText(path); // Write the file, or create it if it does not exit

            if (new FileInfo(path).Length == 0)
            {
                sw.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Statelog>\n</Statelog>");
            }
            sw.Close();


            Progression = ((TotalFilesToCopy - NbFilesLeftToDo) * 100 / TotalFilesToCopy);
            bool writed = false;
            string State = (state == true) ? "ACTIVE" : "END";
            List<string> ListeLines;

            string[] Lines = File.ReadAllLines(path);

            for (int i = 0; i < Lines.Length; i++)
            {
                if (Lines[i] == Title_XML) // if the process already exist
                {
                    ListeLines = Lines.ToList();
                    ListeLines.Remove("</Statelog>"); // delete the last line "</Statelog>"
                    ListeLines[i + 3] = ("      <State>" + State + "</State>");
                    ListeLines[i + 4] = ("      <TotalFilesToCopy>" + TotalFilesToCopy + "</TotalFilesToCopy>");
                    ListeLines[i + 5] = ("      <TotalFilesSize>" + TotalFilesSize + "</TotalFilesSize>");
                    ListeLines[i + 6] = ("      <NbFilesLeftToDo>" + NbFilesLeftToDo + "</NbFilesLeftToDo>");
                    ListeLines[i + 7] = ("      <Progression>" + Progression + "</Progression>");
                    ListeLines.Add("</Statelog>");

                    Lines = ListeLines.ToArray();
                    File.WriteAllLines(path, Lines);
                    writed = true;

                    break;
                }
            }

            if (writed == false)
            {
                ListeLines = Lines.ToList();
                ListeLines.Remove("</Statelog>"); // delete the last line "</Statelog>"
                ListeLines.Add("   <Save>");
                ListeLines.Add("      <Title>" + Title + "</Title>");
                ListeLines.Add("      <SourceFilePath>" + SourceDirectory + "</SourceFilePath>");
                ListeLines.Add("      <TargetFilePath>" + DestinationDirectory + "</TargetFilePath>");
                ListeLines.Add("      <State>" + State + "</State>");
                ListeLines.Add("      <TotalFilesToCopy>" + TotalFilesToCopy + "</TotalFilesToCopy>");
                ListeLines.Add("      <TotalFilesSize>" + TotalFilesSize + "</TotalFilesSize>");
                ListeLines.Add("      <NbFilesLeftToDo>" + NbFilesLeftToDo + "</NbFilesLeftToDo>");
                ListeLines.Add("      <Progression>" + Progression + "</Progression>");
                ListeLines.Add("   </Save>");
                ListeLines.Add("</Statelog>");

                Lines = ListeLines.ToArray();
                File.WriteAllLines(path, Lines);
            }



        }

        public List<RunningLogJson> LogReader()
        {

            
            if (GetLogFileTypeSetting() == "JSON")
            {
                return LogReaderJSON();
            }
            else
            {
                return LogReaderXML();
            }


        }


        public List<RunningLogJson> LogReaderJSON()
        {

            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.Json";
            var myJsonFile = File.ReadAllText(path);
            List<RunningLogJson> ListRunningLog = new List<RunningLogJson>();


            try
            {
                ListRunningLog = JsonConvert.DeserializeObject<List<RunningLogJson>>(myJsonFile);

            }
            catch (InvalidCastException e)
            {

                RunningLogJson RunningLog = new RunningLogJson();
                RunningLog.State = "ACTIVE";

                ListRunningLog.Add(RunningLog);
            }



            return ListRunningLog;

        }


        public List<RunningLogJson> LogReaderXML()
        {


            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.xml";
            List<string> ListeLines;
            string[] Lines = File.ReadAllLines(path);
            List<RunningLogJson> ListRunningLog = new List<RunningLogJson>();
            ListeLines = Lines.ToList();

            for (int i = 2; i < Lines.Length - 1; i = i + 10)
            {

                RunningLogJson RunningLog = new RunningLogJson();

                int index;
                string value;


                index = ListeLines[i + 1].IndexOf(">");
                value = ListeLines[i + 1].Substring(index + 1);
                index = value.IndexOf("<");
                value = value.Substring(0, index);
                RunningLog.Name = value;

                index = ListeLines[i + 2].IndexOf(">");
                value = ListeLines[i + 2].Substring(index + 1);
                index = value.IndexOf("<");
                value = value.Substring(0, index);
                RunningLog.SourceFilePath = value;

                index = ListeLines[i + 3].IndexOf(">");
                value = ListeLines[i + 3].Substring(index + 1);
                index = value.IndexOf("<");
                value = value.Substring(0, index);
                RunningLog.TargetFilePath = value;

                index = ListeLines[i + 4].IndexOf(">");
                value = ListeLines[i + 4].Substring(index + 1);
                index = value.IndexOf("<");
                value = value.Substring(0, index);
                RunningLog.State = value;

                index = ListeLines[i + 5].IndexOf(">");
                value = ListeLines[i + 5].Substring(index + 1);
                index = value.IndexOf("<");
                value = value.Substring(0, index);

                RunningLog.TotalFilesToCopy = Int32.Parse(value);

                index = ListeLines[i + 6].IndexOf(">");
                value = ListeLines[i + 6].Substring(index + 1);
                index = value.IndexOf("<");
                value = value.Substring(0, index);
                RunningLog.TotalFilesSize = long.Parse(value);


                index = ListeLines[i + 7].IndexOf(">");
                value = ListeLines[i + 7].Substring(index + 1);
                index = value.IndexOf("<");
                value = value.Substring(0, index);
                RunningLog.NbFilesLeftToDo = Int32.Parse(value);

                index = ListeLines[i + 8].IndexOf(">");
                value = ListeLines[i + 8].Substring(index + 1);
                index = value.IndexOf("<");
                value = value.Substring(0, index);
                RunningLog.Progression = Int32.Parse(value);



                ListRunningLog.Add(RunningLog);
            }



            return ListRunningLog;

        }



        public void Convert()
        {

            string pathJSON = GetDirectoryPath() + "SaveFilesLogs/StateLog.Json";
            string pathXML = GetDirectoryPath() + "SaveFilesLogs/StateLog.xml";
            
            if (GetLogFileTypeSetting() == "JSON")
            {
                List<RunningLogJson> List2Convert = LogReaderXML();
                string json = JsonConvert.SerializeObject(List2Convert, Newtonsoft.Json.Formatting.Indented);

                using (StreamWriter sw = File.CreateText(pathJSON))
                {

                    sw.WriteLine(json);
                    sw.Close();

                }
                File.Delete(pathXML);
            }
            else
            {
                List<RunningLogJson> List2Convert = LogReaderJSON();

                StreamWriter sw = File.AppendText(pathXML); // Write the file, or create it if it does not exit
                if (new FileInfo(pathXML).Length == 0)
                {
                    sw.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Statelog>\n</Statelog>");

                }
                sw.Close();


                List<string> ListeLines;

                string[] Lines = File.ReadAllLines(pathXML);


                foreach (RunningLogJson runningLog in List2Convert)
                {

                    ListeLines = Lines.ToList();
                    ListeLines.Remove("</Statelog>"); // delete the last line "</Statelog>"
                    ListeLines.Add("   <Save>");
                    ListeLines.Add("      <Title>" + runningLog.Name + "</Title>");
                    ListeLines.Add("      <SourceFilePath>" + runningLog.SourceFilePath + "</SourceFilePath>");
                    ListeLines.Add("      <TargetFilePath>" + runningLog.TargetFilePath + "</TargetFilePath>");
                    ListeLines.Add("      <State>" + runningLog.State + "</State>");
                    ListeLines.Add("      <TotalFilesToCopy>" + runningLog.TotalFilesToCopy + "</TotalFilesToCopy>");
                    ListeLines.Add("      <TotalFilesSize>" + runningLog.TotalFilesSize + "</TotalFilesSize>");
                    ListeLines.Add("      <NbFilesLeftToDo>" + runningLog.NbFilesLeftToDo + "</NbFilesLeftToDo>");
                    ListeLines.Add("      <Progression>" + runningLog.Progression + "</Progression>");
                    ListeLines.Add("   </Save>");
                    ListeLines.Add("</Statelog>");

                    Lines = ListeLines.ToArray();
                    File.WriteAllLines(pathXML, Lines);

                }


                File.Delete(pathJSON);
            }


        }





        public void BeginSaveFileExecution()
        {

            Stopwatch.Restart();
            Stopwatch.Start();
        }

        public long EndSaveFileExecution()
        {
                        
            Stopwatch.Stop();
            SetTimeDuration(Stopwatch.ElapsedMilliseconds);
            return GetTimeDuration();
        }

        public void BeginEndProcess()
        {
            this.ProcessRunning = !this.ProcessRunning;
        }







        public long GetTimeDuration()
        {
            return this.TimeDuration;
        }

        public void SetTimeDuration(long TimeDuration)
        {
            this.TimeDuration = TimeDuration;
        }

        public void SetProcessRunning (bool ProcessRunning)
        {
            this.ProcessRunning = ProcessRunning;
        }

        public bool GetProcessRunning()
        {
            return this.ProcessRunning;
        }

        public string GetLanguageSetting()
        {
            string path = GetDirectoryPath() + @"\Setting.json";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("{\n  \"Language\": \"fr-FR\",\n  \"LogType\": \"XML\"\n}");
                }
            }

            #region GetSettings
            string myJsonFile = File.ReadAllText(path);
            Settingjson SettingObj = JsonConvert.DeserializeObject<Settingjson>(myJsonFile);
            #endregion

            return SettingObj.Language;
        }
        public void SetLanguageSetting(string Language)
        {
            string path = GetDirectoryPath() + @"\Setting.json";

            #region if the file does not exist 
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("[]");
                }
            }
            #endregion

            #region GetSettings
            string myJsonFile = File.ReadAllText(path);
            Settingjson SettingObj = JsonConvert.DeserializeObject<Settingjson>(myJsonFile);
            #endregion

            #region SetSettings
            SettingObj.Language = Language;

            string json = JsonConvert.SerializeObject(SettingObj, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(json);
                sw.Close();
            }
            #endregion

        }

        public void SetLogFileTypeSetting(string LogFileType)
        {
            string path = GetDirectoryPath() + @"\Setting.json";

            #region if the file does not exist 
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("[]");
                }
            }
            #endregion

            #region GetSettings
            string myJsonFile = File.ReadAllText(path);
            Settingjson SettingObj = JsonConvert.DeserializeObject<Settingjson>(myJsonFile);
            #endregion

            #region SetSettings
            SettingObj.LogType = LogFileType;

            string json = JsonConvert.SerializeObject(SettingObj, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(json);
                sw.Close();
            }


            #endregion

        }

        public string GetLogFileTypeSetting()
        {
            string path = GetDirectoryPath() + @"\Setting.json";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("{\n  \"Language\": \"fr-FR\",\n  \"LogType\": \"XML\"\n}");
                }
            }

            #region GetSettings
            string myJsonFile = File.ReadAllText(path);
            Settingjson SettingObj = JsonConvert.DeserializeObject<Settingjson>(myJsonFile);
            #endregion

            return SettingObj.LogType;
        }


    }
}
