using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

//namespace EasySave.Model
namespace EasySave
{
    public class MemoryManager
    {
        public long MaximumAllocatedMemory; // In Bytes

        private void StartGarbageCollector()
        {
            GC.Collect();
            Console.WriteLine("The garbage had been collected, MaximumAllocatedMemory : {0} , GetTotalMemory : {1}", MaximumAllocatedMemory, GC.GetTotalMemory(false));
        }

        public void SetMaximumAllocatedMemory(int bytes)
        {
            MaximumAllocatedMemory = bytes;
            //Console.WriteLine("The MaximumAllocatedMemory had been setted, MaximumAllocatedMemory : {0} , GetTotalMemory : {1}", MaximumAllocatedMemory, GC.GetTotalMemory(false));
        }

        public void TryStartGarbageCollector()
        {

            while (Thread.CurrentThread.IsAlive)
            {
                //Console.WriteLine("MaximumAllocatedMemory : {0} , GetTotalMemory : {1}", MaximumAllocatedMemory, GC.GetTotalMemory(false));
                if (MaximumAllocatedMemory < GC.GetTotalMemory(false))
                {
                    StartGarbageCollector();
                }
            }
            
        }

    }
}
