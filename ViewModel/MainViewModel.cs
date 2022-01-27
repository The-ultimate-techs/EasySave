using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class MainViewModel
    {
        //Private attributes
        private string Language;
        private string MenuChoice;

        // publilc object from other classes 
        public View View;
        //public FileSaveManagement FileSaveManagement;
        public LogManagement LogManagement;




        //Class Constructor 
        public MainViewModel()
        {
            View = new View();
            //FileSaveManagement = new FileSaveManagement();
            LogManagement = new LogManagement();
            View.SetCLILanguage(GetUserLanguage());
            View.DisplayTranslatedMessage("Language");
            System.Threading.Thread.Sleep(2000);
            View.Clear();
            Menu();

        }



        //Menu Method 
        public void Menu()
        {

            do
            {
                View.DisplayTranslatedMessage("MainMenu");
               
                string result = Console.ReadLine();
                View.DisplayBasicMessage("\n ");

                switch (result)
                {
                    case "1":
                        SetMenuChoice(result);
                        break;

                    case "2":
                        SetMenuChoice(result);
                        break;

                    case "3":
                        SetMenuChoice(result);


                        break;

                    case "4":
                        SetMenuChoice(result);
                        View.Clear();
                        View.SetCLILanguage(GetUserLanguage());
                        View.DisplayTranslatedMessage("Language");
                        System.Threading.Thread.Sleep(2000);
                        View.Clear();
                        break;

                    case "5":
                       
                        SetMenuChoice(result);
                        View.Clear();
                        View.DisplayTranslatedMessage("Goodbye");
                        System.Threading.Thread.Sleep(2000);
                        break;
                }

            } while (GetMenuChoice() != "5" );
            
        }




        //Method to get which Language the user wants to choose 
        public string GetUserLanguage()
        {
            // With this loop, users are forced to choose between FR or EN, if the choice is wrong it starts again
            do
            {
                string Message = "Choose your language / Choisissez votre langue : \n \n English: 1 \n Français: 2 \n \nYour choice / Votre choix :  ";
                View.DisplayBasicMessage(Message);
                string result = Console.ReadLine();
                View.DisplayBasicMessage("\n ");

                switch (result)
                {
                    case "1":
                        SetMenuChoice("en-US");
                        break;

                    case "2":
                        SetMenuChoice("fr-FR");
                        break;
                }

            } while (GetMenuChoice() != "en-US" && GetMenuChoice() != "fr-FR");
            return GetMenuChoice();
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

        public void SetMenuChoice(string MenuChoice)
        {
            this.MenuChoice = MenuChoice;
        }

        public string GetMenuChoice()
        {
            return this.MenuChoice;
        }




    }
}
