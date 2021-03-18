using Microsoft.Extensions.Configuration;

namespace PocSecurityTwo.Security
{
    public static class JsonCriptoConfigurationExtensions
    {
        public static IConfigurationBuilder AddCriptoJsonFile(this IConfigurationBuilder builder, byte[] key, byte[] iv, string path)
        {
            return builder.Add(new JsonCriptoConfigurationSource(key, iv, path, false));
        }

        public static IConfigurationBuilder AddCriptoJsonFile(this IConfigurationBuilder builder, byte[] key, byte[] iv, string path, bool optional)
        {
            return builder.Add(new JsonCriptoConfigurationSource(key, iv, path, optional));
        }
    }
}