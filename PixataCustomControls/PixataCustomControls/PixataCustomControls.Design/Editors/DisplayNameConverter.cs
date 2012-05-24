using System;
using System.Globalization;
using System.Windows.Data;

namespace PixataCustomControls.Editors {
#if !SILVERLIGHT
  [ValueConversion(typeof(string), typeof(string))]
#endif
  public class DisplayNameConverter : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture) {
      if (Value != null) {
        return String.Format(CultureInfo.CurrentCulture, "{0}:", Value);
      }
      return String.Empty;
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture) {
      throw new NotSupportedException();
    }
  }
}