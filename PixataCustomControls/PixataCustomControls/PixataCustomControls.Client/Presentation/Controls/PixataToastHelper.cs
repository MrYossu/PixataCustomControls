using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
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
    private static Queue<Tuple<PixataToastControlInterface, PixataToastControlInformation>> _toastQueue = new Queue<Tuple<PixataToastControlInterface, PixataToastControlInformation>>();
    private static bool _showingToast;

    public static void ClearToastQueue() {
      _toastQueue.Clear();
    }

    public static void ShowToast(string TitleText, string MessageText, PixataToastStyles ToastStyle = PixataToastStyles.Plain, int TimeOut = DefaultTimeOut, int Width = DefaultWidth, int Height = DefaultHeight) {
      Dispatchers.Main.BeginInvoke(() => {
        PixataToastControlInterface ctrl = (PixataToastControlInterface)GetToastControl(TitleText, MessageText, ToastStyle);
        ShowToast(TitleText, MessageText, ctrl, TimeOut, Width, Height);
      });
    }

    public static void ShowToast(string TitleText, string MessageText, PixataToastControlInterface ToastControl, int TimeOut = DefaultTimeOut, int Width = DefaultWidth, int Height = DefaultHeight) {
      if (Application.Current.IsRunningOutOfBrowser) {
        Dispatchers.Main.BeginInvoke(() => {
          ToastControl.TitleText = TitleText;
          ToastControl.MessageText = MessageText;
          PixataToastControlInformation info = new PixataToastControlInformation {
            Height = Math.Max(Math.Min(Height, MaxHeight), 0),
            Width = Width = Math.Max(Math.Min(Width, MaxWidth), 0),
            TimeOut = Math.Max(Math.Min(TimeOut, MaxTimeOut), 0)
          };
          _toastQueue.Enqueue(new Tuple<PixataToastControlInterface, PixataToastControlInformation>(ToastControl, info));
          ShowNextToastInQueue();
        });
      }
    }

    private static void ShowNextToastInQueue() {
      if (_toastQueue.Count > 0 && !_showingToast) {
        nw = new NotificationWindow();
        nw.Closed += new EventHandler(nw_Closed);
        Tuple<PixataToastControlInterface, PixataToastControlInformation> t = _toastQueue.Dequeue();
        // Set up the toast
        PixataToastControlInterface toast = t.Item1;
        toast.Closed += new EventHandler(ToastControl_Closed);
        nw.Content = (FrameworkElement)toast;
        // Set up the meta info
        PixataToastControlInformation info = t.Item2;
        nw.Width = info.Width;
        nw.Height = info.Height;
        // Now show it
        _showingToast = true;
        nw.Show(info.TimeOut);
      }
    }

    static void nw_Closed(object sender, EventArgs e) {
      _showingToast = false;
      ShowNextToastInQueue();
    }

    static void ToastControl_Closed(object sender, EventArgs e) {
      nw.Close();
    }

    private static FrameworkElement GetToastControl(string TitleText, string MessageText, PixataToastStyles ToastStyle) {
      PixataToastControlInterface c;
      switch (ToastStyle) {
        case PixataToastStyles.Info:
          c = new PixataToastControlInfoBlue();
          break;
        case PixataToastStyles.Warning:
          c = new PixataToastControlWarningYellow();
          break;
        case PixataToastStyles.Alert:
          c = new PixataToastControlAlert();
          break;
        case PixataToastStyles.Error:
          c = new PixataToastControlErrorRed();
          break;
        case PixataToastStyles.Elephant:
          c = new PixataToastControlElephant();
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
      Plain, // no icon
      Info, // blue circle "i"
      Warning, // yellow triangle
      Alert, // bell
      Error, //red circle "!"
      Elephant // elephant (just don't ask!)
    }

    private class PixataToastControlInformation {
      // Helper class used to store info about the toast. Used when we add the toast to the queue
      public int TimeOut { get; set; }
      public int Width { get; set; }
      public int Height { get; set; }
    }
  }
}
