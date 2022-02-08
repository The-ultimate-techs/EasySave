using System;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;
using System.IO;

namespace EasySave
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MainViewModel
            //MainViewModel MainViewModel = new MainViewModel();
            #endregion

            #region Thread Garbage Collector
            MemoryManager MemoryManager = new MemoryManager();
            MemoryManager.SetMaximumAllocatedMemory(75000);
            //Console.WriteLine("AAAAAAA MaximumAllocatedMemory : {0} , GetTotalMemory : {1}", MemoryManager.MaximumAllocatedMemory, GC.GetTotalMemory(false));


            //Thread Garbage Collector

            Thread InstanceCaller = new Thread(new ThreadStart(MemoryManager.TryStartGarbageCollector));


            //Instance caller
            InstanceCaller.Start();
            #endregion

            Version vt;

            // Create objects and release them to fill up memory with unused objects.
            while(true)
            {
                //Console.WriteLine("An object will be created, MaximumAllocatedMemory : {0} , GetTotalMemory : {1}", MemoryManager.MaximumAllocatedMemory, GC.GetTotalMemory(false));
                vt = new Version();
                Thread.Sleep(2000);
                //Console.WriteLine("An object has been created, MaximumAllocatedMemory : {0} , GetTotalMemory : {1}", MemoryManager.MaximumAllocatedMemory, GC.GetTotalMemory(false));
            }

            /*Console.WriteLine("Memory used before collection:       {0:N0}",GC.GetTotalMemory(false));

            
            // Collect all generations of memory.
            GC.Collect();
            Console.WriteLine("Memory used after full collection:   {0:N0}",GC.GetTotalMemory(true));*/


        }
    }
}
