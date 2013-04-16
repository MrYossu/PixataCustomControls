using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Framework.Helpers;

namespace PixataCustomControls.Presentation.Controls {
  public partial class ExpandablePanel : IContentVisual {
    private bool _isExpanded;

    public ExpandablePanel() {
      InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
      IContentItem contentItem = (IContentItem)DataContext;
      ImageSource userImageSource = ImagePropertyHelper.GetImageSource(contentItem);
      ExpanderImage.Source = userImageSource ?? DefaultImage.Source;
      if (ExpandOnCreated.IsChecked.HasValue && ExpandOnCreated.IsChecked.Value) {
        ExpandPanel();
      }
    }

    private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
      if (_isExpanded) {
        CollapsePanel();
      } else {
        ExpandPanel();
      }
    }

    public event EventHandler OnCollapsed;

    private void CollapsePanel() {
      rotateImageCollapse.Begin();
      VisualStateManager.GoToState(this, "Closed", true);
      _isExpanded = !_isExpanded;
      if (OnCollapsed != null) {
        OnCollapsed(this, new EventArgs());
      }
    }

    public event EventHandler OnExpanded;

    private void ExpandPanel() {
      rotateImageExpand.Begin();
      VisualStateManager.GoToState(this, "Open", true);
      _isExpanded = !_isExpanded;
      if (OnExpanded != null) {
        OnExpanded(this, new EventArgs());
      }
    }

    public bool IsExpanded {
      get {
        return _isExpanded;
      }
      set {
        if (value) {
          if (!_isExpanded) {
            ExpandPanel();
          }
        } else {
          if (_isExpanded) {
            CollapsePanel();
          }
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
  [ControlFactory("PixataCustomControls:ExpandablePanel")]
  internal class ExpandablePanelFactory : IControlFactory {
    #region IControlFactory Members

    public DataTemplate DataTemplate {
      get {
        if (null == this.dataTemplate) {
          this.dataTemplate = XamlReader.Load(ExpandablePanelFactory.ControlTemplate) as DataTemplate;
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
        "<ctl:ExpandablePanel/>" +
        "</DataTemplate>";

    #endregion
  }
}