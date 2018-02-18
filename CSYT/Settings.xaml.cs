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
using System.Configuration;

namespace CSYT
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        Window window;

        public Settings(Window window)
        {
            InitializeComponent();

            Owner = window;

            this.window = window;

            Chk_Autoplay.IsChecked = Properties.Settings.Default.C_Autoplay == 1 ? true : false;

            Chk_VideoControls.IsChecked = Properties.Settings.Default.C_VideoControls == 1 ? true : false;

            Chk_VideoInfo.IsChecked = Properties.Settings.Default.C_VideoInfo == 1 ? true : false;
        }

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
