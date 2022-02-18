using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasySave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (ChangeLanguageButton.Tag.ToString() == "fr-FR")
            {
                France.Opacity = 1;
                US.Opacity = 0.2;
                



            }
            if (ChangeLanguageButton.Tag.ToString() == "en-US")
            {

                France.Opacity = 0.2;
                US.Opacity = 1;
               
            }


        }

        private void Bu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Bu.Toggled1 == true)
            {
                France.Opacity = 1;
                US.Opacity = 0.2;
                Bu.Tag = "fr-FR";
                


            }
            else
            {

                France.Opacity = 0.2;
                US.Opacity = 1;
                Bu.Tag = "en-US";
            }


        }

        private void Bu_Loaded(object sender, RoutedEventArgs e)
        {
            

        }

        void CloseClick(object sender, RoutedEventArgs e)
        {



            Environment.Exit(0);



        }

        void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dash_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {

        }
        private void SearchClick(object sender, RoutedEventArgs e)
        {

        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {

        }


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }


    }
}
