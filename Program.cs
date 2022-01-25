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

            ResourceManager rm = new ResourceManager("EasySave.Resources.Strings", Assembly.GetExecutingAssembly());
            
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr-FR");

            Console.WriteLine(Thread.CurrentThread.CurrentUICulture.Name);
            Console.WriteLine(rm.GetString("Language"));

            TModel a = new TModel(); // Object instantiation 

            a.GetElementsOnADirectory(@"C:\Users\thomas\Desktop"); // Function check
        }
    }
}
