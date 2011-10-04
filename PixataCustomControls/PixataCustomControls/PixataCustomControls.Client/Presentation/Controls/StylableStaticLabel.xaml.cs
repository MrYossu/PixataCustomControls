using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.LightSwitch.Presentation;

namespace PixataCustomControls.Presentation.Controls {
  public partial class StylableStaticLabel : UserControl, IContentVisual {
    public StylableStaticLabel() {
      InitializeComponent();
    }

    public object Control {
      get {
        return TheStylableStaticLabel;
      }
    }

    public void Show() {
    }
  }

  [Export(typeof(IControlFactory))]
  [ControlFactory("PixataCustomControls:StylableStaticLabel")]
  internal class StylableStaticLabelFactory : IControlFactory {
    #region IControlFactory Members

    public DataTemplate DataTemplate {
      get {
        if (null == this.dataTemplate) {
          this.dataTemplate = XamlReader.Load(StylableStaticLabelFactory.ControlTemplate) as DataTemplate;
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
      "<ctl:StylableStaticLabel/>" +
      "</DataTemplate>";

    #endregion
  }
}