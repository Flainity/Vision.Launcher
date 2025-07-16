using Microsoft.Extensions.Configuration;

namespace Library.Configuration.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddIniFile("settings.ini", optional: false, reloadOnChange: true);

        _configuration = builder.Build();
    }
    
    public string GetSetting(string section, string key)
    {
        var configurationSection = _configuration.GetSection(section);
        return configurationSection[key] ?? "";
    }
    
    public int GetSettingInt(string section, string key)
    {
        var configurationSection = _configuration.GetSection(section);
        return int.Parse(configurationSection[key] ?? "0");
    }
}