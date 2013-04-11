using System;
using System.Windows.Data;

namespace PixataCustomControls.Presentation.Controls {
  public class BooleanToBoldVC : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      return ((Value is bool && (bool)Value) ? "Bold" : "Normal");
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      throw new NotImplementedException();
    }
  }
}