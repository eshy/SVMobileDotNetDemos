using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using GridViewAnimations.Core.Models;
using GridViewAnimations.Core.Services;

namespace GridViewAnimations.Core.ViewModels
{
    public class HomeViewModel 
        : BaseViewModel
    {
        private readonly IMovieDataService _movieDataService;

        public HomeViewModel(IMovieDataService movieDataService)
        {
            _movieDataService = movieDataService;
            Movies = new ObservableCollection<MovieModel>();
        }

        #region Properties

        public ObservableCollection<MovieModel> Movies { get; set; }

        private MovieModel _selectedItem;
        public MovieModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        private AvailableViews _selectedView;
        public AvailableViews SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                RaisePropertyChanged(() => SelectedView);
            }
        }


        #endregion

        #region Life Cycle

        public override void Start()
        {
            base.Start();
            LoadMovies();
            SelectedView = AvailableViews.SmallTiles;
        }

        private async void LoadMovies()
        {
            BusyMessage = "Loading Movies...";
            IsBusy = true;
            Movies.Clear();
            var movies = _movieDataService.GetMovies();
            foreach (var movie in movies)
            {
                Movies.Add(movie);
                //await Task.Delay(500); //This is just for testing item added animation
            }
            IsBusy = false;
        }

        #endregion

        #region Commands

        public ICommand SwitchSelectedViewCommand => new MvxCommand(SwitchSelectedView);
        private void SwitchSelectedView()
        {
            //SelectedView = SelectedView == 1 ? 2 : 1;
            SelectedView = SelectedView == AvailableViews.SmallTiles ? AvailableViews.BigTiles : AvailableViews.SmallTiles;
        }

        #endregion

        public enum AvailableViews
        {
            SmallTiles=0,
            BigTiles=1
        }
    }
}