using System.Windows;

namespace CSYT
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings(MainWindow window)
        {
            InitializeComponent();

            Owner = window;

            ChkAutoplay.IsChecked = Properties.Settings.Default.Autoplay == 1;

            ChkVideoControls.IsChecked = Properties.Settings.Default.VideoControls == 1;

            ChkVideoInfo.IsChecked = Properties.Settings.Default.VideoInfo == 1;

            SliderOpacity.Value = Properties.Settings.Default.Opacity;

            SliderOpacity.ValueChanged += (sender, e) =>
            {
                window.WebBrowser.Opacity = e.NewValue;
                window.ImgBg.Opacity = e.NewValue;
                Properties.Settings.Default.Opacity = SliderOpacity.Value;
            };
        }

        // Saves settings
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Autoplay = ChkAutoplay.IsChecked.Value ? 1 : 0;

            Properties.Settings.Default.VideoControls = ChkVideoControls.IsChecked.Value ? 1 : 0;

            Properties.Settings.Default.VideoInfo = ChkVideoInfo.IsChecked.Value ? 1 : 0;

            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}
