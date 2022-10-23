namespace NewsAPI.Core.Interfaces.Settings;

public interface IAppSettings
{
    public string Domain { get; set; }
    public string SecurityKey { get; set; }
}
