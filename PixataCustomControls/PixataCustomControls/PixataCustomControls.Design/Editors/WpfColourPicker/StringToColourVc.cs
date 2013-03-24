using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PixataCustomControls.Editors.WpfColourPicker {
  public class StringToColourVc : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      //try {
      //  Debug.WriteLine("Converting " + value);
      //  return (Color)(ColorConverter.ConvertFromString(value.ToString()));
      //}
      //catch {
      //  return Colors.Orange;
      //}

      string colStr = value.ToString();
      // TODO AYS this needs to cope with colours that don't have an opacity part (eg #ff8800) as well as colour names.
      try {
        if (colStr.StartsWith("#") && colStr.Length == 9) {
          colStr = colStr.Substring(1);
          byte alpha = HexToByte(colStr.Substring(0, 2));
          byte red = HexToByte(colStr.Substring(2, 2));
          byte green = HexToByte(colStr.Substring(4, 2));
          byte blue = HexToByte(colStr.Substring(6, 2));
          return Color.FromArgb(alpha, red, green, blue);
        }
      }
      catch {
      }
      return Colors.Orange;
    }

    private byte HexToByte(string hex) {
      return byte.Parse(hex, NumberStyles.HexNumber);
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