using System;
using System.Windows.Data;

namespace PixataCustomControls.Presentation.Controls {
  public class IntToBorderVC : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      string borderThickness = Value.ToString();
      string borderProperty = borderThickness + ", " + borderThickness + ", " + borderThickness + ", " + borderThickness;
      return borderProperty;
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      throw new NotImplementedException();
    }
  }
}