using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace EasySave
{
    class View
    {

        private String IdMessage;
        private String Language;
        public ResourceManager rm;

        //Constructor
        public View()
        {
            rm = new ResourceManager("EasySave.Resources.Strings", Assembly.GetExecutingAssembly());
            
        }


        //Display methods to display a message on the CLI

        public void DisplayTranslatedMessage(string IdMessage)
        {
            SetIdMessage(IdMessage);

            Console.WriteLine(rm.GetString(GetIdMessage()));

        }


        public void DisplayBasicMessage(string Message)
        {
           
            Console.WriteLine(Message);

        }


        //Method to set CLI language

        public void SetCLILanguage(string Language)
        {
            SetLanguage(Language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(GetLanguage());


            Console.Write(rm.GetString("Loading"));

            Console.Write("[");
            for (int i=0; i< 90; i++)
            {
                Console.Write("/");
                System.Threading.Thread.Sleep(50);
            }
            Console.Write("]");
            DisplayBasicMessage(" \n \n");

        }


        //Method to Clear CLI

        public void Clear()
        {
            Console.Clear();
        }



        //Setter & getter for IdMessage
        public void SetIdMessage(string IdMessage)
        {
            this.IdMessage = IdMessage ;
        }

        public string GetIdMessage()
        {
            return this.IdMessage;
        }


        // Setter & getter for Language

        public void SetLanguage(string Language)
        {
            this.Language = Language;
        }

        public string GetLanguage()
        {
            return this.Language;
        }




    }
}
