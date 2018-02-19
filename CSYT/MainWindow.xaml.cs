using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Media.Imaging;

namespace CSYT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Title = VersionInfo.AppNameVersion;
            
            WebBrowser.RequestHandler = new RequestHandler();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CefSharp.Cef.Shutdown();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

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

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var window = sender as Window;

            int Width = 10;

            double Ratio = 0.5777126099706745;

            if (Keyboard.IsKeyDown(Key.LeftShift))
                Width = 20;

            if (e.Delta > 0)
            {
                window.Width += Width;
                window.Height = Ratio * window.Width;
            }
            else
            {
                if (window.Width > 20)
                {
                    window.Width -= Width;
                    window.Height = Ratio * window.Width;
                }
            }
        }
    }
}
