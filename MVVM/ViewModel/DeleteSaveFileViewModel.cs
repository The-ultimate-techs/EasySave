using EasySave.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace EasySave.MVVM.ViewModel
{
    class DeleteSaveFileViewModel
    {

        FileSaveManagement FileSaveManagement;





        public ObservableCollection<string> TitleList { get; set; } = new ObservableCollection<string>();
        public string Title { get; set; }


        public RelayCommand DeleteCommand { get; set; }

        public DeleteSaveFileViewModel()
        {
            FileSaveManagement = new FileSaveManagement();
            Clean();
            FullFillList();



            DeleteCommand = new RelayCommand(o =>

            {

              
                File.Delete(FileSaveManagement.GetSaveFileDirectory() + Title + ".json");
                TitleList.Remove(Title);
            });

        }


        public void FullFillList()
        {

            List<FileSave> ListFile = new List<FileSave> { };
            ListFile = FileSaveManagement.GetFilesOnADirectory(FileSaveManagement.GetSaveFileDirectory(), "");

            foreach (FileSave FileSave in ListFile)
            {

                TitleList.Add(FileSave.GetTitle().Replace(".Json", ""));

            }

        }


        public void Clean()
        {
            TitleList.Clear();
            Title = "";
        }
    }
}
