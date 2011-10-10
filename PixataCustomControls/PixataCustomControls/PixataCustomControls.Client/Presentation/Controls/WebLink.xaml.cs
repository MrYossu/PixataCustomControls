using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Resources;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Framework.Helpers;

namespace PixataCustomControls.Presentation.Controls {
  public partial class WebLink : UserControl {
    public WebLink() {
      InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
      IContentItem contentItem = (IContentItem)DataContext;
      WebLinkImage.Source = ImagePropertyHelper.GetImageSource(contentItem);
    }
  }

  [Export(typeof(IControlFactory))]
  [ControlFactory("PixataCustomControls:WebLink")]
  internal class WebLinkFactory : IControlFactory {
    #region IControlFactory Members

    public DataTemplate DataTemplate {
      get {
        if (null == this.dataTemplate) {
          this.dataTemplate = XamlReader.Load(WebLinkFactory.ControlTemplate) as DataTemplate;
        }
        return this.dataTemplate;
      }
    }

    public DataTemplate GetDisplayModeDataTemplate(IContentItem contentItem) {
      return null;
    }

    #endregion

    #region Private Fields

    private DataTemplate dataTemplate;

    #endregion

    #region Constants

    private const string ControlTemplate =
        "<DataTemplate" +
        " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"" +
        " xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"" +
        " xmlns:ctl=\"clr-namespace:PixataCustomControls.Presentation.Controls;assembly=PixataCustomControls.Client\">" +
        "<ctl:WebLink/>" +
        "</DataTemplate>";

    #endregion
  }
}
