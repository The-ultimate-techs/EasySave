using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave
{
    public class TModel
    {
        public string[] GetElementsOnADirectory(string dir)
        {
            string[] files = Directory.GetFiles(@dir); // Get all the files in the specified directory
            foreach (string file in files)
                {
                    Console.WriteLine(file); // display each files in the specified directory
            };

            string[] directories = Directory.GetDirectories(@dir); // Get all the directories in the specified directory
            foreach (string directory in directories)
                {
                    Console.WriteLine(directory); // display each directories in the specified directory
                };
            return files; 

        }

        
    }
}
