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
  public partial class StaticToolbar : UserControl, IContentVisual {
    public StaticToolbar() {
      InitializeComponent();
    }

    private void UserControl_Loaded(object Sender, RoutedEventArgs E) {
      IContentItem contentItem = (IContentItem)DataContext;
      SetImage(contentItem, Image1, "Image1");
      SetImage(contentItem, Image2, "Image2");
      SetImage(contentItem, Image3, "Image3");
      SetImage(contentItem, Image4, "Image4");
      SetImage(contentItem, Image5, "Image5");
    }

    private void SetImage(IContentItem ContentItem, Image Image, string PropertyName) {
      string resourceId = (string)ContentItem.Properties["PixataCustomControls:StaticToolbar/" + PropertyName];
      if (string.IsNullOrEmpty(resourceId)) {
        Image.Visibility = Visibility.Collapsed;
      } else {
        IServiceProxy proxy = VsExportProviderService.GetServiceFromCache<IServiceProxy>();
        ImageSource source = (ImageSource)proxy.ResourceService.GetResource(resourceId, CultureInfo.CurrentCulture);
        Image.Source = source;
        Image.Visibility = Visibility.Visible;
      }
    }

    private void Button_Click1(object Sender, RoutedEventArgs E) {
      FireEvent(1, Button1.Tag.ToString());
    }

    private void Button_Click2(object Sender, RoutedEventArgs E) {
      FireEvent(2, Button2.Tag.ToString());
    }

    private void Button_Click3(object Sender, RoutedEventArgs E) {
      FireEvent(3, Button3.Tag.ToString());
    }

    private void Button_Click4(object Sender, RoutedEventArgs E) {
      FireEvent(4, Button4.Tag.ToString());
    }

    private void Button_Click5(object Sender, RoutedEventArgs E) {
      FireEvent(5, Button5.Tag.ToString());
    }

    public event EventHandler<StaticToolbarEventArgs> ButtonClick;

    private void FireEvent(int ButtonNumber, string TagText) {
      StaticToolbarEventArgs ev = new StaticToolbarEventArgs {
        ButtonNumber = ButtonNumber,
        TagText = TagText
      };
      if (ButtonClick != null) {
        ButtonClick(this, ev);
      }
    }

    public Button this[int ButtonNumber] {
      get {
        switch (ButtonNumber) {
          case 1:
            return Button1;
          case 2:
            return Button2;
          case 3:
            return Button3;
          case 4:
            return Button4;
          case 5:
            return Button5;
          default:
            throw new IndexOutOfRangeException("ButtonNumber must be between 1 and 5");
        }
      }
    }

    public void Show() {
    }

    public object Control {
      get {
        return this;
      }
    }
  }

  [Export(typeof(IControlFactory))]
  [ControlFactory("PixataCustomControls:StaticToolbar")]
  internal class StaticToolbarFactory : IControlFactory {
    #region IControlFactory Members

    public DataTemplate DataTemplate {
      get {
        return _dataTemplate ?? (_dataTemplate = XamlReader.Load(ControlTemplate) as DataTemplate);
      }
    }

    public DataTemplate GetDisplayModeDataTemplate(IContentItem ContentItem) {
      return null;
    }

    #endregion

    #region Private Fields

    private DataTemplate _dataTemplate;

    #endregion

    #region Constants

    private const string ControlTemplate =
      "<DataTemplate" +
      " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"" +
      " xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"" +
      " xmlns:ctl=\"clr-namespace:PixataCustomControls.Presentation.Controls;assembly=PixataCustomControls.Client\">" +
      "<ctl:StaticToolbar/>" +
      "</DataTemplate>";

    #endregion
  }
}