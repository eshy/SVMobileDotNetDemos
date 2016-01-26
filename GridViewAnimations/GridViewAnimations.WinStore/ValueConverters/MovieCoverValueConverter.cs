using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace GridViewAnimations.WinStore.ValueConverters
{
    public class MovieCoverValueConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var cover = value as string;
            var bitmapImage = new BitmapImage();
            var uri = new Uri(string.Concat("ms-appx:///Assets/Covers/", cover));
            bitmapImage.UriSource = uri;
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
