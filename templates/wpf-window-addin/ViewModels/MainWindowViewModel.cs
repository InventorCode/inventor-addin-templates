using InvAddin;
using System;
using System.Threading.Tasks;

namespace InvAddin.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public event EventHandler OnRequestClose;

        private string greeting = "Hello World!";
        public string Greeting
        {
            get { return greeting; }
            set
            {
                greeting = value;
                NotifyPropertyChanged("ExportFileName");
            }
        }

        public MainWindowViewModel() { }

        public void WindowClose()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}