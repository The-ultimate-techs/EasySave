using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.MVVM.ObjectsForSerialization
{
    class Settingjson
    {
        public string Language { get; set; }
        public List<string> ExtensionToEncryptlist { get; set; }
        public List<string> SoftwarePackageList { get; set; }
        public List<string> FilesPriorityList { get; set; }
        public int ThresholdLimit { get; set; }
        public string LogType { get; set; }
    }
}
