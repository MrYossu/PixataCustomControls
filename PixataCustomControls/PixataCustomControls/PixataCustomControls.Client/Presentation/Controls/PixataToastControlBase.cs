using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PixataCustomControls.Presentation.Controls {
  public class PixataToastControlBase : UserControl, PixataToastControlInterface {
    protected void CloseMe() {
      if (Closed != null) {
        Closed(this, EventArgs.Empty);
      }
    }

    public virtual string TitleText {
      set { }
    }

    public virtual string MessageText {
      set { }
    }

    public event EventHandler Closed;
  }
}
