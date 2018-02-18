using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

            string VideoID = Regex.Match(TextBox_Url.Text, @"watch\?v=([^\/&]+)").Groups[1].Value;
            string PlayListID = Regex.Match(TextBox_Url.Text, @"(&list=[^\/&]+)").Groups[1].Value;

            string UserParams = String.Format("autoplay={0}&showinfo={1}&controls={2}", Properties.Settings.Default.C_Autoplay, Properties.Settings.Default.C_VideoInfo, Properties.Settings.Default.C_VideoControls);

            window.WebBrowser.Load(String.Format(@"https://www.youtube.com/embed/{0}?iv_load_policy=3&rel=0&{1}&{2}", VideoID, UserParams, PlayListID));
            this.Close();
        }

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