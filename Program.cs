using System;
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
          /*  test.CreateSaveFile("TaMere", @"C:\TEST2\TEST3", @"C:\TEST\TEST3", "complete");
*/

            TModel a = new TModel(); // Object instantiation 

            a.GetDirectoriesOnADirectory(@"C:\Users\hugom\Desktop", @"C:\thomas"); // Function check

            Console.WriteLine(Environment.GetEnvironmentVariable("Username"));


        }
    }
}
