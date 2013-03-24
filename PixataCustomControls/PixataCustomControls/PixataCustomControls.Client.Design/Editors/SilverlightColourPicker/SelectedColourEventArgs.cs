using System;
using System.Windows.Media;

namespace PixataCustomControls.Editors.SilverlightColourPicker {
  /// <summary>
  /// Delegate for the SelectedColourChanged event.
  /// </summary>
  /// <param name="sender">The object instance that fired the event.</param>
  /// <param name="e">The selected colour event arguments for the event.</param>
  public delegate void SelectedColourChangedHandler(object sender, SelectedColourEventArgs e);

  /// <summary>
  /// Delegate for the SelectedColourChanging event.
  /// </summary>
  /// <param name="sender">The object instance that fired the event.</param>
  /// <param name="e">The selected colour event arguments for the event.</param>
  public delegate void SelectedColourChangingHandler(object sender, SelectedColourEventArgs e);


  /// <summary>
  /// Event data for the SelectedColourChanged event.
  /// </summary>
  public class SelectedColourEventArgs : EventArgs {
    /// <summary>
    /// The currently selected colour.
    /// </summary>
    public readonly Color SelectedColour;

    /// <summary>
    /// Create a new instance of the SelectedColourEventArgs class.
    /// </summary>
    /// <param name="colour">The currently selected colour.</param>
    public SelectedColourEventArgs(Color colour) {
      SelectedColour = colour;
    }
  }
}