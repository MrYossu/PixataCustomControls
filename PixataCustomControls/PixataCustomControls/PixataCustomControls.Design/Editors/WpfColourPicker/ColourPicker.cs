using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PixataCustomControls.Editors.WpfColourPicker {
  /// <summary>
  /// Represents a Colour Picker control which allows a user to select a colour.
  /// </summary>
  public class ColourPicker : Control {
    /// <summary>
    /// Event fired when the selected colour changes.  This event occurs when the 
    /// left-mouse button is lifted after clicking.
    /// </summary>
    public event SelectedColourChangedHandler SelectedColourChanged;

    public static Color DefaultColour = Colors.DarkGray;

    /// <summary>
    /// Event fired when the selected colour is changing.  This event occurs when the 
    /// left-mouse button is pressed and the user is moving the mouse.
    /// </summary>
    public event SelectedColourChangingHandler SelectedColourChanging;

    private readonly ColourSpace _mColourSpace;
    private bool m_hueMonitorMouseCaptured;
    private bool m_sampleMouseCaptured;
    private double m_huePos;
    private double m_sampleX;
    private double m_sampleY;

    private Panel m_rootElement;
    private Rectangle m_hueMonitor;
    private Canvas m_sampleSelector;
    private Canvas m_hueSelector;

    private Rectangle m_selectedColourView;
    private Rectangle m_colourSample;
    private TextBlock m_hexValue;
    private ScaleTransform m_scale;

    /// <summary>
    /// Create a new instance of the ColourPicker control.
    /// </summary>
    public ColourPicker() {
      DefaultStyleKey = typeof(ColourPicker);
      _mColourSpace = new ColourSpace();
    }

    /// <summary>
    /// Builds the visual tree for the ColourPicker control when the template is applied. 
    /// </summary>
    public override void OnApplyTemplate() {
      base.OnApplyTemplate();

      m_rootElement = GetTemplateChild("RootElement") as Panel;
      m_hueMonitor = GetTemplateChild("HueMonitor") as Rectangle;
      m_sampleSelector = GetTemplateChild("SampleSelector") as Canvas;
      m_hueSelector = GetTemplateChild("HueSelector") as Canvas;
      m_selectedColourView = GetTemplateChild("SelectedColourView") as Rectangle;
      m_colourSample = GetTemplateChild("ColourSample") as Rectangle;
      m_hexValue = GetTemplateChild("HexValue") as TextBlock;


      m_rootElement.RenderTransform = m_scale = new ScaleTransform();

      m_hueMonitor.MouseLeftButtonDown += rectHueMonitor_MouseLeftButtonDown;
      m_hueMonitor.MouseLeftButtonUp += rectHueMonitor_MouseLeftButtonUp;
      m_hueMonitor.MouseMove += rectHueMonitor_MouseMove;

      m_colourSample.MouseLeftButtonDown += rectSampleMonitor_MouseLeftButtonDown;
      m_colourSample.MouseLeftButtonUp += rectSampleMonitor_MouseLeftButtonUp;
      m_colourSample.MouseMove += rectSampleMonitor_MouseMove;

      m_sampleX = m_colourSample.Width;
      m_sampleY = 0;
      m_huePos = 0;

      UpdateVisuals();
    }

    private void rectHueMonitor_MouseLeftButtonDown(object sender, MouseEventArgs e) {
      m_hueMonitorMouseCaptured = m_hueMonitor.CaptureMouse();
      DragSliders(0, e.GetPosition((UIElement)sender).Y);
    }

    private void rectHueMonitor_MouseLeftButtonUp(object sender, MouseEventArgs e) {
      m_hueMonitor.ReleaseMouseCapture();
      m_hueMonitorMouseCaptured = false;
      SetValue(SelectedColourProperty, GetColour());
    }

    private void rectHueMonitor_MouseMove(object sender, MouseEventArgs e) {
      DragSliders(0, e.GetPosition((UIElement)sender).Y);
    }

    private void rectSampleMonitor_MouseLeftButtonDown(object sender, MouseEventArgs e) {
      m_sampleMouseCaptured = m_colourSample.CaptureMouse();
      Point pos = e.GetPosition((UIElement)sender);
      DragSliders(pos.X, pos.Y);
    }

    private void rectSampleMonitor_MouseLeftButtonUp(object sender, MouseEventArgs e) {
      m_colourSample.ReleaseMouseCapture();
      m_sampleMouseCaptured = false;
      SetValue(SelectedColourProperty, GetColour());
    }

    private void rectSampleMonitor_MouseMove(object sender, MouseEventArgs e) {
      if (!m_sampleMouseCaptured)
        return;

      Point pos = e.GetPosition((UIElement)sender);
      DragSliders(pos.X, pos.Y);
    }


    private Color GetColour() {
      double yComponent = 1 - (m_sampleY/m_colourSample.Height);
      double xComponent = m_sampleX/m_colourSample.Width;
      double hueComponent = (m_huePos/m_hueMonitor.Height)*360;

      return _mColourSpace.ConvertHsvToRgb(hueComponent, xComponent, yComponent);
    }

    private void UpdateSatValSelection() {
      if (m_colourSample == null)
        return;

      m_sampleSelector.SetValue(Canvas.LeftProperty, m_sampleX - (m_sampleSelector.Height/2));
      m_sampleSelector.SetValue(Canvas.TopProperty, m_sampleY - (m_sampleSelector.Height/2));

      Color currColour = GetColour();
      m_selectedColourView.Fill = new SolidColorBrush(currColour);
      m_hexValue.Text = _mColourSpace.GetHexCode(currColour);

      FireSelectedColourChangingEvent(currColour);
    }

    private void UpdateHueSelection() {
      if (m_hueMonitor == null)
        return;
      double huePos = m_huePos/m_hueMonitor.Height*255;
      Color c = _mColourSpace.GetColourFromPosition(huePos);
      m_colourSample.Fill = new SolidColorBrush(c);

      m_hueSelector.SetValue(Canvas.TopProperty, m_huePos - (m_hueSelector.Height/2));

      Color currColour = GetColour();

      m_selectedColourView.Fill = new SolidColorBrush(currColour);
      m_hexValue.Text = _mColourSpace.GetHexCode(currColour);

      FireSelectedColourChangingEvent(currColour);
    }

    private void UpdateVisuals() {
      if (m_hueMonitor == null)
        return;

      Color c = SelectedColour;
      ColourSpace cs = new ColourSpace();
      HSV hsv = cs.ConvertRgbToHsv(c);

      m_huePos = (hsv.Hue/360*m_hueMonitor.Height);
      m_sampleY = -1*(hsv.Value - 1)*m_colourSample.Height;
      m_sampleX = hsv.Saturation*m_colourSample.Width;
      if (!double.IsNaN(m_huePos))
        UpdateHueSelection();
      UpdateSatValSelection();
    }

    private void DragSliders(double x, double y) {
      if (m_hueMonitorMouseCaptured) {
        if (y < 0)
          m_huePos = 0;
        else if (y > m_hueMonitor.Height)
          m_huePos = m_hueMonitor.Height;
        else
          m_huePos = y;
        UpdateHueSelection();
      } else if (m_sampleMouseCaptured) {
        if (x < 0)
          m_sampleX = 0;
        else if (x > m_colourSample.Width)
          m_sampleX = m_colourSample.Width;
        else
          m_sampleX = x;

        if (y < 0)
          m_sampleY = 0;
        else if (y > m_colourSample.Height)
          m_sampleY = m_colourSample.Height;
        else
          m_sampleY = y;

        UpdateSatValSelection();
      }
    }

    /// <summary>
    /// Called by the layout system during a layout pass.
    /// </summary>
    /// <param name="availableSize">The size available to the child elements.</param>
    /// <returns>The size set by the child elements.</returns>
    protected override Size MeasureOverride(Size availableSize) {
      Size size = new Size();
      if (m_rootElement != null) {
        m_rootElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        Size desiredSize = m_rootElement.DesiredSize;

        Size scale = ComputeScaleFactor(availableSize, desiredSize);

        size.Width = scale.Width*desiredSize.Width;
        size.Height = scale.Height*desiredSize.Height;
      }

      return size;
    }

    /// <summary>
    /// Called by the layout system during a layout pass.
    /// </summary>
    /// <param name="finalSize">The size determined to be availble to the child elements.</param>
    /// <returns>The final size used by the child elements.</returns>
    protected override Size ArrangeOverride(Size finalSize) {
      if (m_rootElement != null) {
        // Determine the scale factor given the final size
        Size desiredSize = m_rootElement.DesiredSize;
        Size scale = ComputeScaleFactor(finalSize, desiredSize);

        m_scale.ScaleX = scale.Width;
        m_scale.ScaleY = scale.Height;

        // Position the ChildElement to fill the ChildElement
        Rect originalPosition = new Rect(0, 0, desiredSize.Width, desiredSize.Height);
        m_rootElement.Arrange(originalPosition);

        // Determine the final size used by the Viewbox
        finalSize.Width = scale.Width*desiredSize.Width;
        finalSize.Height = scale.Height*desiredSize.Height;
      }
      return finalSize;
    }


    private Size ComputeScaleFactor(Size availableSize, Size contentSize) {
      double scaleX = 1.0;
      double scaleY = 1.0;

      bool isConstrainedWidth = !double.IsPositiveInfinity(availableSize.Width);
      bool isConstrainedHeight = !double.IsPositiveInfinity(availableSize.Height);

      // Don't scale if we shouldn't stretch or the scaleX and scaleY are both infinity.
      if (isConstrainedWidth || isConstrainedHeight) {
        // Compute the individual scaleX and scaleY scale factors
        scaleX = contentSize.Width == 0 ? 0.0 : (availableSize.Width/contentSize.Width);
        scaleY = contentSize.Height == 0 ? 0.0 : (availableSize.Height/contentSize.Height);

        // Make the scale factors uniform by setting them both equal to
        // the larger or smaller (depending on infinite lengths and the
        // Stretch value)
        if (!isConstrainedWidth) {
          scaleX = scaleY;
        } else if (!isConstrainedHeight) {
          scaleY = scaleX;
        }
      }

      return new Size(scaleX, scaleY);
    }

    private void FireSelectedColourChangingEvent(Color selectedColour) {
      if (SelectedColourChanging != null) {
        SelectedColourEventArgs args = new SelectedColourEventArgs(selectedColour);
        SelectedColourChanging(this, args);
      }
    }

    #region SelectedColour Dependency Property

    /// <summary>
    /// Gets or sets the currently selected colour in the Colour Picker.
    /// </summary>
    public Color SelectedColour {
      get {
        return (Color)GetValue(SelectedColourProperty);
      }
      set {
        SetValue(SelectedColourProperty, value);
        UpdateVisuals();
      }
    }

    /// <summary>
    /// SelectedColour Dependency Property.
    /// </summary>
    public static readonly DependencyProperty SelectedColourProperty = DependencyProperty.Register("SelectedColour", typeof(Color), typeof(ColourPicker), new PropertyMetadata(SelectedColourPropertyChanged));

    private static void SelectedColourPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      ColourPicker p = d as ColourPicker;
      if (p != null && p.SelectedColourChanged != null) {
        SelectedColourEventArgs args = new SelectedColourEventArgs((Color)e.NewValue);
        p.SelectedColourChanged(p, args);
      }
    }

    #endregion

    #region NewColour dep property

    public static readonly DependencyProperty NewColourProperty = DependencyProperty.Register("NewColour", typeof(string), typeof(ColourPicker), new FrameworkPropertyMetadata(SetNewColourStatic));

    private static void SetNewColourStatic(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      (d as ColourPicker).SetNewColour(e.NewValue.ToString());
    }

    private void SetNewColour(string newColour) {
      try {
        SelectedColour = (Color)(ColorConverter.ConvertFromString(newColour));
      }
      catch {
        SelectedColour = DefaultColour;
      }
    }

    public string NewColour {
      get {
        return (string)GetValue(NewColourProperty);
      }
      set {
        SetValue(NewColourProperty, value);
      }
    }

    #endregion

    #region SelectedBrush dependency property

    public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.Register("SelectedBrush", typeof(SolidColorBrush), typeof(ColourPicker), new PropertyMetadata(default(SolidColorBrush)));

    public SolidColorBrush SelectedBrush {
      get {
        return (SolidColorBrush)GetValue(SelectedBrushProperty);
      }
      set {
        SetValue(SelectedBrushProperty, value);
      }
    }

    #endregion
  }
}