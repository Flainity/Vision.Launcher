namespace Veritas.Library.Configuration.Services;

public interface IConfigurationService
{
    public string GetSetting(string section, string key);
    public int GetSettingInt(string section, string key);
}