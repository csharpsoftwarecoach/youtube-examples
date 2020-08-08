using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Adressverwaltung_Ohne_MVVM
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialisierung: Hier werden zwei Adressen automatisch in die ListView geladen. Zulässige Werte für den Familienstand werden geladen.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Gültigen Werte für den Familienstand werden geladen
            Familienstand.Items.Add("ledig");
            Familienstand.Items.Add("verheiratet");
            Familienstand.Items.Add("geschieden");
            Familienstand.Items.Add("verwitwet");


            // Lade die ersten Adressen vom Programm aus
            var adresse = new Adresse();
            adresse.Vorname = "Andrea";
            adresse.Nachname = "Huber";
            adresse.GeborenAm = Convert.ToDateTime("20.09.1989");
            adresse.Familienstand = "verheiratet";
            adresse.Strasse = "Finkstraße";
            adresse.StrassenNummer = "3";
            adresse.Plz = "45435";
            adresse.Ort = "Lindenberg";

            AdressListBox.Items.Add(adresse);



            adresse = new Adresse();
            adresse.Vorname = "Bernd";
            adresse.Nachname = "Kemnitz";
            adresse.GeborenAm = Convert.ToDateTime("01.03.2001");
            adresse.Familienstand = "ledig";
            adresse.Strasse = "Ottostraße";
            adresse.StrassenNummer = "456";
            adresse.Plz = "65467";
            adresse.Ort = "Bern";

            AdressListBox.Items.Add(adresse);
        }

        /// <summary>
        /// Eine neue Adresse wird in die ListView übertragen. Davor wird noch überprüft, ob die Daten korrekt und vollständig sind.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NeueAdresseOnClick(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Vorname.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Vornamen an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(Nachname.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Nachname an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(GeborenAm.Text))
            {
                MessageBox.Show("Bitte geben Sie Ihren Geburtstag an!", "Ungültige Eingabe");
                return;
            }

            if (!DateTime.TryParse(GeborenAm.Text, out _))
            {
                MessageBox.Show("Bitte geben Sie ein gültiges Datum an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(Familienstand.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Familienstand an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(Strasse.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Strasse an!", "Ungültige Eingabe");
                return;
            }

            if (!int.TryParse(StrassenNummer.Text, out _))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Strassen-Nummer an!", "Ungültige Eingabe");
                return;
            }

            if (!int.TryParse(PLZ.Text, out _) || PLZ.Text.Length != 5)
            {
                MessageBox.Show("Bitte geben Sie eine gültige PLZ an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(Ort.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Ort an!", "Ungültige Eingabe");
                return;
            }


            var adresse = new Adresse();
            adresse.Vorname = Vorname.Text;
            adresse.Nachname = Nachname.Text;
            adresse.GeborenAm = Convert.ToDateTime(GeborenAm.Text);
            adresse.Familienstand = Familienstand.Text;
            adresse.HandyNummer = Handynummer.Text;
            adresse.EMail = EMail.Text;
            adresse.Strasse = Strasse.Text;
            adresse.StrassenNummer = StrassenNummer.Text;
            adresse.Plz = PLZ.Text;
            adresse.Ort = Ort.Text;

            AdressListBox.Items.Add(adresse);

            Vorname.Text = string.Empty;
            Nachname.Text = string.Empty;
            GeborenAm.Text = string.Empty;
            Familienstand.Text = string.Empty;
            Handynummer.Text = string.Empty;
            EMail.Text = string.Empty;
            Strasse.Text = string.Empty;
            StrassenNummer.Text = string.Empty;
            PLZ.Text = string.Empty;
            Ort.Text = string.Empty;

            AdressListBox.SelectedItem = null;
        }

        /// <summary>
        /// Löscht die selektierte Adresse aus der ListView und anschließend wird auch die Maske und die Selektion gelöscht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoescheAdresseOnClick(object sender, RoutedEventArgs e)
        {
            if(AdressListBox.SelectedItem != null)
            {
                AdressListBox.Items.Remove(AdressListBox.SelectedItem);

                Vorname.Text = string.Empty;
                Nachname.Text = string.Empty;
                GeborenAm.Text = string.Empty;
                Familienstand.Text = string.Empty;
                Handynummer.Text = string.Empty;
                EMail.Text = string.Empty;
                Strasse.Text = string.Empty;
                StrassenNummer.Text = string.Empty;
                PLZ.Text = string.Empty;
                Ort.Text = string.Empty;

                AdressListBox.SelectedItem = null;
            }
        }

        /// <summary>
        /// Speichert die Adressinformationen aus der Maske (Steuerelemente) in die ListView, davor wird noch überprüft, ob alle Eingaben korrekt und vollständig sind.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeichernAdresseOnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Vorname.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Vornamen an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(Nachname.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Nachname an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(GeborenAm.Text))
            {
                MessageBox.Show("Bitte geben Sie Ihren Geburtstag an!", "Ungültige Eingabe");
                return;
            }

            if (!DateTime.TryParse(GeborenAm.Text, out _))
            {
                MessageBox.Show("Bitte geben Sie ein gültiges Datum an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(Familienstand.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Familienstand an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(Strasse.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Strasse an!", "Ungültige Eingabe");
                return;
            }

            if (!int.TryParse(StrassenNummer.Text, out _))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Strassen-Nummer an!", "Ungültige Eingabe");
                return;
            }

            if (!int.TryParse(PLZ.Text, out _) || PLZ.Text.Length != 5)
            {
                MessageBox.Show("Bitte geben Sie eine gültige PLZ an!", "Ungültige Eingabe");
                return;
            }

            if (string.IsNullOrEmpty(Ort.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Ort an!", "Ungültige Eingabe");
                return;
            }

            var adresse = new Adresse();

            if(AdressListBox.SelectedItem != null)
            {
                adresse = (Adresse)AdressListBox.SelectedItem;
            }

            adresse.Vorname = Vorname.Text;
            adresse.Nachname = Nachname.Text;
            adresse.GeborenAm = Convert.ToDateTime(GeborenAm.Text);
            adresse.Familienstand = Familienstand.Text;
            adresse.HandyNummer = Handynummer.Text;
            adresse.EMail = EMail.Text;
            adresse.Strasse = Strasse.Text;
            adresse.StrassenNummer = StrassenNummer.Text;
            adresse.Plz = PLZ.Text;
            adresse.Ort = Ort.Text;

            AdressListBox.Items.Refresh();

            Vorname.Text = string.Empty;
            Nachname.Text = string.Empty;
            GeborenAm.Text = string.Empty;
            Familienstand.Text = string.Empty;
            Handynummer.Text = string.Empty;
            EMail.Text = string.Empty;
            Strasse.Text = string.Empty;
            StrassenNummer.Text = string.Empty;
            PLZ.Text = string.Empty;
            Ort.Text = string.Empty;

            AdressListBox.SelectedItem = null;
        }

        /// <summary>
        /// Eine Adresse wird aus der ListView selektiert (markiert): Die Adresse wird in die obere Maske geladen (in die Steuerelemente).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdresseSelection_Click(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Falls keine Zeile in der ListView markiert wurde, mache dann nicht weiter.
            if (e.AddedItems.Count == 0)
                return;

            var adresse = (Adresse)e.AddedItems[0];

            Vorname.Text = adresse.Vorname;
            Nachname.Text = adresse.Nachname;
            GeborenAm.Text = adresse.GeborenAm.ToShortDateString();
            Familienstand.Text = adresse.Familienstand;
            Handynummer.Text = adresse.HandyNummer;
            EMail.Text = adresse.EMail;
            Strasse.Text = adresse.Strasse;
            StrassenNummer.Text = adresse.StrassenNummer;
            PLZ.Text = adresse.Plz;
            Ort.Text = adresse.Ort;
        }


        /// <summary>
        /// Lädt die Adressen aus der Textdatei in die ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LadeAdresseAusDateiOnClick(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Title = "Lade Adressen";
            openDialog.Filter = "Textdatei (*.txt)|*.txt";
            openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if (openDialog.ShowDialog() == true)
            {
                using (var reader = new StreamReader(openDialog.FileName))
                {
                    // Alle Adressen, die vielleicht davor existieren, werden gelöscht
                    AdressListBox.Items.Clear();

                    // Schleife solange durchlaufen, bis das Ende der Datei erreicht wurde.
                    while (!reader.EndOfStream)
                    {
                        // Lies die aktuelle Zeile aus der Datei in die Variable 'line'
                        var line = reader.ReadLine();

                        // Die Datenzeile wird in ein Array aufgeteilt, das Trennzeichen ist: ;
                        var values = line.Split(';');

                        var adresse = new Adresse();

                        adresse.Vorname = values[0];
                        adresse.Nachname = values[1];
                        adresse.GeborenAm = Convert.ToDateTime(values[2]);
                        adresse.Familienstand = values[3];
                        adresse.HandyNummer = values[4];
                        adresse.EMail = values[5];
                        adresse.Strasse = values[6];
                        adresse.StrassenNummer = values[7];
                        adresse.Plz = values[8];
                        adresse.Ort = values[9];

                        // Füge die neue Adresse der ListView hinzu
                        AdressListBox.Items.Add(adresse);
                    }
                }
            }
        }

        /// <summary>
        /// Speichert eine Textdatei mit den Adressen aus der ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeichernInEineDateiAdresseOnClick(object sender, RoutedEventArgs e)
        {
            // Wenn es keine Adressen in der ListView gibt, dann führe den weiteren Ablauf nicht aus.
            if (AdressListBox.Items.Count <= 0)
                return;

            var saveDialog = new SaveFileDialog();
            saveDialog.Title = "Speichere Adressen";
            saveDialog.Filter = "Textdatei (*.txt)|*.txt";
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if(saveDialog.ShowDialog() == true)
            {
                var csvDaten = new StringBuilder();

                foreach (var adresse in AdressListBox.Items.Cast<Adresse>())
                {
                    var newLine = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}", 
                                    adresse.Vorname, 
                                    adresse.Nachname,
                                    adresse.GeborenAm,
                                    adresse.Familienstand,
                                    adresse.HandyNummer,
                                    adresse.EMail,
                                    adresse.Strasse,
                                    adresse.StrassenNummer,
                                    adresse.Plz,
                                    adresse.Ort
                                    );

                    csvDaten.AppendLine(newLine);
                }

                // Es werden alle Adressen in die Textdatei geschrieben
                File.WriteAllText(saveDialog.FileName, csvDaten.ToString());
            }
        }
    }
}
