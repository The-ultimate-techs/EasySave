using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class LogManagement : FileSave
    {

        private DateTime DateTime;
        private System.Diagnostics.Stopwatch Stopwatch;
        private long TimeDuration;


        public LogManagement()
        {
            Stopwatch = new System.Diagnostics.Stopwatch();
            
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



        public long GetTimeDuration()
        {
            return this.TimeDuration;
        }

        public void SetTimeDuration(long TimeDuration)
        {
            this.TimeDuration = TimeDuration;
        }


    }
}
