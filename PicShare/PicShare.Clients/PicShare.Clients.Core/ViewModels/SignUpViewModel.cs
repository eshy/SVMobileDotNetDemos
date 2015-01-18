using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using PicShare.Clients.Core.Services;

namespace PicShare.Clients.Core.ViewModels
{
    public class SignUpViewModel : MvxViewModel
    {
        private IPicShareApiClient _apiClient;

        public SignUpViewModel(IPicShareApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        #region Properties

        private string _nickName;
        public string NickName
        {
            get { return _nickName; }
            set
            {
                _nickName = value;
                RaisePropertyChanged(() => NickName);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        #endregion

        #region Commands

        public ICommand SignUpCommand
        {
            get { return new MvxCommand(SignUp); }
        }

        private async void SignUp()
        {
            try
            {
                await _apiClient.CreateAccountAsync(NickName, Password);
                Close(this);
            }
            catch (Exception ex)
            {
                
            }
        }

        #endregion
    }
}
