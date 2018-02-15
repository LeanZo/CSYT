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

namespace CSYT_CefSharp
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
            string VideoID = Regex.Match(TextBox_Url.Text, @"watch\?v=([^\/&]+)").Groups[1].Value;
            string PlayListID = Regex.Match(TextBox_Url.Text, @"(&list=[^\/&]+)").Groups[1].Value;

            window.WebBrowser.Load(String.Format(@"https://www.youtube.com/embed/{0}?{1}{2}", VideoID, @"autoplay=1&modestbranding=0&showinfo=0&controls=0&hl=pt", PlayListID));
            this.Close();
        }
    }
}
