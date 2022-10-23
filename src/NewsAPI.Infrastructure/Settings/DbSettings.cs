using NewsAPI.Core.Interfaces.Settings;

namespace NewsAPI.Infrastructure.Settings;

public class DbSettings : IDbSettings
{
    public string DefaultConnection { get; set; } = String.Empty;
}
