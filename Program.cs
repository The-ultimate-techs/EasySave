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



            TModel a = new TModel(); // Object instantiation 

            a.GetElementsOnADirectory(@"C:\Users\thomas\Desktop"); // Function check

            Console.WriteLine(Environment.GetEnvironmentVariable("Username"));


        }
    }
}
