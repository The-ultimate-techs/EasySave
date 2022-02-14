using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.MVVM.JsonObjects
{
    class Settingjson
    {
        public string Language { get; set; }
        public List<string>? ExtensionToEncryptlist { get; set; }
        public List<string>? SoftwarePackageList { get; set; }
        public string LogType { get; set; }
    }
}
