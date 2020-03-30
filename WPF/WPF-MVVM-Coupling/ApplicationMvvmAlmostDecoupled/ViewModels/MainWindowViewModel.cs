using Models;
using Shared.Models;
using System.Windows;
using System.Windows.Input;

namespace ViewModels
{
    public class MainWindowViewModel : ModelBase
    {
		public ICommand StartCommand { get; }

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

			StartCommand = new DelegateCommand(StartCommandCallback);
		}

		private void StartCommandCallback(object obj)
		{
			MessageBox.Show($"Guten Tag Frau/Herr {PrivatePerson.Name}!", "Hinweis");
		}

	}
}
