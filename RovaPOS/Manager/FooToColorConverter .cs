using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RovaPOS.Manager
{
    class FooToColorConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new FooToColorConverter();
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int foo = (int)value;
            return
              foo == 1 ? Brushes.Blue :
              foo == 2 ? Brushes.Red :
              foo == 3 ? Brushes.Yellow :
              foo > 3 ? Brushes.Green :
                Brushes.Transparent;  // For foo<1
        }

       

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
