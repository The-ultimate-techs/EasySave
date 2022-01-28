using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

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
        }



        public bool DailyLogGénérator(string Title , string SourceDirectory , string DestinationDirectory, string Type )
        {
            SetTitle(Title);
            SetSourceDirectory(SourceDirectory);
            SetDestinationDirectory(DestinationDirectory);
            SetType(Type);




            string path = GetSaveFilesDirectory() + "DailyLog/" + GetTitle() + ".Json";


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
                   

                    sw.WriteLine(" { \n   \"Name\": \"" + GetTitle() + "\",\n   \"FileSource\": \"" + GetSourceDirectory() + "\",\n   \"FileTarget\": \"" + GetDestinationDirectory() + "\",\n   \"destPath\": \"\",\n   \"FileSize\": "+FileSize(SourceDirectory)+",\n   \"FileTransferTime\": " + GetTimeDuration() + ",\n   \"time\": \"" + DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss") + "\"\n }\n]");

                }
            }
            return true;
        }



        public bool RunningLogGénérator(string Title, string SourceDirectory, string DestinationDirectory,  int TotalFilesToCopy, int TotalFilesSize, int NbFilesLeftToDo)
        
        {

            
            string path = GetSaveFilesDirectory() + "StateLog.Json";

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {

                    sw.WriteLine("[]");

                }
            }

            string myJsonFile = File.ReadAllText(path);
            var myJsonList = JsonConvert.DeserializeObject<List<RunningLogJson>>(myJsonFile);
            bool trigger = false;
            foreach (RunningLogJson obj in myJsonList)
            {
                if (obj.Name == Title && trigger == false)
                {
                    obj.SourceFilePath = SourceDirectory;
                    obj.TargetFilePath = DestinationDirectory;
                    obj.State = (GetProcessRunning() == true) ? "ACTIVE" : "END"; 
                    obj.TotalFilesToCopy = TotalFilesToCopy;
                    obj.TotalFilesSize = TotalFilesSize;
                    obj.NbFilesLeftToDo = NbFilesLeftToDo;
                    obj.Progression = ((TotalFilesToCopy - NbFilesLeftToDo)*100 / TotalFilesToCopy);
                    trigger = true;
                }
            }
            if (trigger == false)
            {
                RunningLogJson Newobj = new RunningLogJson();
                Newobj.Name = Title;
                Newobj.SourceFilePath = SourceDirectory;
                Newobj.TargetFilePath = DestinationDirectory;
                Newobj.State = (GetProcessRunning() == true) ? "ACTIVE" : "END";
                Newobj.TotalFilesToCopy = TotalFilesToCopy;
                Newobj.TotalFilesSize = TotalFilesSize;
                Newobj.NbFilesLeftToDo = NbFilesLeftToDo;
                Newobj.Progression = ((TotalFilesToCopy - NbFilesLeftToDo) * 100 / TotalFilesToCopy);
                trigger = true;

                myJsonList.Add(Newobj);

            }


            string json = JsonConvert.SerializeObject(myJsonList, Formatting.Indented);

            using (StreamWriter sw = File.CreateText(path))
            {

                sw.WriteLine(json);

            }


            return true;
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
