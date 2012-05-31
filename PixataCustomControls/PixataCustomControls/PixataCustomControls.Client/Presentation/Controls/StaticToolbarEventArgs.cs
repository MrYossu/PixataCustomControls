using System;

namespace PixataCustomControls.Presentation.Controls {
  public class StaticToolbarEventArgs : EventArgs {
    public int ButtonNumber { get; set; }
    public string TagText { get; set; }
  }
}