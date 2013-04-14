using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PixataCustomControls.Editors.SilverlightColourPicker {
  public partial class SilverlightColourPicker {
    private StringToColourVc vc = new StringToColourVc();

    public SilverlightColourPicker() {
      InitializeComponent();
      TheColourPicker.SelectedColourChanged += TheColourPicker_SelectedColourChanged;
      ErrorTb.Visibility = Visibility.Collapsed;
      ColourTb.Text = TheColourPicker.SelectedColour.ToString();
    }

    private void TheColourPicker_SelectedColourChanged(object sender, SelectedColourEventArgs e) {
      ColourTb.Text = TheColourPicker.SelectedColour.ToString();
    }

    private void ColourTb_OnTextChanged(object sender, TextChangedEventArgs e) {
      Color convertedColour = (Color)vc.Convert(ColourTb.Text, null, null, null);
      if (convertedColour != Colors.Orange) {
        ErrorTb.Visibility = Visibility.Collapsed;
        TheColourPicker.SelectedColour = convertedColour;
      } else {
        ErrorTb.Visibility = Visibility.Visible;
      }
    }
  }
}