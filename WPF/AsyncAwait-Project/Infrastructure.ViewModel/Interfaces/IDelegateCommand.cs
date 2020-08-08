using System.Windows.Input;

namespace Infrastructure.ViewModel
{
    public interface IDelegateCommand : ICommand
    {
        void Update();
    }
}
