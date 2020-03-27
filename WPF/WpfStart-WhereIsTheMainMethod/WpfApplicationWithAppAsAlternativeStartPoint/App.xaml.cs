using System.Windows;

namespace WpfApplicationWithAppAsAlternativeStartPoint
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Hier ist es auch möglich, das MainWindow zu instanziieren, die Anwendung funktioniert auch.
            // Aber an dieser Stelle haben wir keine Möglichkeit Anwendungsargumente abzufragen, die evtl. beim Start der Anwendung angegeben worden sind.
            //var mainWindow = new MainWindow();
            //mainWindow.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Das MainWindow sollte in der OnStartup-Methode aufgerufen werden, denn hier haben wir die Möglichkeit die Anwendungsargumente abzufragen,
            // die beim Start der Anwendung gemacht worden sind (siehe Parameter von OnStartup: StartupEventArgs e).
            //
            // Die Anwendungsargumente können zu Testzwecken unter folgendem Punkt gefunden und geändert werden:
            //  -> Rechte Maustaste auf das Projekt klicken und im Kontextmenü findet man die Eigenschaften: unter Debuggen / Anwendungsargumente (hier lassen sich mit Leerzeichen getrennt Parameter angeben) 

            var mode = e.Args[0];
            var user = e.Args[1];
            var password = e.Args[2];

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
