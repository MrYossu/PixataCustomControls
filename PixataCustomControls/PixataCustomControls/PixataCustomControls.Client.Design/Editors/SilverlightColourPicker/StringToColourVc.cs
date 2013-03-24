using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PixataCustomControls.Editors.SilverlightColourPicker {
  public class StringToColourVc : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      try {
        return StringToColour(value.ToString());
      }
      catch {
        return Colors.Orange;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      if (value is Color) {
        return ((Color)value).ToString();
      }
      return "#ff00ff";
    }

    private static Color StringToColour(string colourName) {
      Line lne = (Line)XamlReader.Load("<Line xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" Fill=\"" + colourName + "\" />");
      return (Color)lne.Fill.GetValue(SolidColorBrush.ColorProperty);
    }
  }
}