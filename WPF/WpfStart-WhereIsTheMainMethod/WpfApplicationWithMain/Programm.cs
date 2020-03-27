using System;
using System.Windows;

namespace WpfApplicationWithMain
{
    public class Programm
    {
        [STAThread]
        static void Main(string[] args)
        {
            // 1.) Der saubere Weg für eine einwandfrei funktionierende WPF-Anwendung.
            var app = new Application();
            app.Run(new MainWindow());

            // 2.) Das würde nicht funktionieren: Das Fenster schließt sich sofort.
            //var mainWindow = new MainWindow();
            //mainWindow.Show();

            // 3.) Das würde zwar funktionieren und das MainWindow zeigt sich an (über ShowDialog handelt es sich um ein modales Fenster).
            //     - Aber globale Styles, Templates (die Applikationsweit) können ohne das Application-Objekt nicht definiert und zugewiesen werden.
            //     - Über den Namespace Application.Current.... lassen sich applikationsweit Informationen abfragen, allerdings nur mit einem instanzierten Applications-Objekt.
            //var mainWindow = new MainWindow();
            //mainWindow.ShowDialog();
        }
    }
}
