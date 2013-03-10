using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Sdk.Proxy;
using Microsoft.VisualStudio.ExtensibilityHosting;

namespace PixataCustomControls.Presentation.Controls {
  public partial class DynamicImageViewer {
    public DynamicImageViewer() {
      InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
      SetImage();
    }

    private void TheInt_TextChanged(object sender, TextChangedEventArgs e) {
      SetImage();
    }

    private void SetImage() {
      int imageNumber = Int32.Parse(TheInt.Text);
      if (imageNumber > 0 && imageNumber <= 5) {
        IContentItem contentItem = (IContentItem)DataContext;
        string imageResourceId = (string)contentItem.Properties["PixataCustomControls:DynamicImageViewer/Image" + imageNumber];
        if (!string.IsNullOrEmpty(imageResourceId)) {
          IServiceProxy proxy = VsExportProviderService.GetServiceFromCache<IServiceProxy>();
          ImageSource source = (ImageSource)proxy.ResourceService.GetResource(imageResourceId, CultureInfo.CurrentCulture);
          DynamicImage.Source = source;
        } else {
          DynamicImage.Source = null;
        }
        string tooltip = (string)contentItem.Properties["PixataCustomControls:DynamicImageViewer/ToolTip" + imageNumber];
        TheToolTip.Content = (tooltip == "" ? null : tooltip);
      }
    }
  }

  [Export(typeof(IControlFactory))]
  [ControlFactory("PixataCustomControls:DynamicImageViewer")]
  internal class DynamicImageViewerFactory : IControlFactory {
    #region IControlFactory Members

    public DataTemplate DataTemplate {
      get {
        if (null == this.dataTemplate) {
          this.dataTemplate = XamlReader.Load(DynamicImageViewerFactory.ControlTemplate) as DataTemplate;
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
        "<ctl:DynamicImageViewer/>" +
        "</DataTemplate>";

    #endregion
  }
}