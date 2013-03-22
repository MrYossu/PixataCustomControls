using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Markup;
using Microsoft.LightSwitch.Designers.PropertyPages;
using Microsoft.LightSwitch.RuntimeEdit;

namespace PixataCustomControls.Editors.OpacityEditor {
  public class OpacityPickerEditor {
    [Export(typeof(IPropertyValueEditorProvider))]
    [PropertyValueEditorName("PixataCustomControls:OpacityPicker")]
    [PropertyValueEditorType("System.String")]
    public class ClientOpacityPickerEditorEditorProvider : IPropertyValueEditorProvider {
      public IPropertyValueEditor GetEditor(IPropertyEntry entry) {
        return new Editor();
      }

      private class Editor : IPropertyValueEditor {
        public DataTemplate GetEditorTemplate(IPropertyEntry entry) {
          return (DataTemplate)XamlReader.Load(ControlTemplate);
        }
      }

      private const string ControlTemplate =
        "<DataTemplate" +
        " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"" +
        " xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"" +
        " xmlns:editors=\"clr-namespace:PixataCustomControls.Editors.OpacityEditor;assembly=PixataCustomControls.Client.Design\">" +
        "   <editors:ClientOpacityPicker/>" +
        "</DataTemplate>";
    }
  }
}