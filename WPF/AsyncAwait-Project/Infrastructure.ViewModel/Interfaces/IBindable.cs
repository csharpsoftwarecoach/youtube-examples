using System.ComponentModel;

namespace Infrastructure.ViewModel
{
    /// <inheritdoc cref="INotifyPropertyChanged" />
    /// <summary> Notifies clients that a property value is changing or has changed. </summary>
    public interface IBindable : INotifyPropertyChanging, INotifyPropertyChanged
    {
    }
}
