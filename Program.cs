﻿using System;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace EasySave
{
    class Program
    {
        static void Main(string[] args)
        {

/*            MainViewModel MainViewModel = new MainViewModel();
*/

            FileSaveManagement test = new FileSaveManagement();

            // test.CreateSaveFile("Title",@"SourcePath",@"DestinationPath","Type") where type can be "differential" or "complete"
            test.CreateSaveFile("test", @"D:\TEST2", @"D:\TEST", "differential");

            /*
                        TModel a = new TModel(); // Object instantiation 

                        a.GetElementsOnADirectory(@"C:\Users\thomas\Desktop"); // Function check

                        Console.WriteLine(Environment.GetEnvironmentVariable("Username"));
            */

        }
    }
}
