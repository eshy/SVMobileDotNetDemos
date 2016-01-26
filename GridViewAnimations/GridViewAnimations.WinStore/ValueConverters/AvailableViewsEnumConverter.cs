using System;
using Windows.UI.Xaml.Data;
using GridViewAnimations.Core.ViewModels;

namespace GridViewAnimations.WinStore.ValueConverters
{
    public class AvailableViewsEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var enumValue = (HomeViewModel.AvailableViews)value;
            return enumValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
