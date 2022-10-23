using NewsAPI.Core.Interfaces.Settings;

namespace NewsAPI.Infrastructure.Settings;

public class AppSettings : IAppSettings
{
    public string Domain { get; set; } = String.Empty;
    public string SecurityKey { get; set; } = String.Empty;
}
