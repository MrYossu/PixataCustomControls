using System;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Client;

namespace PixataCustomControls.Presentation.Controls {
  public class InformationIndexToInformationValueVC : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      // Get the index of the combobox
      int informationIndex = -1;
      Int32.TryParse(Value.ToString(), out informationIndex);
      // Get the config file
      XDocument appConfig = XDocument.Load("config.xml");
      // Pull out the application name
      string appName = appConfig.Descendants("ApplicationName").Single().Value;
      // Pull out the application version
      string[] appVersion = appConfig.Descendants("Version").Single().Value.Split(new[] { '.' });
      // Set the information
      string informationValue = "";
      switch (informationIndex) {
        case 0:
          informationValue = appVersion[0];
          break;
        case 1:
          informationValue = appVersion[0] + "." + appVersion[1];
          break;
        case 2:
          informationValue = appVersion[0] + "." + appVersion[1] + "." + appVersion[2];
          break;
        case 3:
          informationValue = appName;
          break;
        case 4:
          informationValue = appName + " v" + appVersion[0];
          break;
        case 5:
          informationValue = appName + " v" + appVersion[0] + "." + appVersion[1];
          break;
        case 6:
          informationValue = appName + " v" + appVersion[0] + "." + appVersion[1] + "." + appVersion[2];
          break;
      }
      return informationValue;
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      throw new NotImplementedException();
    }
  }
}