using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PocSecurityTwo.Security
{
    public class JsonCriptoConfigurationSource : JsonConfigurationSource
    {
        private byte[] _key;
        private byte[] _iv;
        private string _path;
        private bool _optional;

        public JsonCriptoConfigurationSource(byte[] key, byte[] iv, string path, bool optional)
        {
            _key = key;
            _iv = iv;
            _path = path;
            _optional = optional;
        }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var provider = builder.GetFileProvider();

            base.FileProvider = builder.GetFileProvider();
            base.ResolveFileProvider();

            base.Optional = _optional;
            base.Path = _path;
            base.ReloadOnChange = false;

            return new JsonCriptoConfigurationProvider(this, _key, _iv);
        }
    }
}