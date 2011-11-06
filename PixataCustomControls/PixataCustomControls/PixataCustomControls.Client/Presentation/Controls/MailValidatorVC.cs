using System;
using System.Windows.Data;

namespace PixataCustomControls.Presentation.Controls {
  public class MailValidatorVC : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      if (Value != null) {
        string mailLink = Value.ToString();
        if (!mailLink.StartsWith("mailto://")) {
          mailLink = "mailto://" + mailLink;
        }
        return mailLink;
      }
      return null;
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      throw new NotImplementedException();
    }

  }
}
