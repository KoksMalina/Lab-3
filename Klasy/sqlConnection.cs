using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using System.ServiceModel.Channels;

namespace PPIU_Lab_3.Klasy
{
      public class sqlConnection
      {
        public static async void createDB()
        { //ciag komend do podlaczenia do bazy
            await ApplicationData.Current.LocalFolder.CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                //otworzenie polaczenia z baza danych
                db.Open();

                //zapytanie tworzace tabele uzytkownikow
                String tableUsers = "CREATE TABLE IF NOT " +
                    "EXISTS Users (ID INTEGER PRIMARY KEY, " +
                    "IMIE VARCHAR NOT NULL," +
                    "NAZWISKO VARCHAR NOT NULL , " +
                    "STANOWISKO VARCHAR NOT NULL, " +
                    "PLEC VARCHAR NOT NULL, " +
                    "EMAIL VARCHAR NOT NULL, "+
                    "LOGIN VARCHAR NOT NULL,"+
                    "HASLO VARCHAR  NOT NULL,"+ 
                    "ROLA VARCHAR NOT NULL"+ ")";

                //komendy wywolujace zapytanie
                SqliteCommand createTableUsers = new SqliteCommand(tableUsers, db);
                createTableUsers.ExecuteReader();
                
                //zapytanie, a ponizej komendy je realizujace dla tabeli wydarzenia
                String tableEvents = "CREATE TABLE IF NOT EXISTS Events(ID INTEGER PRIMARY KEY AUTOINCREMENT, NAZWA VARCHAR NOT NULL, AGENDA VARCHAR NOT NULL," +
                    "DATA DATE NOT NULL, TYP_UCZESTNICTWA VARCHAR NOT NULL, WYZYWNIENIE VARCHAR NOT NULL)";
                SqliteCommand createTableEvents = new SqliteCommand(tableEvents, db);
                createTableEvents.ExecuteReader();

                string EventParticipantsQuery = "CREATE TABLE IF NOT EXISTS EventParticipants(ID INTEGER PRIMARY KEY AUTOINCREMENT, "
                    + "Imie VARCHAR NOT NULL ,"+ 
                    "Nazwisko VARCHAR NOT NULL ,"+
                    "Nazwa VARCHAR NOT NULL," +
                    " TypUczestnika VARCHAR NOT NULL, " +
                    "Wyzywienie VARCHAR NOT NULL)";
                SqliteCommand EventParticipants = new SqliteCommand(EventParticipantsQuery, db);
                EventParticipants.ExecuteReader();
                

                db.Close();
            }
        }
        //przekazanie danych z rejestracji do zmiennych, a potem do bazy danych
        public static string imie, nazwisko, plec, email, haslo,login,rola="user",stanowisko;
       

        public static void AddData()
        {
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection dba= new SqliteConnection($"Filename={dbPath}"))
            {
                dba.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = dba;
                insertCommand.CommandText = "INSERT INTO Users (IMIE,NAZWISKO,STANOWISKO,PLEC,EMAIL,LOGIN,HASLO,ROLA) " +
                    "VALUES(@IMIE,@NAZWISKO,@STANOWISKO,@PLEC,@EMAIL,@LOGIN,@HASLO,@ROLA)";

                insertCommand.Parameters.AddWithValue("@IMIE", imie);
                insertCommand.Parameters.AddWithValue("@NAZWISKO", nazwisko);
                insertCommand.Parameters.AddWithValue("@STANOWISKO", stanowisko);
                insertCommand.Parameters.AddWithValue("@PLEC", plec);
                insertCommand.Parameters.AddWithValue("@EMAIL", email);
                insertCommand.Parameters.AddWithValue("@LOGIN", login);
                insertCommand.Parameters.AddWithValue("@HASLO", haslo);
                insertCommand.Parameters.AddWithValue("@ROLA", rola);
                insertCommand.ExecuteNonQuery();

                dba.Close();
            }
        }

       public static int loginattempt = 3;
        public static bool positiveSignIn = false;
        public static string loginLogowanie,hasloLogowanie;

        public static async void signIn()
        {
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();
                SqliteCommand query1 = new SqliteCommand();
                query1.Connection = db;
                query1.CommandText = $"Select LOGIN FROM Users WHERE LOGIN= '{loginLogowanie}'";
                query1.ExecuteScalar();

                SqliteCommand query2 = new SqliteCommand();
                query2.Connection = db;
                query2.CommandText = $"Select HASLO from Users WHERE HASLO= '{hasloLogowanie}'";
                query2.ExecuteScalar();


                if (loginattempt==0)
                {
                    var messageDialog2 = new MessageDialog("Nie pozostało już żadnych prób logowania");
                       await messageDialog2.ShowAsync();
                }
               else if (query1.ExecuteScalar() == null && query2.ExecuteScalar() == null)
                {
                    loginattempt--;
                    var messageDialog = new MessageDialog($"Dane Logowania są nieprawidłowe. Pozostała ilośc logowań: {loginattempt}");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    positiveSignIn = true;

                }
                db.Close();
            }  
        }


        public static string nazwaWydarzenia, TypUczestnika, Wyzywienie,imieZalogowaneKonto, NazwiskoZalogowaneKonto;



        public static void RegisterForEvent()
        {
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection connection = new SqliteConnection($"Filename={dbPath}"))
            {
                connection.Open();
              
                SqliteCommand insertEventData = new SqliteCommand();
                insertEventData.Connection = connection;
                insertEventData.CommandText = " INSERT INTO EventParticiPants (Nazwa,TypUczestnika,Wyzywienie, Imie, Nazwisko) Values(@Nazwa, @TypUczestnika, @Wyzywienie, " +
                    "@Imie, @Nazwisko)";
                insertEventData.Parameters.AddWithValue("@Nazwa", nazwaWydarzenia);
                insertEventData.Parameters.AddWithValue("@TypUczestnika", TypUczestnika);
                insertEventData.Parameters.AddWithValue("@Wyzywienie", Wyzywienie);
                insertEventData.Parameters.AddWithValue("@Imie", imieZalogowaneKonto);
                insertEventData.Parameters.AddWithValue("@Nazwisko", NazwiskoZalogowaneKonto);
                insertEventData.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static string managementEmail;
        public static void DeleteUser()
        {
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection connection = new SqliteConnection($"Filename={dbPath}"))
            {
                connection.Open();

                SqliteCommand deleteUser = new SqliteCommand();
                deleteUser.Connection = connection;
                deleteUser.CommandText = $"DELETE FROM Users WHERE EMAIL= '{managementEmail}'";
                deleteUser.ExecuteScalar();

                connection.Close();
            }
        }

        public static void ResetPassword()
        {
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection connection = new SqliteConnection($"Filename={dbPath}"))
            {
                connection.Open();
                SqliteCommand ReseetPWQuery = new SqliteCommand();
                ReseetPWQuery.Connection = connection;
                ReseetPWQuery.CommandText = $"UPDATE Users Set HASLO=1234 WHERE EMAIL='{managementEmail}'";
                connection.Close();
            }

        }

      }
}
