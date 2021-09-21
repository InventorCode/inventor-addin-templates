using System;
using System.Windows.Input;

namespace InvAddin.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand CustomCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public Action CloseAction { get; set; } //assign Window.Close() to this Action in the View

        public MainWindowViewModel()
        {
            CustomCommand = new RelayCommand(o => GreetingText = "Goodbye!", o => true);
            CloseCommand = new RelayCommand(o => CloseAction(), o => true);
        }

        private string _greetingText = "Hello World!";
        public string GreetingText
        {
            get { return _greetingText; }
            set
            {
                _greetingText = value;
                NotifyPropertyChanged("GreetingText");
            }
        }
    }
}