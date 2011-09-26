using System;
using System.Windows.Data;

namespace PixataCustomControls.Editors {
  /// <summary>
  /// DisplayNameConverter is a converter to append ':' to the property name.  
  /// The result is used inside the label to edit the property.
  /// It is important not to append ':' directly inside the property name.
  /// </summary>
  public class DisplayNameConverter : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      if (Value != null) {
        return String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}:", Value);
      }
      return String.Empty;
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      throw new NotSupportedException();
    }
  }
}
