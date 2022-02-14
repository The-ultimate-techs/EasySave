using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class RunningLogJson
    {
        public string Name { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }
        public string DestPath { get; set; }
        public long FileSize { get; set; }
        public long FileTransferTime { get; set; }
        public string Time { get; set; }
        public string State { get; set; }
        public int TotalFilesToCopy { get; set; }
        public long TotalFilesSize { get; set; }
        public int NbFilesLeftToDo { get; set; }
        public int Progression { get; set; }

    }
}
