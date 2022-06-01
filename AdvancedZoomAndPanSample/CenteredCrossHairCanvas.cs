using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ZoomAndPanSample
{
    public class CenteredCrossHairCanvas : Canvas
    {
        static CenteredCrossHairCanvas()
        {
            IsHitTestVisibleProperty.OverrideMetadata(typeof (CenteredCrossHairCanvas), new FrameworkPropertyMetadata(false));
            BackgroundProperty.OverrideMetadata(typeof (CenteredCrossHairCanvas), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        }
 
        public static readonly DependencyProperty ShowProperty =
                DependencyProperty.Register("Show", typeof(bool), typeof(CenteredCrossHairCanvas), new PropertyMetadata(true, PropertyChangedCallback));
        public bool Show { get { return (bool)GetValue(ShowProperty); } set { SetValue(ShowProperty, value); } }

        public static readonly DependencyProperty ScaleProperty =
                DependencyProperty.Register("Scale", typeof(double), typeof(CenteredCrossHairCanvas), new PropertyMetadata(.95, PropertyChangedCallback));
        public double Scale { get { return (double)GetValue(ScaleProperty); } set { SetValue(ScaleProperty, value); } }

        public static readonly DependencyProperty HorizontalLinesProperty =
                DependencyProperty.Register("HorizontalLines", typeof(int), typeof(CenteredCrossHairCanvas), new PropertyMetadata(1, PropertyChangedCallback));
        public int HorizontalLines { get { return (int)GetValue(HorizontalLinesProperty); } set { SetValue(HorizontalLinesProperty, value); } }

        public static readonly DependencyProperty VerticalLinesProperty =
                DependencyProperty.Register("VerticalLines", typeof(int), typeof(CenteredCrossHairCanvas), new PropertyMetadata(3, PropertyChangedCallback));
        public int VerticalLines { get { return (int)GetValue(VerticalLinesProperty); } set { SetValue(VerticalLinesProperty, value); } }

        public static readonly DependencyProperty StrokeBrushProperty =
            DependencyProperty.Register("StrokeBrush", typeof(Brush), typeof(CenteredCrossHairCanvas), new PropertyMetadata(new SolidColorBrush(Colors.Black), PropertyChangedCallback));
        public Brush StrokeBrush { get { return (Brush)GetValue(StrokeBrushProperty); } set { SetValue(StrokeBrushProperty, value); } }

        public static readonly DependencyProperty StrokeThicknessProperty =
                DependencyProperty.Register("StrokeThickness", typeof(double), typeof(CenteredCrossHairCanvas), new PropertyMetadata(1.0, PropertyChangedCallback));
        public double StrokeThickness { get { return (double)GetValue(StrokeThicknessProperty); } set { SetValue(StrokeThicknessProperty, value); } }

        public static readonly DependencyProperty StrokeDashStyleProperty =
                DependencyProperty.Register("StrokeDashStyle", typeof(DoubleCollection), typeof(CenteredCrossHairCanvas), new PropertyMetadata(new DoubleCollection { }, PropertyChangedCallback));
        public DoubleCollection StrokeDashStyle { get { return (DoubleCollection)GetValue(StrokeDashStyleProperty); } set { SetValue(StrokeDashStyleProperty, value); } }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Redraw();
        }

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            (dependencyObject as CenteredCrossHairCanvas)?.Redraw();
        }

        private void Redraw()
        {
            Children.Clear();
            if (!Show) return;
            try
            {
                if (Math.Abs(ActualHeight) < 1 || Math.Abs(ActualWidth) < 1) return;

                for (var i = 1; i <= HorizontalLines; i++)
                {
                    var horizontalLine = new Line
                    {
                        Stroke = StrokeBrush,
                        StrokeDashArray = StrokeDashStyle,
                        X1 = 0,
                        X2 = ActualWidth,
                        Y1 = (ActualHeight * i) / (HorizontalLines + 1),
                        Y2 = (ActualHeight * i) / (HorizontalLines + 1),
                        StrokeThickness = StrokeThickness / Scale,
                    };
                    Children.Add(horizontalLine);
                }
                for (var i = 1; i <= VerticalLines; i++)
                {
                    var verticalLine = new Line
                    {
                        Stroke = StrokeBrush,
                        StrokeDashArray = StrokeDashStyle,
                        Y1 = 0,
                        Y2 = ActualHeight,
                        X1 = (ActualWidth * i) / (VerticalLines + 1),
                        X2 = (ActualWidth * i) / (VerticalLines + 1),
                        StrokeThickness = StrokeThickness / Scale,
                    };
                    Children.Add(verticalLine);
                }
            }
            catch { }
        }
    }
}
