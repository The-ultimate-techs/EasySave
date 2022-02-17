using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour CreateSaveFileView.xaml
    /// </summary>
    public partial class CreateSaveFileView : UserControl
    {
        public CreateSaveFileView()
        {
            InitializeComponent();

        }

        private void ButtonCreateClick1(object sender, RoutedEventArgs e)
        {


            ButtonCreate.Visibility = Visibility.Hidden;
            Buttonno.Visibility = Visibility.Visible;
            Buttonyes.Visibility = Visibility.Visible;
            Areyousure.Visibility = Visibility.Visible;


        }

        private void WriteText2(object sender, RoutedEventArgs e)
        {

        }

        private void WriteText1(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonyesClick(object sender, RoutedEventArgs e)
        {
            Title.Text = "";
            SourcePath.Text = "";
            DestPath.Text = "";


            ButtonCreate.Visibility = Visibility.Visible;
            Buttonno.Visibility = Visibility.Hidden;
            Buttonyes.Visibility = Visibility.Hidden;
            Areyousure.Visibility = Visibility.Hidden;
        }

        private void ButtonnoClick(object sender, RoutedEventArgs e)
        {


            ButtonCreate.Visibility = Visibility.Visible;
            Buttonno.Visibility = Visibility.Hidden;
            Buttonyes.Visibility = Visibility.Hidden;
            Areyousure.Visibility = Visibility.Hidden;
        }



        private void BrowseButton1Click(object sender, RoutedEventArgs e)
        {

            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                SourcePath.Text =  dlg.SelectedPath;
                SourcePath.Focus();
                
            }



        }

        private void BrowseButton2Click(object sender, RoutedEventArgs e)
        {

            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                DestPath.Text = dlg.SelectedPath;
                DestPath.Focus();
                
            }

        }




    }
}