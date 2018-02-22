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

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (window.IMG_BG.IsVisible) window.IMG_BG.Visibility = Visibility.Hidden;

            UrlLoad(TextBox_Url.Text, window);   

            this.Close();
        }

        // Extracts video's and playlist's IDs, creates a new url using user settings and Load it.
        public static void UrlLoad(string url, MainWindow window)
        {
            string videoId = Regex.Match(url, @"watch\?v=([^\/&]+)").Groups[1].Value;
            string playListId = Regex.Match(url, @"(&list=[^\/&]+)").Groups[1].Value;

            string userParams = String.Format("autoplay={0}&showinfo={1}&controls={2}", Properties.Settings.Default.C_Autoplay, Properties.Settings.Default.C_VideoInfo, Properties.Settings.Default.C_VideoControls);

            if (videoId != string.Empty)
                window.WebBrowser.Load(String.Format(@"https://www.youtube.com/embed/{0}?iv_load_policy=3&fs=0&rel=1&{1}&{2}", videoId, userParams, playListId));
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