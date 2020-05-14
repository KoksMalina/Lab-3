using PPIU_Lab_3.Klasy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace PPIU_Lab_3.Strony
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class Register : Page
    {
        public Register()
        {
            this.InitializeComponent();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            sqlConnection.imie = tbName.Text;
        }

        private void tbLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            sqlConnection.nazwisko = tbLastName.Text;

        }

        private void cbGenderM_Checked(object sender, RoutedEventArgs e)
        {
            sqlConnection.plec = "Mezczyzna";
        }

        private void cbGenderF_Checked(object sender, RoutedEventArgs e)
        {
            sqlConnection.plec = "Kobieta";
        }

        private void tbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            sqlConnection.email = tbEmail.Text;
        }

        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            sqlConnection.login = tbLogin.Text;
        }

        private void tbPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            sqlConnection.haslo = tbPassword.Text;
        }
       public void tbRepeatPW_TextChanged(object sender, TextChangedEventArgs e)
       {
            string reapeatedPW = tbRepeatPW.Text;
       }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if( sqlConnection.haslo==tbRepeatPW.Text)
            {
                sqlConnection.AddData();
            }
            else
            {
                var messageDialog = new MessageDialog("Hasła są niezgodne");
                await messageDialog.ShowAsync();

            }
        }

        private void tbPosession_TextChanged(object sender, TextChangedEventArgs e)
        {
            sqlConnection.stanowisko = tbPosession.Text;
        }
    }
}
