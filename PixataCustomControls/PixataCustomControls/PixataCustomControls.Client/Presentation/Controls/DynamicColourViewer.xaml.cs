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

namespace PixataCustomControls.Presentation.Controls {
  public partial class DynamicColourViewer : UserControl {
    public DynamicColourViewer() {
      InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
      SetBlock();
    }

    private void TheInt_TextChanged(object sender, TextChangedEventArgs e) {
      SetBlock();
    }

    private void SetBlock() {
      int colourNumber = Int32.Parse(TheInt.Text);
      if (colourNumber > 0 && colourNumber <= 5) {
        IContentItem contentItem = (IContentItem)DataContext;
        string colour = (string)contentItem.Properties["PixataCustomControls:DynamicColourViewer/Colour" + colourNumber];
        if (!string.IsNullOrEmpty(colour)) {
          DynamicBlock.Fill = new SolidColorBrush(ColourFromString(colour));
        }
      }
    }

    public static Color ColourFromString(string colourName) {
      Line lne = (Line)XamlReader.Load("<Line xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" Fill=\"" + colourName + "\" />");
      return (Color)lne.Fill.GetValue(SolidColorBrush.ColorProperty);
    }
  }

  [Export(typeof(IControlFactory))]
  [ControlFactory("PixataCustomControls:DynamicColourViewer")]
  internal class DynamicColourViewerFactory : IControlFactory {
    #region IControlFactory Members

    public DataTemplate DataTemplate {
      get {
        if (null == this.dataTemplate) {
          this.dataTemplate = XamlReader.Load(DynamicColourViewerFactory.ControlTemplate) as DataTemplate;
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
        "<ctl:DynamicColourViewer/>" +
        "</DataTemplate>";

    #endregion
  }
}
