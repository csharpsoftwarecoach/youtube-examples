using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace Infrastructure.ViewModel
{
    public abstract class ViewModel : ValidationBindable, IViewModel
    {
        protected ViewModel()
        {
            var collections = this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => typeof(INotifyCollectionChanged).IsAssignableFrom(p.PropertyType))
                .Select(p => p.GetValue(this, null))
                .OfType<INotifyCollectionChanged>();

        }


    }
}
