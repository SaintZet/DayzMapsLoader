using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ZoomAndPanSample
{
    public class RectangleObserableCollectionCanvas : Canvas
    {
        static RectangleObserableCollectionCanvas()
        {
            IsHitTestVisibleProperty.OverrideMetadata(typeof(RectangleObserableCollectionCanvas), new FrameworkPropertyMetadata(false));
            BackgroundProperty.OverrideMetadata(typeof(RectangleObserableCollectionCanvas), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        }

        public static readonly DependencyProperty RectanglesProperty =
                DependencyProperty.Register("Rectangles", typeof(ObservableCollection<Tuple<Rect, Color>>),
                    typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(null, ObservableCollectionChangedCallback));

        public ObservableCollection<Tuple<Rect, Color>> Rectangles
        { get { return (ObservableCollection<Tuple<Rect, Color>>)GetValue(RectanglesProperty); } set { SetValue(RectanglesProperty, value); } }

        public static readonly DependencyProperty ShowProperty =
                DependencyProperty.Register("Show", typeof(bool), typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(true, PropertyChangedCallback));

        public bool Show
        { get { return (bool)GetValue(ShowProperty); } set { SetValue(ShowProperty, value); } }

        public static readonly DependencyProperty ScaleProperty =
                DependencyProperty.Register("Scale", typeof(double), typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(.95, PropertyChangedCallback));

        public double Scale
        { get { return (double)GetValue(ScaleProperty); } set { SetValue(ScaleProperty, value); } }

        public static readonly DependencyProperty StrokeThicknessProperty =
                DependencyProperty.Register("StrokeThickness", typeof(double), typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(1.0, PropertyChangedCallback));

        public double StrokeThickness
        { get { return (double)GetValue(StrokeThicknessProperty); } set { SetValue(StrokeThicknessProperty, value); } }

        public static readonly DependencyProperty StrokeDashStyleProperty =
                DependencyProperty.Register("StrokeDashStyle", typeof(DoubleCollection), typeof(RectangleObserableCollectionCanvas), new PropertyMetadata(new DoubleCollection { }, PropertyChangedCallback));

        public DoubleCollection StrokeDashStyle
        { get { return (DoubleCollection)GetValue(StrokeDashStyleProperty); } set { SetValue(StrokeDashStyleProperty, value); } }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Redraw();
        }

        private static void ObservableCollectionChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var rectangleObserableCollectionCanvas = (RectangleObserableCollectionCanvas)dependencyObject;
            rectangleObserableCollectionCanvas.Rectangles.CollectionChanged += (s, e) =>
                rectangleObserableCollectionCanvas?.Redraw();
            rectangleObserableCollectionCanvas?.Redraw();
        }

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            (dependencyObject as RectangleObserableCollectionCanvas)?.Redraw();
        }

        private void Redraw()
        {
            Children.Clear();
            if (!Show) return;
            try
            {
                if (Math.Abs(ActualHeight) < 1 || Math.Abs(ActualWidth) < 1) return;

                if (Rectangles == null)
                {
                    return;
                }

                foreach (var rectangleProperties in Rectangles)
                {
                    var rectangle = new System.Windows.Shapes.Rectangle
                    {
                        Stroke = new SolidColorBrush(rectangleProperties.Item2),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        StrokeDashArray = StrokeDashStyle,
                        Width = rectangleProperties.Item1.Width,
                        Height = rectangleProperties.Item1.Height,
                        StrokeThickness = StrokeThickness / Scale,
                    };
                    Canvas.SetLeft(rectangle, rectangleProperties.Item1.Left);
                    Canvas.SetTop(rectangle, rectangleProperties.Item1.Top);
                    Children.Add(rectangle);
                }
            }
            catch { }
        }
    }
}