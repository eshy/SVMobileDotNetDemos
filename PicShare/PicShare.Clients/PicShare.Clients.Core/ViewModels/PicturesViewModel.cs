using System.Collections.ObjectModel;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using PicShare.Clients.Core.Model;
using PicShare.Clients.Core.Services;

namespace PicShare.Clients.Core.ViewModels
{
    public class PicturesViewModel 
		: MvxViewModel
    {
        private IPicShareApiClient _apiClient;

        public PicturesViewModel(IPicShareApiClient apiClient)
        {
            _apiClient = apiClient;

            ListItems = new ObservableCollection<PictureModel>();
        }

        #region Properties

        public ObservableCollection<PictureModel> ListItems { get; set; }

        public int CurrentPage { get; set; }

        private const int PageSize = 25;

        #endregion

        #region Life Cycle

        public override async void Start()
        {
            base.Start();

            CurrentPage = 1;
            var pictures = await _apiClient.GetPicturesAsync(CurrentPage, PageSize);
            
            ListItems.Clear();
            foreach (var picture in pictures)
            {
                ListItems.Add(picture);
            }
        }

        #endregion

        #region Commands

        public ICommand AddPictureCommand
        {
            get { return new MvxCommand(TakePicture); }
        }

        private void TakePicture()
        {
            ShowViewModel<AddNewPictureViewModel>();
        }


        #endregion
    }
}
