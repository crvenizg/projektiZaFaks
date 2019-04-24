using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp1
{
    [ValueConversion(typeof(object), typeof(int))]
    public class ColorRowClass : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = (int)System.Convert.ChangeType(value, typeof(int));
            if(number >=0 && number < 7)
            {
                return -1;
            }
            if(number >= 7 && number < 10)
            {
                return 0;
            }
            if(number < 0)
            {
                return -2;
            }
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
