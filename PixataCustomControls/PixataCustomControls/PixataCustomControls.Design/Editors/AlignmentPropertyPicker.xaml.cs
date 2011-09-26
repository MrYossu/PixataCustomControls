using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.LightSwitch.Designers.PropertyPages.ViewModel;

namespace PixataCustomControls.Editors {
  public partial class AlignmentPropertyPicker : UserControl {
    public static readonly DependencyProperty ExplorerControlDataContextProperty = DependencyProperty.Register("DummyProperty", typeof(IBindablePropertyEntry), typeof(AlignmentPropertyPicker), new PropertyMetadata(OnExplorerControlDataContextPropertyChanged));

    public AlignmentPropertyPicker() {
      InitializeComponent();
      SetBinding(ExplorerControlDataContextProperty, new Binding());
    }

    private static void OnExplorerControlDataContextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      AlignmentPropertyPicker EC = (AlignmentPropertyPicker)d;
      EC.cbComboBox.Items.Add("Left");
      EC.cbComboBox.Items.Add("Center");
      EC.cbComboBox.Items.Add("Right");
    }
  }
}