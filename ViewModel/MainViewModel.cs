using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class MainViewModel
    {

        private string Language;


        public View View;
/*      public FileSaveManagement FileSaveManagement;
        public  LogManagement();*/




        public MainViewModel()
        {
            View = new View();
            /*         FileSaveManagement = new FileSaveManagement();
                     LogManagement = new LogManagement();*/
            View.SetCLILanguage(GetUserLanguage());
            View.DisplayTranslatedMessage("Language");
        }

        public string GetUserLanguage()
        {
            
            while (true)
            { 
            string Message = "Choose your language / Choisissez votre langue : \n \n English: 1 \n Français: 2 \n \n Your choice / Votre choix :";
            View.DisplayBasicMessage(Message);

            string result = Console.ReadLine();

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
