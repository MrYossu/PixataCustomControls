using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PixataCustomControls.Presentation.Controls {
  public class EmptyStringToVisibilityVC : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      if (Value != null && Value.ToString() != "") {
        return "Visible";
      }
      return "Collapsed";
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      throw new NotImplementedException();
    }
  }
}
