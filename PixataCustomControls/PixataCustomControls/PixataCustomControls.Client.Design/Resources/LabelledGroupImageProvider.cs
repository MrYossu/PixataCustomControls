using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Media.Imaging;

using Microsoft.LightSwitch.BaseServices.ResourceService;

namespace PixataCustomControls.Resources {
  [Export(typeof(IResourceProvider))]
  [ResourceProvider("PixataCustomControls.LabelledGroup")]
  internal class LabelledGroupImageProvider : IResourceProvider {
    #region IResourceProvider Members

    public object GetResource(string resourceId, CultureInfo cultureInfo) {
      return new BitmapImage(new Uri("/PixataCustomControls.Client.Design;component/Resources/ControlImages/LabelledGroup.png", UriKind.Relative));
    }

    #endregion
  }
}