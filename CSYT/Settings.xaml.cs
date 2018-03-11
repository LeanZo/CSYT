#region License Information (GPL v3)

/*
CSYT is a free and open source program that allow you to watch Youtube videos while doing other stuff.
Copyright(C) 2018  Lucas Lean

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see<https://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

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

            foreach (string file in Languages.LanguageFilesList)
            {
                CBoxLanguage.Items.Add(file);
            }

            CBoxLanguage.SelectedItem = Properties.Settings.Default.Language;

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

            // Translation.
            Title = Languages.Get("Settings_Settings");
            GBoxSettings.Header = Languages.Get("Settings_Settings");
            LblLanguage.Content = Languages.Get("Settings_Language");
            ChkAutoplay.Content = Languages.Get("Settings_EnableAutoplay");
            ChkVideoInfo.Content = Languages.Get("Settings_ShowVideoInfo");
            ChkVideoControls.Content = Languages.Get("Settings_EnableVideoControls");
            LblOpacity.Content = Languages.Get("Settings_Opacity");
            BtnOk.Content = Languages.Get("Settings_OK");
            BtnCancel.Content = Languages.Get("Settings_Cancel");
        }

        // Saves settings
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Language != CBoxLanguage.SelectedItem.ToString())
            {
                Properties.Settings.Default.Language = CBoxLanguage.SelectedItem.ToString();
                MessageBox.Show(this, Languages.Get("Settings_YouMustRestart"), VersionInfo.AppNameAndVersion);
            }

            Properties.Settings.Default.Autoplay = ChkAutoplay.IsChecked.Value ? 1 : 0;

            Properties.Settings.Default.VideoControls = ChkVideoControls.IsChecked.Value ? 1 : 0;

            Properties.Settings.Default.VideoInfo = ChkVideoInfo.IsChecked.Value ? 1 : 0;

            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}
