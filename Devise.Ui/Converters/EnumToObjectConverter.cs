using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Devise.Ui.Converters
{
    public class EnumToObjectConverter : IValueConverter
    {
        #region Properties
        
        public object MatchedObject { get; set; }
        
        public object UnmatchedObject { get; set; }

        #endregion // Properties

        #region Members
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string)
            {
                return value.ToString() == (parameter as string) ? MatchedObject : UnmatchedObject;
            }
            var enumerable = parameter as IEnumerable;
            if (enumerable != null)
            {
                return enumerable.Cast<object>().Contains(value) ? MatchedObject : UnmatchedObject;
            }
            return value == parameter ? MatchedObject : UnmatchedObject;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
