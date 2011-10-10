using System;
using System.Windows.Data;

namespace PixataCustomControls.Presentation.Controls {
  public class UrlValidatorVC : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      if (Value != null) {
        string url = Value.ToString();
        if (!url.StartsWith("http://") && !url.StartsWith("https://")) {
          url = "http://" + url;
        }
        return url;
      }
      return null;
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      throw new NotImplementedException();
    }
  }
}
