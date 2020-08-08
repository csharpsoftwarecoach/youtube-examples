using Infrastructure.ViewModel;

namespace Model
{
    public class Person : Bindable
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _vorname;

        public string Vorname
        {
            get { return _vorname; }
            set { SetProperty(ref _vorname, value); }
        }

        public Person(string name, string vorname)
        {
            Name = name;
            Vorname = vorname;
        }

    }
}
