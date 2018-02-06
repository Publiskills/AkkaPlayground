using System.IO;
using Akka.Configuration;

namespace AkkaPlayground
{
    internal partial class Program
    {
        private static Config GetAkkaConfigurationFromHoconFile(string configFileName = "akka.hocon")
        {
            return ConfigurationFactory.ParseString(File.ReadAllText(configFileName));
        }
    }
}
