using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Framework.Helpers;

namespace PixataCustomControls.Presentation.Controls {
  public partial class WebLink : UserControl, IContentVisual {
    public WebLink() {
      InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
      IContentItem contentItem = (IContentItem)DataContext;
      ImageSource imgSource = ImagePropertyHelper.GetImageSource(contentItem);
      if (imgSource != null) {
        WebLinkImage.Source = imgSource;
        WebLinkImage.Visibility = Visibility.Visible;
      } else {
        WebLinkImage.Visibility = Visibility.Collapsed;
      }
    }

    public void Show() {
    }

    public object Control {
      get {
        return TheWebLink;
      }
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