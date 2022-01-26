using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class MainViewModel
    {
        //Private attributes
        private string Language;

        // publilc object from other classes 
        public View View;
/*      public FileSaveManagement FileSaveManagement;
        public  LogManagement();*/



        //Class Constructor 
        public MainViewModel()
        {
            View = new View();
            /*         FileSaveManagement = new FileSaveManagement();
                     LogManagement = new LogManagement();*/
            View.SetCLILanguage(GetUserLanguage());
            View.DisplayTranslatedMessage("Language");
            System.Threading.Thread.Sleep(5000);
            View.Clear();
           
        }


        //Method to get which Language the user wants to choose 
        public string GetUserLanguage()
        {
            // With this loop, users are forced to choose between FR or EN, if the choice is wrong it starts again
            while (true)
            { 
            string Message = "Choose your language / Choisissez votre langue : \n \n English: 1 \n Français: 2 \n \n Your choice / Votre choix :";
            View.DisplayBasicMessage(Message);
            string result = Console.ReadLine();
            View.DisplayBasicMessage("\n ");

                switch (result)
                {
                    case "1":
                        return "en-US";

                    case "2":
                        return "fr-FR";
                }
           
            }

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
