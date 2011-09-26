using System.Windows.Data;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.LightSwitch.Presentation;

namespace PixataCustomControls.Presentation.Controls {
  public partial class StylableStaticLabel : UserControl {
    public StylableStaticLabel() {
      InitializeComponent();
      SetBinding(StylableFontColourProperty, new Binding("Properties[PixataCustomControls:StylableStaticLabel/StylableFontColour]"));
    }

    // dependency property for the font colour
    public string StylableFontColour {
      get {
        return (string)GetValue(StylableFontColourProperty);
      }
      set {
        SetValue(StylableFontColourProperty, value);
      }
    }

    public static readonly DependencyProperty StylableFontColourProperty = DependencyProperty.Register("StylableFontColourProperty", typeof(string), typeof(StylableStaticLabel), new PropertyMetadata(OnStylableFontColourPropertyChanged));

    private static void OnStylableFontColourPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      // When the ContentItem is changed, reset the internal data binding.
      StylableStaticLabel styLbl = (StylableStaticLabel)d;
      styLbl.TheStylableStaticLabel.Foreground = new SolidColorBrush(ColourUtils.GetColourFromName(styLbl.StylableFontColour));
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
