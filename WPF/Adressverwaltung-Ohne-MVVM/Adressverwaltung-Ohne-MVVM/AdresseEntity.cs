using System;

namespace Adressverwaltung_Ohne_MVVM
{
    /// <summary>
    /// Adresse mit den entsprechenden Properties (Eigenschaften)
    /// </summary>
    public class AdresseEntity
    {
        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public DateTime GeborenAm { get; set; }

        public string Familienstand { get; set; }

        public string HandyNummer { get; set; }

        public string EMail { get; set; }

        public string Strasse { get; set; }

        public string StrassenNummer { get; set; }

        public string Plz { get; set; }

        public string Ort { get; set; }
    }
}
