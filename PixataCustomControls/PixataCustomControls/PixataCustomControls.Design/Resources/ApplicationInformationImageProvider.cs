using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Media.Imaging;

using Microsoft.LightSwitch.BaseServices.ResourceService;

namespace PixataCustomControls.Resources {
  [Export(typeof(IResourceProvider))]
  [ResourceProvider("PixataCustomControls.ApplicationInformation")]
  internal class ApplicationInformationImageProvider : IResourceProvider {
    #region IResourceProvider Members

    public object GetResource(string resourceId, CultureInfo cultureInfo) {
      return new BitmapImage(new Uri("/PixataCustomControls.Design;component/Resources/ControlImages/ApplicationInformation.png", UriKind.Relative));
    }

    #endregion
  }
}