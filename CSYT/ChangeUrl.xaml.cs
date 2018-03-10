using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Navigation;

namespace CSYT
{
    /// <summary>
    /// Interaction logic for ChangeUrl.xaml
    /// </summary>
    public partial class ChangeUrl : Window
    {
        readonly MainWindow window;

        public ChangeUrl(MainWindow window)
        {
            InitializeComponent();

            this.window = window;

            Owner = window;

            // Translation.
            Title = Languages.Get("ChangeVideo_ChangeVideo");
            TextBlockUrl.Text = Languages.Get("ChangeVideo_InsertVideoUrl");
            ChkLoop.Content = Languages.Get("ChangeVideo_Loop");
            BtnOk.Content = Languages.Get("ChangeVideo_OK");
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (window.ImgBg.IsVisible) window.ImgBg.Visibility = Visibility.Hidden;

            UrlLoad(TextBoxUrl.Text);   

            this.Close();
        }

        // Extracts video's and playlist's IDs, creates a new url using user settings and Load it.
        private void UrlLoad(string url)
        {
            string videoId = Regex.Match(url, @"watch\?v=([^\/&]+)").Groups[1].Value;
            string playListId = Regex.Match(url, @"(list=[^\/&]+)").Groups[1].Value;

            string userParams = String.Format("autoplay={0}&showinfo={1}&controls={2}",
                Properties.Settings.Default.Autoplay,
                Properties.Settings.Default.VideoInfo,
                Properties.Settings.Default.VideoControls);

            if ((bool)ChkLoop.IsChecked)
            {
                if (playListId == string.Empty) playListId = $"playlist={videoId}";

                userParams += "&loop=1";
            }

            if (videoId != string.Empty)
                window.WebBrowser.Load(String.Format(@"https://www.youtube.com/embed/{0}?iv_load_policy=3&fs=0&rel=1&{1}&{2}", videoId, userParams, playListId));
        }

        // This overload is to deal with related videos. TODO: Refactoring
        public static void UrlLoad(string url, MainWindow window)
        {
            string videoId = Regex.Match(url, @"watch\?v=([^\/&]+)").Groups[1].Value;
            string playListId = Regex.Match(url, @"(list=[^\/&]+)").Groups[1].Value;

            string userParams = String.Format("autoplay={0}&showinfo={1}&controls={2}",
                Properties.Settings.Default.Autoplay,
                Properties.Settings.Default.VideoInfo,
                Properties.Settings.Default.VideoControls);

            if (videoId != string.Empty)
                window.WebBrowser.Load(String.Format(@"https://www.youtube.com/embed/{0}?iv_load_policy=3&fs=0&rel=1&{1}&{2}", videoId, userParams, playListId));
        }

        // Checks if TextBlock_Url.Text is a valid Youtube Url
        private void TextBoxUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlockUrl.Text = TextBoxUrl.Text != string.Empty ? string.Empty : "Insert Video Url...";

            BtnOk.IsEnabled = Regex.IsMatch(TextBoxUrl.Text, @"youtube.com/.*?watch\?v=([^\/&]+)");
        }
    }
}