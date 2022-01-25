using System;

namespace EasySave
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TModel a = new TModel(); // Object instantiation 

            a.GetElementsOnADirectory(@"C:\Users\hugom\Desktop"); // Function check
        }
    }
}
