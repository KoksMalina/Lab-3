using PPIU_Lab_3.Klasy;
using System;
using System.Collections.Generic;
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
    public sealed partial class EventRegister : Page
    {
        public EventRegister()
        {
            this.InitializeComponent();
        }

        private async void btnRejestracjaNaWydarzenie_Click(object sender, RoutedEventArgs e)
        {
            sqlConnection.imieZalogowaneKonto = tbName.Text;
            sqlConnection.NazwiskoZalogowaneKonto = tbNazwisko.Text;
            
            
            if (cbWydarzenie.SelectedIndex == 0)
            {
                sqlConnection.nazwaWydarzenia = "E3";
            }
            else if (cbWydarzenie.SelectedIndex == 1)
            {
                sqlConnection.nazwaWydarzenia = "Pyrkon";
            }
            else if (cbWydarzenie.SelectedIndex == 2)
            {
                sqlConnection.nazwaWydarzenia = "Intel Extreme Masters";
            }


            if(cbWyzywienie.SelectedIndex==0)
            {
                sqlConnection.Wyzywienie = "Bez Preferencji";
            }
            else if (cbWyzywienie.SelectedIndex == 1)
            {
                sqlConnection.Wyzywienie = "Wegetariańskie";
            }
           else if (cbWyzywienie.SelectedIndex == 0)
            {
                sqlConnection.Wyzywienie = "Bez glutenu";
            }


            if (cbtypUczestnika.SelectedIndex == 0)
            {
                sqlConnection.TypUczestnika = "Słuchacz";
            }
            else    if (cbtypUczestnika.SelectedIndex == 1)
            {
                sqlConnection.TypUczestnika = "Sponsor";
            }
            else  if (cbtypUczestnika.SelectedIndex == 2)
            {
                sqlConnection.TypUczestnika = "Organizator";
            }
            else if (cbtypUczestnika.SelectedIndex == 3)
            {
                sqlConnection.TypUczestnika = "Autor";
            }

            sqlConnection.RegisterForEvent();

            var messageDialog = new MessageDialog("Udało się pomyslnie zarejestrować na wydarzenie");
            await messageDialog.ShowAsync();
        }

        private void cbWydarzenie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbWydarzenie.SelectedIndex==0)
            {
                tbDataWydarzenia.Text = " Data wydarzenia: 26.07.2020";
            }
            else if( cbWydarzenie.SelectedIndex==1)
            {
                tbDataWydarzenia.Text = "Data wydarzenia 27.07.2020";
            }
            else if(cbWydarzenie.SelectedIndex==2)
            {
                tbDataWydarzenia.Text = "Data wydarzenia 28.07.2020";
            }

        }
    }
}
