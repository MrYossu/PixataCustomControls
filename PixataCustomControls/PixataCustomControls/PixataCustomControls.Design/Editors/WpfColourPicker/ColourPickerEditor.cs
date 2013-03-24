using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Markup;
using Microsoft.LightSwitch.Designers.PropertyPages;
using Microsoft.LightSwitch.Designers.PropertyPages.UI;

namespace PixataCustomControls.Editors.WpfColourPicker {
  [Export(typeof(IPropertyValueEditorProvider))]
  [PropertyValueEditorName("PixataCustomControls:PixataColourPicker")]
  [PropertyValueEditorType("System.String")]
  public class ColourPickerEditor : IPropertyValueEditorProvider {
    public IPropertyValueEditor GetEditor(IPropertyEntry Entry) {
      return new Editor();
    }

    private class Editor : IPropertyValueEditor {
      public object Context {
        get {
          return null;
        }
      }

      public DataTemplate GetEditorTemplate(IPropertyEntry Entry) {
        return XamlReader.Parse(ControlTemplate) as DataTemplate;
      }
    }

    private const string ControlTemplate =
      "<DataTemplate" +
      " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"" +
      " xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"" +
      " xmlns:editors=\"clr-namespace:PixataCustomControls.Editors.WpfColourPicker;assembly=PixataCustomControls.Design\">" +
      "   <editors:WpfColourPicker />" +
      "</DataTemplate>";
  }
}