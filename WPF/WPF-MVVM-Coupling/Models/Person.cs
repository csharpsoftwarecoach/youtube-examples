using Shared.Models;

namespace Models
{
    public class Person : ModelBase
    {
		private string _name;

		public string Name
		{
			get 
			{ 
				return _name; 
			}
			set 
			{
				if (_name != value)
				{
					_name = value;
					OnPropertyChanged();
				}
			}
		}

	}
}
