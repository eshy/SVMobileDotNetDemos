using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

            await LoadFirstPage();
        }

        //Call LoadFirstPage when the view is loaded the first time and when refreshing the list (clears the list and loads page 1)
        private async Task LoadFirstPage()
        {
            CurrentPage = 1;
            await LoadListItems();
        }

        private async Task LoadListItems()
        {
            var pictures = await _apiClient.GetPicturesAsync(CurrentPage, PageSize);

            //if loading the first page, clear the list, if not, just add the items to the existing list
            if (CurrentPage == 1)
            {
                ListItems.Clear();
            }

            foreach (var picture in pictures)
            {
                ListItems.Add(picture);
            }
        }

        #endregion

        #region Commands

        public ICommand AddPictureCommand
        {
            get { return new MvxCommand(AddPicture); }
        }

        private void AddPicture()
        {
            ShowViewModel<AddNewPictureViewModel>();
        }

        //TODO: Create commands for loading the next/previous pages

        #endregion
    }
}
