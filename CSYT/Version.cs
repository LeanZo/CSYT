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
using System.Linq;
using System.Reflection;

namespace CSYT
{
    internal static class VersionInfo
    {
        internal static bool IsBeta { get; } = false;


        internal static string AppName { get; } = Assembly.GetEntryAssembly()
                .GetCustomAttributes(typeof(AssemblyProductAttribute))
                .OfType<AssemblyProductAttribute>()
                .FirstOrDefault().Product;

        internal static string AppVersion
        {
            get
            {
                var fullVersion = Version.Parse(Assembly.GetEntryAssembly()
                .GetCustomAttributes(typeof(AssemblyFileVersionAttribute))
                .OfType<AssemblyFileVersionAttribute>()
                .FirstOrDefault().Version);

                string version = String.Format("{0}.{1}", fullVersion.Major, fullVersion.Minor);

                version += fullVersion.Build > 0 ? "." + fullVersion.Build : string.Empty;
                version += fullVersion.Revision > 0 ? "." + fullVersion.Revision : string.Empty;
                if (IsBeta) version += " (Beta)";

                return version;
            }
        }

        internal static string AppNameAndVersion { get; } = AppName + " " + AppVersion;
    }
}
