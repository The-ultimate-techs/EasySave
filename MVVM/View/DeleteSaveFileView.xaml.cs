using EasySave.MVVM.Model;
using EasySave.MVVM.ObjectsForSerialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasySave.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour EditDeleteSaveFileView.xaml
    /// </summary>
    public partial class DeleteSaveFileView : UserControl
    {

        FileSaveManagement FileSaveManagement = new FileSaveManagement();
        public DeleteSaveFileView()
        {
            InitializeComponent();
        }

        private void ButtonnoClick(object sender, RoutedEventArgs e)
        {


            Delete.Visibility = Visibility.Visible;
            Buttonno.Visibility = Visibility.Hidden;
            Buttonyes.Visibility = Visibility.Hidden;
            Areyousure.Visibility = Visibility.Hidden;
        }


        private void ButtonDeleteClick1(object sender, RoutedEventArgs e)
        {


            Delete.Visibility = Visibility.Hidden;
            Buttonno.Visibility = Visibility.Visible;
            Buttonyes.Visibility = Visibility.Visible;
            Areyousure.Visibility = Visibility.Visible;


        }

        private void ButtonyesClick(object sender, RoutedEventArgs e)
        {
            
            SourcePath.Text = "";
            DestPath.Text = "";

            Delete.Visibility = Visibility.Visible;
            Buttonno.Visibility = Visibility.Hidden;
            Buttonyes.Visibility = Visibility.Hidden;
            Areyousure.Visibility = Visibility.Hidden;
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {

            if (TitleSelected.Tag != null)
            {
                TitleSelected.SelectedItem = TitleSelected.Tag;
                FullFillForm(TitleSelected.Tag);
            }


        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SourcePath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TitleSelected.SelectedItem != null)
            {
                FullFillForm(TitleSelected.SelectedItem);

            }
        }


        public void FullFillForm(object name)
        {



            string myJsonFile = File.ReadAllText(FileSaveManagement.GetSaveFileDirectory() + name.ToString() + ".json");

            SaveFileJson FileDetail = new SaveFileJson { };

            FileDetail = JsonConvert.DeserializeObject<SaveFileJson>(myJsonFile);

            SourcePath.Text = FileDetail.SourcePath;
            SourcePath.Focus();
            DestPath.Text = FileDetail.DestPath;
            DestPath.Focus();

            if (FileDetail.Type == "COMPLETE")
            {
                Diferential.IsChecked = false;
                Complete.IsChecked = true;

            }

            if (FileDetail.Type == "PARTIAL")
            {
                Complete.IsChecked = false;
                Diferential.IsChecked = true;

            }

            TitleSelected.Text = name.ToString();




        }
    }
}
