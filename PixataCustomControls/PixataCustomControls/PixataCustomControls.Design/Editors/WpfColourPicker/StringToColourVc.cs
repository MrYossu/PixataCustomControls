using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PixataCustomControls.Editors.WpfColourPicker {
  public class StringToColourVc : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      Color returnValue = Colors.Orange;
      try {
        returnValue = (Color)(ColorConverter.ConvertFromString(value.ToString()));
      }
      catch {
      }
      return returnValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      if (value is Color) {
        return ((Color)value).ToString();
      }
      return "#ff00ff";
    }
  }
}