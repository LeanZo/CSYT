using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace CSYT
{
    /// <summary>
    /// Interaction logic for ChangeUrl.xaml
    /// </summary>
    public partial class ChangeUrl : Window
    {
        MainWindow window;

        public ChangeUrl(MainWindow window)
        {
            InitializeComponent();

            this.window = window;

            Owner = window;
        }

        // Extracts video's and playlist's IDs, creates a new url using user settings and Load it.
        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (window.IMG_BG.IsVisible) window.IMG_BG.Visibility = Visibility.Hidden;

            string videoId = Regex.Match(TextBox_Url.Text, @"watch\?v=([^\/&]+)").Groups[1].Value;
            string playListId = Regex.Match(TextBox_Url.Text, @"(&list=[^\/&]+)").Groups[1].Value;

            string userParams = String.Format("autoplay={0}&showinfo={1}&controls={2}", Properties.Settings.Default.C_Autoplay, Properties.Settings.Default.C_VideoInfo, Properties.Settings.Default.C_VideoControls);

            window.WebBrowser.Load(String.Format(@"https://www.youtube.com/embed/{0}?iv_load_policy=3&rel=0&{1}&{2}", videoId, userParams, playListId));
            this.Close();
        }

        // Checks if TextBlock_Url.Text is a valid Youtube Url
        private void TextBox_Url_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlock_Url.Text = TextBox_Url.Text != string.Empty ? string.Empty : "Insert Video Url...";

            if (Regex.IsMatch(TextBox_Url.Text, @"youtube.com/.*?watch\?v=([^\/&]+)"))
                Btn_OK.IsEnabled = true;
            else
                Btn_OK.IsEnabled = false;
        }
    }
}