using System;
using System.Windows.Data;

namespace PixataCustomControls.Presentation.Controls {
  public class BoolToVisibilityVC : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      return ((Value is bool && (bool)Value) ? "Visible" : "Collapsed");
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      throw new NotImplementedException();
    }
  }
}