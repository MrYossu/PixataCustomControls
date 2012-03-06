using System;

namespace PixataCustomControls.Presentation.Controls {
  public interface PixataToastControlInterface {
    string TitleText { set; }
    string MessageText { set; }
    event EventHandler Closed;
  }
}
