using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public FileSaveManagement FileSaveManagement;
        public LogManagement LogManagement;




        //Class Constructor 
        public MainViewModel()
        {
            View = new View();
            FileSaveManagement = new FileSaveManagement();
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
                View.Clear();
                View.DisplayTranslatedMessage("MainMenu");
               
                string result = Console.ReadLine();
                View.DisplayBasicMessage("\n ");

                switch (result)
                {
                    case "1":
                        SetMenuChoice(result);
                        SubMenuCreate();
                        break;

                    case "2":
                        SetMenuChoice(result);
                        SubMenuEditDelete();
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


        public void SubMenuCreate()
        {
            View.Clear();
            View.DisplayTranslatedMessage("SubMenuCreate");
            DisplayListSaveFile();

            View.DisplayTranslatedMessage("SubMenuCreate2");
            View.DisplayTranslatedMessage("SubMenuCreate.Title");
            string Title = Console.ReadLine();
            View.DisplayTranslatedMessage("SubMenuCreate.SourcePath");
            string SourcePath = Console.ReadLine();
            View.DisplayTranslatedMessage("SubMenuCreate.DestPath");
            string DestPath = Console.ReadLine();

            string Type;
            do
            {
                View.DisplayTranslatedMessage("SubMenuCreate.Type");

                Type = Console.ReadLine();
            } while (Type != "1" && Type != "2");

            
            SaveFileJson SaveFileJson = new SaveFileJson();
            SaveFileJson.Title = Title;
            SaveFileJson.SourcePath = SourcePath;
            SaveFileJson.DestPath = DestPath;
            SaveFileJson.Type = (Type == "1") ? "COMPLETE" : "PARTIAL"; ;

            string JsonPath = FileSaveManagement.GetSaveFileDirectory() + Title + ".Json";


            string json = JsonConvert.SerializeObject(SaveFileJson, Formatting.Indented);

            using (StreamWriter sw = File.CreateText(JsonPath))
            {

                sw.WriteLine(json);

            }

        }


        public void SubMenuEditDelete()
        {



            do
            {
                View.Clear();
                DisplayListSaveFile();
                View.DisplayTranslatedMessage("SubMenuEditDelete");
                View.DisplayTranslatedMessage("SubMenuEditDelete.choice");
                string result = Console.ReadLine();
                View.DisplayBasicMessage("\n ");

                switch (result)
                {
                    case "1":

                        SetMenuChoice(result);
                        string FileToEdit;
                        do
                        {
                            
                            View.Clear();
                            DisplayListSaveFile();
                            View.DisplayTranslatedMessage("SubMenuEditDelete.choice2");
                            FileToEdit = Console.ReadLine();


                            bool isNumeric = int.TryParse(FileToEdit, out _);
                            if (isNumeric ==  false )
                            {
                                FileToEdit = "0";
                            }

                        } while (Convert.ToInt32(FileToEdit) > ListSaveFile().Count || Convert.ToInt32(FileToEdit) <= 0);


                        View.Clear();

                       
                        View.DisplayTranslatedMessage("SubMenuCreate.SourcePath");
                        string SourcePath = Console.ReadLine();
                        View.DisplayTranslatedMessage("SubMenuCreate.DestPath");
                        string DestPath = Console.ReadLine();

                        string Type;
                        do
                        {
                            View.DisplayTranslatedMessage("SubMenuCreate.Type");

                            Type = Console.ReadLine();
                        } while (Type != "1" && Type != "2");


                        FileSave FileSave = new FileSave();
                        FileSave = ListSaveFile()[Convert.ToInt32(FileToEdit) - 1];

                        SaveFileJson EditFileJson = new SaveFileJson();
                        EditFileJson.Title = FileSave.GetTitle().Remove(5);
                        EditFileJson.SourcePath = SourcePath;
                        EditFileJson.DestPath = DestPath;
                        EditFileJson.Type = (Type == "1") ? "COMPLETE" : "PARTIAL"; ;


                        string JsonPath = FileSaveManagement.GetSaveFileDirectory() + EditFileJson.Title ;


                        string json = JsonConvert.SerializeObject(EditFileJson, Formatting.Indented);


                        string AreyousureEdit;
                        do
                        {

                            View.Clear();
                            View.DisplayTranslatedMessage("Areyousure");
                            AreyousureEdit = Console.ReadLine();


                        } while (AreyousureEdit != "1" && AreyousureEdit != "2");

                        if (AreyousureEdit == "1")
                        {
                            using (StreamWriter sw = File.CreateText(JsonPath))
                            {

                                sw.WriteLine(json);

                            }
                        }


                        break;

                    case "2":

                        string FileToDelete;
                        SetMenuChoice(result);
                        do
                        {
                            
                            View.Clear();
                            DisplayListSaveFile();
                            View.DisplayTranslatedMessage("SubMenuEditDelete.choice2");
                            FileToDelete = Console.ReadLine();


                            bool isNumeric = int.TryParse(FileToDelete, out _);
                            if (isNumeric == false)
                            {
                                FileToDelete = "0";
                            }

                        } while (Convert.ToInt32(FileToDelete) > ListSaveFile().Count || Convert.ToInt32(FileToDelete) <= 0);

                        string AreyousureDelete;
                        do
                        {
                            
                            View.Clear();
                            View.DisplayTranslatedMessage("Areyousure");
                            AreyousureDelete = Console.ReadLine();


                        } while (AreyousureDelete != "1" && AreyousureDelete != "2");

                        if (AreyousureDelete == "1")
                        {
                            FileSave FileToDeleteInfo = new FileSave();
                            FileToDeleteInfo = ListSaveFile()[Convert.ToInt32(FileToDelete)-1];
                            File.Delete(FileSaveManagement.GetSaveFileDirectory() + FileToDeleteInfo.GetTitle());
                        }

                       
                        break;

                    case "3":
                        SetMenuChoice(result);
                        break;



                }

            } while (GetMenuChoice() != "3");

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


        public void DisplayListSaveFile()
        {
            View.DisplayTranslatedMessage("ListSaveFile");
            List<FileSave> ListFile = new List<FileSave> { };
            ListFile = FileSaveManagement.GetFilesOnADirectory(FileSaveManagement.GetSaveFileDirectory(), "");

            int index = 1;
            foreach (FileSave FileSave in ListFile)
            {
                View.DisplayBasicMessage("\n n°" + index + " -->  " + FileSave.GetTitle());
                index++;
            }
        }

        public List<FileSave> ListSaveFile()
        {
            return FileSaveManagement.GetFilesOnADirectory(FileSaveManagement.GetSaveFileDirectory(), "");

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
