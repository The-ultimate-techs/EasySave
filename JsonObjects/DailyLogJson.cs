using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class DailyLogJson
    {
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public string DestPath { get; set; } = "";
        public long FileSize { get; set; }
        public long FileTransferTime { get; set; }
        public string Time { get; set; }
    }
}
