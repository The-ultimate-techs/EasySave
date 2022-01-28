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

            /*            MainViewModel MainViewModel = new MainViewModel();
            */

            /*FileSaveManagement test = new FileSaveManagement();*/

            // test.CreateSaveFile("Title",@"SourcePath",@"DestinationPath","Type") where type can be "differential" or "complete"
            /*  test.CreateSaveFile("TaMere", @"C:\TEST2\TEST3", @"C:\TEST\TEST3", "complete");
  */

            /*

            

            Console.WriteLine(Environment.GetEnvironmentVariable("Username"));*/

            FileSaveManagement a = new FileSaveManagement(); // Object instantiation 

            a.GetDirectoriesOnADirectory(@"C:\Users\chloe\OneDrive\Documents\GitHub", @"C:\thomas"); // Function check
            

        }
    }
}
