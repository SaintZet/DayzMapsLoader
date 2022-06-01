using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace ZoomAndPanSample
{
    /// <summary>
    ///     This is a Window that uses ZoomAndPanControl to zoom and pan around some content.
    ///     This demonstrates how to use application specific mouse handling logic with ZoomAndPanControl.
    /// </summary>
    public partial class MainWindow : Window
    {
 
        public MainWindow()
        {
            InitializeComponent();
            Rectangles = new ObservableCollection<Tuple<Rect, Color>>
            {
                new Tuple<Rect, Color>(new Rect(50,25,50,25), Colors.Blue ),
                new Tuple<Rect, Color>(new Rect(150,100,100,50), Colors.Aqua ),
            };
            DataContext = this;
        }

        /// <summary>
        ///     Event raised when the Window has loaded.
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var helpTextWindow = new HelpTextWindow
            {
                Left = Left + Width + 5,
                Top = Top,
                Owner = this
            };
            helpTextWindow.Show();
        }


        public ObservableCollection<Tuple<Rect, Color>> Rectangles { get;
            private set; }
    }
}