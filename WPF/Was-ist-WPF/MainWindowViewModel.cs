using System;
using System.Collections.Generic;

namespace Was_ist_WPF
{
    internal class MainWindowViewModel
    {
        public List<Person> Personen { get; set; } = new List<Person>();

        public MainWindowViewModel()
        {
            Personen.Add(new Person("Peter", "Huber"));
            Personen.Add(new Person("Anna", "Schweiger"));
            Personen.Add(new Person("Otto", "Kiesel"));
            Personen.Add(new Person("Gertrud", "Schmidtbauer"));
        }
    }
}
