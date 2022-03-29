using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using EasySave.MVVM.ObjectsForSerialization;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace EasySave.MVVM.Model
{
    class LogManagement : FileSave
    {
        
        
        private System.Diagnostics.Stopwatch Stopwatch;
        private long TimeDuration;
        private bool ProcessRunning;
        SettingManager SettingManager;


        public LogManagement()
        {
            Stopwatch = new System.Diagnostics.Stopwatch();
            SettingManager = new SettingManager();
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

            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.Json";

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {

                    sw.WriteLine("[]");

                }
            }
        }



        public void DailyLogGénérator(string Title , string SourceDirectory , string DestinationDirectory, string Type )
        {
            if (SettingManager.Getsettings().LogType == "JSON")
            {
                DailyLogGénératorJSON(Title, SourceDirectory, DestinationDirectory, Type);
            }
            else
            {
                DailyLogGénératorXML(Title, SourceDirectory, DestinationDirectory, Type);
            }
        }


        public void DailyLogGénératorJSON (string Title, string SourceDirectory, string DestinationDirectory, string Type)
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

            DailyLogXml obj = new DailyLogXml();

            string path = GetDirectoryPath() + "SaveFilesLogs\\DailyLog\\" + GetTitle() + ".xml";

            // if the file does not already exist, we create it
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            #region Update attributes of the object
            obj.Name = Title;
            obj.FileSource = SourceDirectory;
            obj.FileTarget = DestinationDirectory;
            obj.DestPath = "";
            obj.FileSize = FileSize(SourceDirectory);
            obj.FileTransferTime = GetTimeDuration();
            obj.Time = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            #endregion

            XmlSerializer XmlSerializer = new XmlSerializer(typeof(DailyLogXml));
            StreamWriter WriterXml = new StreamWriter(path, true);
            XmlSerializer.Serialize(WriterXml, obj); // This method serialize and write the xml file
            var fi1 = new FileInfo(path);
            long Fi1Length = fi1.Length;

            if (Fi1Length == 0) // On crée le fichier s'il n'existe pas encore
            {
                File.AppendAllText(path, "");
            }

            WriterXml.Close();


        }


        public void RunningLogGénérator(string Title, string SourceDirectory, string DestinationDirectory, int TotalFilesToCopy, long TotalFilesSize, int NbFilesLeftToDo, bool state)

        {

            if (SettingManager.Getsettings().LogType == "JSON")
            {
                RunningLogGénératorJSON( Title,  SourceDirectory,  DestinationDirectory,  TotalFilesToCopy,  TotalFilesSize,  NbFilesLeftToDo,  state);
            }
            else
            {
                RunningLogGénératorXML(Title, SourceDirectory, DestinationDirectory, TotalFilesToCopy, TotalFilesSize, NbFilesLeftToDo, state);
            }


        }






        public void RunningLogGénératorJSON(string Title, string SourceDirectory, string DestinationDirectory,  int TotalFilesToCopy, long TotalFilesSize, int NbFilesLeftToDo , bool state)
        
        {

            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.Json";


            string myJsonFile = File.ReadAllText(path);
            var myJsonList = JsonConvert.DeserializeObject<List<RunningLog>>(myJsonFile);
            bool trigger = false;
            foreach (RunningLog obj in myJsonList)
            {
                if (obj.Name == Title && trigger == false)
                {
                    obj.SourceFilePath = SourceDirectory;
                    obj.TargetFilePath = DestinationDirectory;
                    obj.State = (state == true) ? "ACTIVE" : "END"; 
                    obj.TotalFilesToCopy = TotalFilesToCopy;
                    obj.TotalFilesSize = TotalFilesSize;
                    obj.NbFilesLeftToDo = NbFilesLeftToDo;
                    obj.Progression = ((TotalFilesToCopy - NbFilesLeftToDo)*100 / TotalFilesToCopy);
                    trigger = true;
                }
            }
            if (trigger == false)
            {
                RunningLog Newobj = new RunningLog();
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


        public  void RunningLogGénératorXML(string Title, string SourceDirectory, string DestinationDirectory, int TotalFilesToCopy, long TotalFilesSize, int NbFilesLeftToDo , bool state)
        {
            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.xml";


            RunningLogXml ObjXml = new RunningLogXml();
           
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






        public bool RunningLogDeleted(string Title)

        {


            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.Json";
            string myJsonFile = File.ReadAllText(path);
            var myJsonList = JsonConvert.DeserializeObject<List<RunningLog>>(myJsonFile);
           
            foreach (RunningLog obj in myJsonList)
            {
                if (obj.Name == Title )
                {
                    obj.SourceFilePath = "";
                    obj.TargetFilePath = "";
                    obj.State = "DELETED";
                    obj.TotalFilesToCopy = 0;
                    obj.TotalFilesSize = 0;
                    obj.NbFilesLeftToDo = 0;
                    obj.Progression =0;
                   
                }
            }
            
            string json = JsonConvert.SerializeObject(myJsonList, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter sw = File.CreateText(path))
            {

                sw.WriteLine(json);
                sw.Close();

            }


            return true;
        }


        public List<RunningLog> LogReader()
        {


            if (SettingManager.Getsettings().LogType == "JSON")
            {
                return LogReaderJSON();
            }
            else
            {
                return LogReaderXML();
            }


        }


        public List<RunningLog> LogReaderJSON()
        {

            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.Json";
            var myJsonFile = File.ReadAllText(path);
            List<RunningLog> ListRunningLog = new List<RunningLog>();


            try
            {
                ListRunningLog = JsonConvert.DeserializeObject<List<RunningLog>>(myJsonFile);

            }
            catch (InvalidCastException e)
            {

                RunningLog RunningLog = new RunningLog();
                RunningLog.State = "ACTIVE";

                ListRunningLog.Add(RunningLog);
            }



            return ListRunningLog;

        }


        public List<RunningLog> LogReaderXML()
        {

       


            return ListRunningLog;

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




    }
}
