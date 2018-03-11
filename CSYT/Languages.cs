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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace CSYT
{
    internal static class Languages
    {
        internal static List<string> LanguageFilesList { get; } = new List<string>();

        private static readonly Dictionary<string, string> LanguageFile =
            JsonConvert.DeserializeObject<Dictionary<string, string>>(
                File.ReadAllText($@"Languages\{Properties.Settings.Default.Language}.json", Encoding.UTF8));

        internal static readonly BitmapImage ShortcutsImg = new BitmapImage(new Uri($@"Languages\{Properties.Settings.Default.Language}_shortcuts.png", UriKind.Relative));

        static Languages()
        {
            // If a shortcuts.png does not exist for the selected language, sets the default image in english.
            string[] resourceNames = GetResourceNames();

            if (!resourceNames.Contains($@"languages/{Properties.Settings.Default.Language.ToLower()}_shortcuts.png"))
                ShortcutsImg = new BitmapImage(new Uri($@"Languages\English_shortcuts.png", UriKind.Relative));

            // Gather all files ending in '.json'
            List<string> allFiles = Directory
                .GetFiles(Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString() + "\\Languages")
                .ToList();

            foreach (string file in allFiles)
            {
                if(file.EndsWith(".json"))
                    LanguageFilesList.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private static string[] GetResourceNames()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resName = assembly.GetName().Name + ".g.resources";
            using (var stream = assembly.GetManifestResourceStream(resName))
            {
                using (var reader = new System.Resources.ResourceReader(stream))
                {
                    return reader.Cast<System.Collections.DictionaryEntry>().Select(entry =>
                        (string)entry.Key).ToArray();
                }
            }
        }

        // Returns the translated string.
        internal static string Get(string Key)
        {
            return LanguageFile[Key];
        }
    }
}
