using Microsoft.Extensions.Configuration;
using PocSecurityTwo.Security;
using System;
using System.IO;

namespace PocSecurityTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Aes aes = Aes.Create();
            // aes.GenerateIV();
            // aes.GenerateKey();
            
            var iv = Convert.FromBase64String("xRZddRsGPXbnXyafWX8UuA==");
            var key = Convert.FromBase64String("/LPUZKn89XN5kJrs7Qx1sbw0o4wBcMszfKliqjHSsCI=");

            var chave = Environment.GetEnvironmentVariable("CHAVE_S", EnvironmentVariableTarget.Machine);

            IConfigurationBuilder encryptingBuilder =
                new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddCriptoJsonFile(key, iv, "appsettings.Cripto.json");
                        //.AddUserSecrets<Program>();

            IConfiguration cfg = encryptingBuilder.Build();

            string encryptedValue7 = cfg.GetValue<string>("Cripto:ValorSecreto");
            string encryptedValue8 = cfg.GetValue<string>("Cripto:Normal");

            Console.WriteLine(encryptedValue7);
            Console.WriteLine("--");
            Console.WriteLine(encryptedValue8);
        }
    }
}
