using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.LightSwitch.Designers.PropertyPages.ViewModel;

namespace PixataCustomControls.Editors {
  public partial class EntityPropertyPicker : UserControl {
    // This property is created because it allows us to raise "OnExplorerControlDataContextPropertyChanged" when the DataContext is updated
    public static readonly DependencyProperty ExplorerControlDataContextProperty = DependencyProperty.Register("DummyProperty", typeof(IBindablePropertyEntry), typeof(EntityPropertyPicker), new PropertyMetadata(OnExplorerControlDataContextPropertyChanged));

    public EntityPropertyPicker() {
      InitializeComponent();
      SetBinding(ExplorerControlDataContextProperty, new Binding());
    }

    private static void OnExplorerControlDataContextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      EntityPropertyPicker EC = (EntityPropertyPicker)d;
      //IBindablePropertyEntry contentItem = (IBindablePropertyEntry)EC.DataContext;
      EC.cbComboBox.Items.Add("Black");
      EC.cbComboBox.Items.Add("Blue");
      EC.cbComboBox.Items.Add("Brown");
      EC.cbComboBox.Items.Add("CornflowerBlue");
      EC.cbComboBox.Items.Add("Crimson");
      EC.cbComboBox.Items.Add("Cyan");
      EC.cbComboBox.Items.Add("DarkBlue");
      EC.cbComboBox.Items.Add("DarkViolet");
      EC.cbComboBox.Items.Add("DeepPink");
      EC.cbComboBox.Items.Add("DeepSkyBlue");
      EC.cbComboBox.Items.Add("ForestGreen");
      EC.cbComboBox.Items.Add("Gold");
      EC.cbComboBox.Items.Add("Gray");
      EC.cbComboBox.Items.Add("Green");
      EC.cbComboBox.Items.Add("LawnGreen");
      EC.cbComboBox.Items.Add("Lime");
      EC.cbComboBox.Items.Add("Magenta");
      EC.cbComboBox.Items.Add("Navy");
      EC.cbComboBox.Items.Add("Olive");
      EC.cbComboBox.Items.Add("Orange");
      EC.cbComboBox.Items.Add("OrangeRed");
      EC.cbComboBox.Items.Add("Red");
      EC.cbComboBox.Items.Add("White");
    }
  }
}