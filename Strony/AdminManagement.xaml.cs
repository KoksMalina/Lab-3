using PPIU_Lab_3.Klasy;
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

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace PPIU_Lab_3.Strony
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class AdminManagement : Page
    {
        public AdminManagement()
        {
            this.InitializeComponent();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register));
        }

        private void btnUsunUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            tbEmailDoZarzadzania.Visibility = Visibility.Visible;
            sqlConnection.managementEmail = tbEmailDoZarzadzania.Text;
            confirDeletionmBtn.Visibility = Visibility.Visible;
        }

        private void btnResetujHasloUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            tbEmailDoZarzadzania.Visibility = Visibility.Visible;
            sqlConnection.managementEmail = tbEmailDoZarzadzania.Text;
            resetBtn.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sqlConnection.DeleteUser();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sqlConnection.ResetPassword();
        }
    }
}
