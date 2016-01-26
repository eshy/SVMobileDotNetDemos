using Cirrious.MvvmCross.ViewModels;

namespace GridViewAnimations.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

        private string _busyMessage;
        public string BusyMessage
        {
            get { return _busyMessage; }
            set { _busyMessage = value; RaisePropertyChanged(() => BusyMessage); }
        }
    }
}
