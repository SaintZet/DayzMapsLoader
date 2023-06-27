using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace DayzMapsLoader.Presentation.Wpf.Converters
{
    public class BooleanToCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isBusy && isBusy)
                return Cursors.Wait;
            
            return Cursors.Arrow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}