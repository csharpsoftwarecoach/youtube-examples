using Infrastructure.ViewModel;
using Model;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels
{
    public class MainViewModel : ViewModel
    {
        public ICommand StartAsyncCommand { get; }

        private Person _person;

        public Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }


        private string _statusText;

        public string StatusText
        {
            get { return _statusText; }
            set { SetProperty(ref _statusText, value); }
        }

        public MainViewModel()
        {
            StartAsyncCommand = new DelegateCommandAsyncProgress<object>(StartAsyncCommandCallback, ReportCallback);
        }

        private void ReportCallback(object obj)
        {
            
        }

        private async Task StartAsyncCommandCallback(Action<object> reportAction, CancellationTokenSource token)
        {
            //reportAction?.Invoke(null);

            await AsyncCall();
        }

        private void LoadPerson(object sender)
        {
            Person = new Person("Huber", "Anton");
            Task.Delay(1000);
            StatusText = "Done!";
        }

        private async Task AsyncCall()
        {
            StatusText = "Waiting...";
            await new Task(() => LoadPerson(null)).ConfigureAwait(false);
            StatusText = "Done!";
        }
    }
}
