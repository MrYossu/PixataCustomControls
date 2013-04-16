using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PixataCustomControls.Editors.WpfColourPicker {
  public partial class WpfColourPicker {
    private StringToColourVc vc = new StringToColourVc();

    public WpfColourPicker() {
      InitializeComponent();
      TheColourPicker.SelectedColourChanged += TheColourPicker_SelectedColourChanged;
      ColourTb.Text = TheColourPicker.SelectedColour.ToString();
    }

    private void TheColourPicker_SelectedColourChanged(object sender, SelectedColourEventArgs e) {
      ColourTb.Text = TheColourPicker.SelectedColour.ToString();
    }

    private void ColourTb_OnTextChanged(object sender, TextChangedEventArgs e) {
      Color convertedColour = (Color)vc.Convert(ColourTb.Text, null, null, null);
      if (convertedColour != ColourPicker.DefaultColour) {
        ErrorTb.Visibility = Visibility.Collapsed;
        TheColourPicker.SelectedColour = convertedColour;
      } else {
        ErrorTb.Visibility = Visibility.Visible;
      }
    }
  }
}