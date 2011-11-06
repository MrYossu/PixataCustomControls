using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Framework.Helpers;

namespace PixataCustomControls.Presentation.Controls {
  public partial class MailLink : UserControl {
    public MailLink() {
      InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
      IContentItem contentItem = (IContentItem)DataContext;
      ImageSource imgSource = ImagePropertyHelper.GetImageSource(contentItem);
      if (imgSource != null) {
        MailLinkImage.Source = imgSource;
        MailLinkImage.Visibility = Visibility.Visible;
      } else {
        MailLinkImage.Visibility = Visibility.Collapsed;
      }
    }
  }

  [Export(typeof(IControlFactory))]
  [ControlFactory("PixataCustomControls:MailLink")]
  internal class MailLinkFactory : IControlFactory {
    #region IControlFactory Members

    public DataTemplate DataTemplate {
      get {
        if (null == this.dataTemplate) {
          this.dataTemplate = XamlReader.Load(MailLinkFactory.ControlTemplate) as DataTemplate;
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
        "<ctl:MailLink/>" +
        "</DataTemplate>";

    #endregion
  }
}
