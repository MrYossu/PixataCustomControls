using System;
using Microsoft.LightSwitch.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PixataCustomControls.Presentation.Controls {
  public class PixataToastHelper {
    private const int MaxTimeOut = 30000;
    private const int DefaultTimeOut = 4000;
    private const int MaxWidth = 400;
    private const int DefaultWidth = 300;
    private const int MaxHeight = 100;
    private const int DefaultHeight = 85;
    private static NotificationWindow nw;

    public static void ShowToast(string TitleText, string MessageText, PixataToastStyles ToastStyle = PixataToastStyles.Plain, int TimeOut = DefaultTimeOut, int Width = DefaultWidth, int Height = DefaultHeight) {
      Dispatchers.Main.BeginInvoke(() => {
        PixataToastControlInterface ctrl = (PixataToastControlInterface)GetToastControl(TitleText, MessageText, ToastStyle);
        ShowToast(TitleText, MessageText, ctrl, TimeOut, Width, Height);
      });
    }

    public static void ShowToast(string TitleText, string MessageText, PixataToastControlInterface ToastControl, int TimeOut = DefaultTimeOut, int Width = DefaultWidth, int Height = DefaultHeight) {
      if (Application.Current.IsRunningOutOfBrowser) {
        Dispatchers.Main.BeginInvoke(() => {
          nw = new NotificationWindow();
          try {
            // just in case there's already one showing
            nw.Close();
          }
          catch { }
          ToastControl.Closed += new EventHandler(ToastControl_Closed);
          nw.Width = Math.Max(Math.Min(Width, MaxWidth), 0);
          nw.Height = Math.Max(Math.Min(Height, MaxHeight), 0);
          ToastControl.TitleText = TitleText;
          ToastControl.MessageText = MessageText;
          nw.Content = (FrameworkElement)ToastControl;
          nw.Show(Math.Max(Math.Min(TimeOut, MaxTimeOut), 0));
        });
      }
    }

    static void ToastControl_Closed(object sender, EventArgs e) {
      nw.Close();
    }

    private static FrameworkElement GetToastControl(string TitleText, string MessageText, PixataToastStyles ToastStyle) {
      PixataToastControlInterface c;
      switch (ToastStyle) {
        case PixataToastStyles.InfoBlue:
          c = new PixataToastControlInfoBlue();
          break;
        default:
          c = new PixataToastControlPlain();
          break;
      }
      c.TitleText = TitleText;
      c.MessageText = MessageText;
      return (FrameworkElement)c;
    }

    public enum PixataToastStyles {
      Plain,
      InfoBlue
    }
  }
}
