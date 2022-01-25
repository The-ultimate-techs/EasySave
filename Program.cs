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
<<<<<<< HEAD
            ResourceManager rm = new ResourceManager("EasySave.Resources.Strings", Assembly.GetExecutingAssembly());
            
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr-FR");

            Console.WriteLine(Thread.CurrentThread.CurrentUICulture.Name);
            Console.WriteLine(rm.GetString("Language"));
=======
            Console.WriteLine("Hello World!");

            TModel a = new TModel(); // Object instantiation 

            a.GetElementsOnADirectory(@"C:\Users\hugom\Desktop"); // Function check
<<<<<<< Updated upstream
=======
>>>>>>> b55a3fd0e81dbd25d3ac01b09e75cf1e85158dda
>>>>>>> Stashed changes
        }
    }
}
