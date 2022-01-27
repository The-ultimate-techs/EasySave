using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class LogManagement : FileSave
    {

        private DateTime DateTime;
        private System.Diagnostics.Stopwatch Stopwatch;


        public LogManagement()
        {
            Stopwatch = new System.Diagnostics.Stopwatch();
            Stopwatch.Start();
        }




        public void BeginSaveFileExecution()
        {
            watch.start();
        }

    }
}
