using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PixataCustomControls.Editors.WpfColourPicker {
  public class StringToColourVc : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      try {
        Debug.WriteLine("Converting " + value);
        return (Color)(ColorConverter.ConvertFromString(value.ToString()));
      }
      catch {
        return Colors.Orange;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      Debug.WriteLine("Converting back " + value);
      if (value is Color) {
        return ((Color)value).ToString();
      }
      return "#ff00ff";
    }
  }
}