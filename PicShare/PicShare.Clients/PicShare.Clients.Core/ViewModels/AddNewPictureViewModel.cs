using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.PictureChooser;
using Cirrious.MvvmCross.ViewModels;
using PicShare.Clients.Core.Helpers;
using PicShare.Clients.Core.Services;

namespace PicShare.Clients.Core.ViewModels
{
    public class AddNewPictureViewModel : MvxViewModel
    {
        private IPicShareApiClient _apiClient;
        private IMvxPictureChooserTask _pictureChooserTask;
        public AddNewPictureViewModel(IPicShareApiClient apiClient, 
            IMvxPictureChooserTask pictureChooserTask)
        {
            _apiClient = apiClient;
            _pictureChooserTask = pictureChooserTask;
        }

        #region Properties

        private byte[] _pictureBytes;

        public byte[] PictureBytes
        {
            get { return _pictureBytes; }
            set
            {
                _pictureBytes = value;
                RaisePropertyChanged(() => PictureBytes);
            }
        }
        #endregion

        #region Life Cycle

        public override void Start()
        {
            base.Start();

            if (String.IsNullOrWhiteSpace(Settings.NickName))
            {
                ShowViewModel<SignUpViewModel>();
            }
        }

        #endregion

        #region Commands

        public ICommand TakePictureCommand
        {
            get { return new MvxCommand(TakePicture);}
        }

        private async void TakePicture()
        {
            var pictureStream = await _pictureChooserTask.TakePictureAsync(640, 70);

            var memoryStream = new MemoryStream();
            await pictureStream.CopyToAsync(memoryStream);
            PictureBytes = memoryStream.ToArray();
        }

        public ICommand UploadPictureCommand
        {
            get { return new MvxCommand(UploadPicture);}
        }

        private async void UploadPicture()
        {
            if (PictureBytes == null || PictureBytes.Length == 0)
            {
                return;
            }

            await _apiClient.UploadPictureAsync(PictureBytes);
            Close(this);
        }

        #endregion

    }
}
