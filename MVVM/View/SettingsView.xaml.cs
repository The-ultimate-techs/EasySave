using System;
using System.Collections;
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
    /// Logique d'interaction pour SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl

    {

        

        public SettingsView()
        {
            
            InitializeComponent();
        }

     


        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                FileNameTextBox.Text = openFileDlg.FileName;
                FileNameTextBox.Focus();
            }

        }

        private void ExtensionsToEncrypt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ExtensionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Processlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void ExtensionsList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Add1Click(object sender, RoutedEventArgs e)
        {
        
            

        }

        private void Remove1Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Add2Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove2Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
