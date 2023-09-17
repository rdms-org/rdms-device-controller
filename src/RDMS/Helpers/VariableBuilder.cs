using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RDMS.Helpers
{
    internal class VariableBuilder
    {
        internal static string GetProductVersion()
        {
            var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            return attribute != null ? attribute!.InformationalVersion : "dev";
        }

        internal static string GetFileVersion()
        {
            var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyVersionAttribute>();
            return attribute != null ? attribute!.Version : "dev";
        }

        internal static string GetApplicationLocation()
        {
            return Path.Combine(GetBaseDirectory(), AppDomain.CurrentDomain.FriendlyName + ".exe");
        }

        internal static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
