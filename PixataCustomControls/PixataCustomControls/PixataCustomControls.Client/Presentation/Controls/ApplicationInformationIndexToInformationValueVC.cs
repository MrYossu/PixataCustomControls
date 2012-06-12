using System;
using System.Linq;
using System.Windows.Data;
using System.Xml.Linq;

namespace PixataCustomControls.Presentation.Controls {
  public class ApplicationInformationIndexToInformationValueVC : IValueConverter {
    public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      // Set this first, so that if we encoutner any problems below, we'll have the variable available to send back
      string informationValue = "";
      // Get the index of the combobox
      int informationIndex = -1;
      Int32.TryParse(Value.ToString(), out informationIndex);
      try {
        // The next three lines read the app name and version from the config file, and are wrapped in a try/catch block in case something goes wrong. This seems like a flaky way to do it to me, but Justin Anderson suggested it, so
        // presumably it has some sort of haskommo from Microsoft. I don't trust such code, which is why it's wrapped in the try/catch block :)
        // Get the config file
        XDocument appConfig = XDocument.Load("config.xml");
        // Pull out the application name
        string appName = appConfig.Descendants("ApplicationName").Single().Value;
        // Pull out the application version
        string[] appVersion = appConfig.Descendants("Version").Single().Value.Split(new[] {'.'});
        // Set the information
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
      }
      catch {
      }
      return informationValue;
    }

    public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture) {
      throw new NotImplementedException();
    }
  }
}