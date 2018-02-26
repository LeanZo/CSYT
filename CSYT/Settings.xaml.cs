using System.Windows;

namespace CSYT
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        MainWindow window;

        public Settings(MainWindow window)
        {
            InitializeComponent();

            Owner = window;

            this.window = window;

            Chk_Autoplay.IsChecked = Properties.Settings.Default.C_Autoplay == 1 ? true : false;

            Chk_VideoControls.IsChecked = Properties.Settings.Default.C_VideoControls == 1 ? true : false;

            Chk_VideoInfo.IsChecked = Properties.Settings.Default.C_VideoInfo == 1 ? true : false;

            SliderOpacity.Value = Properties.Settings.Default.C_Opacity;

            SliderOpacity.ValueChanged += (sender, e) =>
            {
                window.WebBrowser.Opacity = e.NewValue;
                window.IMG_BG.Opacity = e.NewValue;
                Properties.Settings.Default.C_Opacity = SliderOpacity.Value;
            };
        }

        // Saves settings
        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.C_Autoplay = Chk_Autoplay.IsChecked.Value ? 1 : 0;

            Properties.Settings.Default.C_VideoControls = Chk_VideoControls.IsChecked.Value ? 1 : 0;

            Properties.Settings.Default.C_VideoInfo = Chk_VideoInfo.IsChecked.Value ? 1 : 0;

            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}
