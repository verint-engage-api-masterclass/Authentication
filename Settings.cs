using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Need to add the following NuGet Packages:
//Microsoft.Extensions.Configuration
//Microsoft.Extensions.Configuration.Binder
//Microsoft.Extensions.Configuration.EnvironmentVariables
//Microsoft.Extensions.Configuration.Json
//Microsoft.Extensions.ConfigurationExtensions


namespace Authentication1;

internal class Settings : ISettings
{
    private string? _apiKeyId;
    private string? _apiKey;
    private string? _hostname;
    private string? _protocol;

    public Settings()
    {
        var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appSettings.json")
                            .AddEnvironmentVariables()
                            .Build();

        _apiKeyId ??= config.GetValue<string>("VerintAPIKeyID");

        _apiKey ??= config.GetValue<string>("VerintAPIKey");

        _hostname ??= config.GetValue<string>("VerintAPIHostname");

        _protocol ??= config.GetValue<string>("Protocol");
    }

    public string APIKeyID { get => _apiKeyId; }

    public string APIKey { get => _apiKey; }

    public string Protocol { get => _protocol; }

    public string Hostname { get => _hostname; }


}
