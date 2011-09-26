using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Markup;
using Microsoft.LightSwitch.Designers.PropertyPages;
using Microsoft.LightSwitch.Designers.PropertyPages.UI;

namespace PixataCustomControls.Editors {
  /// <summary>
  /// EntityPropertyPickerProvider is a component to allow LightSwitch designers in Visual Studio 
  /// to create a property value editor.
  /// The name of the editor is specified in a PropertyValueEditorName attribute.  
  /// When it is needed, a designer will use the
  /// EditorTemplate to create a WPF control, which can be hosted inside a property sheet window.
  /// The DataContext of this control will be an IBindablePropertyEntry object.  
  /// Through the DataContext, the control can update the property value.
  /// </summary>
  [Export(typeof(IPropertyValueEditorProvider))]
  [PropertyValueEditorName("PixataCustomControls:EntityPropertyPicker")]
  [PropertyValueEditorType("System.String")]
  public class EntityPropertyPickerProvider : IPropertyValueEditorProvider {
    public IPropertyValueEditor GetEditor(IPropertyEntry entry) {
      return new Editor();
    }

    private class Editor : IPropertyValueEditor {
      public object Context {
        get {
          // A design-time editor allows an additional Context object, 
          // which is exposed through IBindablePropertyEntry.EditorContext.
          // This allows the editor to have additional status.
          // However, the run-time designer does not support it.  
          // It is not used in this sample.
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
      "   <editors:EntityPropertyPicker/>" +
      "</DataTemplate>";
  }
}