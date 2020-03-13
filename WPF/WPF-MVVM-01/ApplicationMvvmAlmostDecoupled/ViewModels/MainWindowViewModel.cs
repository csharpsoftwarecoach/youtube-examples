using Models;
using Shared.Models;

namespace ViewModels
{
    public class MainWindowViewModel : ModelBase
    {
		private Person _privatePerson;

		public Person PrivatePerson
		{
			get
			{
				return _privatePerson;
			}
			set
			{
				if (_privatePerson != value)
				{
					_privatePerson = value;
					OnPropertyChanged();
				}
			}
		}

		public MainWindowViewModel()
		{
			PrivatePerson = new Person();
		}

	}
}
