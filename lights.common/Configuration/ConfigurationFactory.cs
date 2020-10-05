using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace lights.common.Configuration
{
    public static class ConfigurationFactory
    {
        public static ApplicationConfig Load()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new HyphenatedNamingConvention())
                .Build();

            var fileContent = ReadFileContent("application-config.yaml");
            return deserializer.Deserialize<ApplicationConfig>(fileContent);
        }

        private static string ReadFileContent(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
