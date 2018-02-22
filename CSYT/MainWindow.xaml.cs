using System;
using System.Windows;
using System.Windows.Input;
using CefSharp;

namespace CSYT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point startPosition = new Point();
        public double InitialWidth { get; } = 683.0;
        public double InitialHeight { get; } = 384.0;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Title = VersionInfo.AppNameAndVersion;

            WebBrowser.RequestHandler = new RequestHandler();

            WebBrowser.LifeSpanHandler = new LifeSpanHandler(this);
        }

        // Prevents browser's right-click.
        private void WebBrowser_RightClickOff(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPosition = e.GetPosition(this);
        }

        private void Window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Point endPosition = e.GetPosition(this);
                Vector vector = endPosition - startPosition;
                this.Left += vector.X;
                this.Top += vector.Y;
            }
        }

        // Shotcuts.
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var window = sender as Window;

            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.N))
                new ChangeUrl(this).ShowDialog();

            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.Z) && window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Normal;

            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.S))
                new Settings(this).ShowDialog();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            var window = sender as Window;

            if (window.WindowState == WindowState.Minimized) window.WindowState = WindowState.Normal;
        }

        // Resize maintaining aspect ratio.
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var window = sender as Window;

            double relativeWidth = 10.0;

            double ratio = InitialHeight / InitialWidth;

            if (Keyboard.IsKeyDown(Key.LeftShift)) relativeWidth = 20.0;

            if (e.Delta > 0)
            {
                window.Width += relativeWidth;
                window.Height = ratio * window.Width;

                Grid.Width += relativeWidth;
                Grid.Height = ratio * window.Width;

            }
            else if (window.Width > 200)
            {
                window.Width -= relativeWidth;
                window.Height = ratio * window.Width;

                Grid.Width -= relativeWidth;
                Grid.Height = ratio * window.Width;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CefSharp.Cef.Shutdown();
        }
    }
}
