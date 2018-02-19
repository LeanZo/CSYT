using System;
using System.Linq;
using System.Reflection;

namespace CSYT
{
    static class VersionInfo
    {
        private static bool Beta { get; } = false;


        public static string ProductName { get; } = Assembly.GetEntryAssembly()
                .GetCustomAttributes(typeof(AssemblyProductAttribute))
                .OfType<AssemblyProductAttribute>()
                .FirstOrDefault().Product;

        public static string Versao
        {
            get
            {
                var _versao = Version.Parse(Assembly.GetEntryAssembly()
                .GetCustomAttributes(typeof(AssemblyFileVersionAttribute))
                .OfType<AssemblyFileVersionAttribute>()
                .FirstOrDefault().Version);

                string versao = String.Format("{0}.{1}", _versao.Major, _versao.Minor);

                if (_versao.Build > 0) versao += "." + _versao.Build;
                if (_versao.Revision > 0) versao += "." + _versao.Revision;
                if (Beta) versao += " (Beta)";

                return versao;
            }
        }

        public static string Copyright { get; } = Assembly.GetEntryAssembly()
                .GetCustomAttributes(typeof(AssemblyCopyrightAttribute))
                .OfType<AssemblyCopyrightAttribute>()
                .FirstOrDefault().Copyright;

        public static string AppNameVersion { get; } = ProductName + " " + Versao;

    }
}
