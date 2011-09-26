using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Markup;
using Microsoft.LightSwitch.Designers.PropertyPages;
using Microsoft.LightSwitch.Designers.PropertyPages.UI;

namespace PixataCustomControls.Editors {
  [Export(typeof(IPropertyValueEditorProvider))]
  [PropertyValueEditorName("PixataCustomControls:AlignmentPropertyPicker")]
  [PropertyValueEditorType("System.String")]
  public class AlignmentPropertyPickerEditor : IPropertyValueEditorProvider {
    public IPropertyValueEditor GetEditor(IPropertyEntry entry) {
      return new Editor();
    }

    private class Editor : IPropertyValueEditor {
      public object Context {
        get {
          return null;
        }
      }

      // The DataTemplate is used by the screen designer to create the UI control on the property sheet.
      public DataTemplate GetEditorTemplate(IPropertyEntry entry) {
        return XamlReader.Parse(ControlTemplate) as DataTemplate;
      }
    }

    private const string ControlTemplate =
      "<DataTemplate" +
      " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"" +
      " xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"" +
      " xmlns:editors=\"clr-namespace:PixataCustomControls.Editors;assembly=PixataCustomControls.Design\">" +
      "   <editors:AlignmentPropertyPicker/>" +
      "</DataTemplate>";
  }
}