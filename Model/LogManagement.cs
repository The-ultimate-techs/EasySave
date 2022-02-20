using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
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

        public bool DailyLogGénérator_XML(string Title, string SourceDirectory, string DestinationDirectory, string Type)
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
            XmlSerializer.Serialize(WriterXml, obj ); // This method serialize and write the xml file
            var fi1 = new FileInfo(path);
            long Fi1Length = fi1.Length;

            if (Fi1Length == 0) // On crée le fichier s'il n'existe pas encore
            {
                File.AppendAllText(path, "");
            }

            WriterXml.Close();

            return true;

        }



        public bool DailyLogGénérator_JSON(string Title , string SourceDirectory , string DestinationDirectory, string Type)
        {
            SetTitle(Title);
            SetSourceDirectory(SourceDirectory.Replace("\\","\\\\"));
            SetDestinationDirectory(DestinationDirectory.Replace("\\", "\\\\"));
            SetType(Type);

            DailyLogJson obj= new DailyLogJson();


            string path = GetDirectoryPath() + "SaveFilesLogs\\DailyLog\\" + GetTitle() + ".Json";

            if (!File.Exists(path)) // if the file does not already exist, we create it
            {
                File.WriteAllText(path, "");
            }

            // update of the attributes
            obj.Name = Title;
            obj.FileSource = SourceDirectory;
            obj.FileTarget = DestinationDirectory;
            obj.DestPath = "";
            obj.FileSize = FileSize(SourceDirectory);
            obj.FileTransferTime = GetTimeDuration();
            obj.Time = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");

            

            string json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            var fi1 = new FileInfo(path);
            string line;
            long Fi1Length = fi1.Length;

            if (Fi1Length == 0) // On crée le fichier q'il n'existe pas encore
            {
                File.AppendAllText(path, "");
            }

            // on récupère le texte existant
            var sr = new StreamReader(path);
            line = sr.ReadToEnd();
            sr.Close();

            // mise en forme entre chaque objet

            line= String.Concat(line, json);
            if (Fi1Length != 0)
            {
                line= line.Replace("}{", "}, {");
            }
            File.WriteAllText(path, line);
            
            
            


            /*// Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                obj.FileSource = SourceDirectory;
                obj.FileTarget = DestinationDirectory;
                obj.DestPath = "";
                obj.FileSize = FileSize(SourceDirectory);
                obj.FileTransferTime = GetTimeDuration();
                obj.Time = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
                trigger = true;



                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                sw.Write("\n ");
                sw.Write(json);
                sw.WriteLine(" \n");
                sw.Close();

                // old methode
                //sw.WriteLine("[\n { \n   \"Name\": \"" + GetTitle() + "\",\n   \"FileSource\": \"" + GetSourceDirectory() + "\",\n   \"FileTarget\": \"" + GetDestinationDirectory() + "\",\n   \"destPath\": \"\",\n   \"FileSize\": " + FileSize(SourceDirectory) + ",\n   \"FileTransferTime\": " + GetTimeDuration() + ",\n   \"time\": \"" + DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss") + "\"\n }\n]");

            }
        }

            else 
            {
                // This text is always added, making the file longer over time
                // if it is not deleted.

                using (StreamWriter sw = File.AppendText(path))
                {
                    // Update the attributes
                    obj.FileSource = SourceDirectory;
                    obj.FileTarget = DestinationDirectory;
                    obj.DestPath = "";
                    obj.FileSize = FileSize(SourceDirectory);
                    obj.FileTransferTime = GetTimeDuration();
                    obj.Time = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
                    trigger = true;

                    //old methode
                    //sw.WriteLine(" { \n   \"Name\": \"" + GetTitle() + "\",\n   \"FileSource\": \"" + GetSourceDirectory() + "\",\n   \"FileTarget\": \"" + GetDestinationDirectory() + "\",\n   \"destPath\": \"\",\n   \"FileSize\": "+FileSize(SourceDirectory)+",\n   \"FileTransferTime\": " + GetTimeDuration() + ",\n   \"time\": \"" + DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss") + "\"\n }\n]");


                    //transform the string into a json string
                    string newJson= JsonConvert.SerializeObject(obj, Formatting.Indented);

                    sw.Write(newJson);
                    sw.Write("\n]");
                    sw.Close();
                }
            }*/
            return true;
        }

        public bool RunningLogGénérator_XML_V2(string Title, string SourceDirectory, string DestinationDirectory, int TotalFilesToCopy, long TotalFilesSize, int NbFilesLeftToDo)
        {
            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.xml";

            XmlDocument doc = new XmlDocument();

            #region decla
            // The Xml declaration
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //(2) string.Empty makes cleaner code
            XmlElement element1 = doc.CreateElement(string.Empty, "body", string.Empty);
            doc.AppendChild(element1);

            XmlElement element2 = doc.CreateElement(string.Empty, "level1", string.Empty);
            element1.AppendChild(element2);

            XmlElement element3 = doc.CreateElement(string.Empty, "level2", string.Empty);
            XmlText text1 = doc.CreateTextNode("text");
            element3.AppendChild(text1);
            element2.AppendChild(element3);

            XmlElement element4 = doc.CreateElement(string.Empty, "level2", string.Empty);
            XmlText text2 = doc.CreateTextNode("other text");
            element4.AppendChild(text2);
            element2.AppendChild(element4);

            doc.Save(path);

            #endregion




            return true;
        }


        public bool RunningLogGénérator_XML_V1(string Title, string SourceDirectory, string DestinationDirectory, int TotalFilesToCopy, long TotalFilesSize, int NbFilesLeftToDo)
        {
            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.xml";

            var serializer = new XmlSerializer(typeof(RunningLogXml));
            RunningLogXml ObjXml = new RunningLogXml();
            List<RunningLogXml> ListObjXml = new List<RunningLogXml>();

            if (!File.Exists(path)) // if the log file does not exist
            {
                File.Create(path).Close();
            }

            else // if the log file already exist
            {
                StreamReader reader = new StreamReader(path);
                ObjXml = (RunningLogXml)serializer.Deserialize(reader);
                ListObjXml.Add(ObjXml);
                reader.Close();

                foreach(RunningLogXml ExistingObjXml in ListObjXml)
                if (ExistingObjXml.Name == Title)
                {
                    ObjXml.Name = Title;
                    ObjXml.SourceFilePath = SourceDirectory;
                    ObjXml.TargetFilePath = DestinationDirectory;
                    ObjXml.State = (GetProcessRunning() == true) ? "ACTIVE" : "END";
                    ObjXml.TotalFilesToCopy = TotalFilesToCopy;
                    ObjXml.TotalFilesSize = TotalFilesSize;
                    ObjXml.NbFilesLeftToDo = NbFilesLeftToDo;
                    ObjXml.Progression = ((TotalFilesToCopy - NbFilesLeftToDo) * 100 / TotalFilesToCopy);

                    FileStream OpenFile = File.Open(path, FileMode.Open);
                    // Serialize using the XmlTextWriter.
                    serializer.Serialize(OpenFile, ObjXml);
                    OpenFile.Close();
                }

                else
                {
                    RunningLogXml NewObjXml = new RunningLogXml();

                    NewObjXml.Name = Title;
                    NewObjXml.SourceFilePath = SourceDirectory;
                    NewObjXml.TargetFilePath = DestinationDirectory;
                    NewObjXml.State = (GetProcessRunning() == true) ? "ACTIVE" : "END";
                    NewObjXml.TotalFilesToCopy = TotalFilesToCopy;
                    NewObjXml.TotalFilesSize = TotalFilesSize;
                    NewObjXml.NbFilesLeftToDo = NbFilesLeftToDo;
                    NewObjXml.Progression = ((TotalFilesToCopy - NbFilesLeftToDo) * 100 / TotalFilesToCopy);

                    FileStream OpenFile = File.Open(path, FileMode.Open);
                    // Serialize using the XmlTextWriter.
                    serializer.Serialize(OpenFile, ObjXml);
                    OpenFile.Close();
                }
            }

            


            /*// if the file does not already exist, we create it
            if (!File.Exists(path))
            {
                // Create an XmlTextWriter using a FileStream.
                Stream Fs_Xml = new FileStream(path, FileMode.Create);
                XmlWriter Xml_Writer = new XmlTextWriter(Fs_Xml, Encoding.Unicode);
                // Serialize using the XmlTextWriter.
                serializer.Serialize(Xml_Writer, ObjXml);
                Xml_Writer.Close();
            }*/

            /*FileStream file = File.CreateText(path);
            // Serialize using the XmlTextWriter.
            serializer.Serialize(file, ObjXml);
            file.Close();*/


            return true;
        }

        public bool RunningLogGénérator_JSON(string Title, string SourceDirectory, string DestinationDirectory,  int TotalFilesToCopy, long TotalFilesSize, int NbFilesLeftToDo)
        {

            
            string path = GetDirectoryPath() + "SaveFilesLogs/StateLog.Json";

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


            string json = JsonConvert.SerializeObject(myJsonList, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter sw = File.CreateText(path))
            {

                sw.WriteLine(json);
                sw.Close();

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
