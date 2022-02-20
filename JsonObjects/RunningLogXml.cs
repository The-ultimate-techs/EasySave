using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    public class RunningLogXml
    {

        [System.Xml.Serialization.XmlElement("Name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("SourceFilePath")]
        public string SourceFilePath { get; set; }

        [System.Xml.Serialization.XmlElement("TargetFilePath")]
        public string TargetFilePath { get; set; }

        [System.Xml.Serialization.XmlElement("State")]
        public string State { get; set; }

        [System.Xml.Serialization.XmlElement("TotalFilesToCopy")]
        public int TotalFilesToCopy { get; set; }

        [System.Xml.Serialization.XmlElement("TotalFilesSize")]
        public long TotalFilesSize { get; set; }

        [System.Xml.Serialization.XmlElement("NbFilesLeftToDo")]
        public int NbFilesLeftToDo { get; set; }

        [System.Xml.Serialization.XmlElement("Progression")]
        public int Progression { get; set; }
    }
}
