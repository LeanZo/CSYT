using System;
using System.Linq;
using System.Reflection;

namespace CSYT
{
    static class VersionInfo
    {
        public static bool IsBeta { get; } = false;


        public static string AppName { get; } = Assembly.GetEntryAssembly()
                .GetCustomAttributes(typeof(AssemblyProductAttribute))
                .OfType<AssemblyProductAttribute>()
                .FirstOrDefault().Product;

        public static string AppVersion
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

        public static string AppNameAndVersion { get; } = AppName + " " + AppVersion;
    }
}
