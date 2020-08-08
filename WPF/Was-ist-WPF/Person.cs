namespace Was_ist_WPF
{
    internal class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public Person(string vorname, string nachname)
        {
            Vorname = vorname;
            Nachname = nachname;
        }
    }
}

