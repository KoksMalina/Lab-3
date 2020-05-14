using PPIU_Lab_3.Klasy;
using PPIU_Lab_3.Strony;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace PPIU_Lab_3
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            sqlConnection.createDB();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == "admin" && tbPassword.Password.ToString() == "admin")
            {
                Frame.Navigate(typeof(AdminManagement));
            }
            else
            {
                sqlConnection.signIn();

                if (sqlConnection.positiveSignIn == true)
                {
                    Frame.Navigate(typeof(EventRegister));
                }

            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register));
        }

        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            sqlConnection.loginLogowanie = tbLogin.Text;
        }

        private void tbPassword_CharacterReceived(UIElement sender, CharacterReceivedRoutedEventArgs args)
        {
           
        }

        private void tbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            sqlConnection.hasloLogowanie = tbPassword.Password.ToString();
        }
    }
}
