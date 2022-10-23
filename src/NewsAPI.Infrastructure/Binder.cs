using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using NewsAPI.Core.Extensions;

namespace NewsAPI.Infrastructure;

public static class Binder
{
    public static T BindSettings<T>(this IConfiguration configuration, SettingsInfo settingsInfo)
              where T : class, new()
    {
        T settings = new T();
        configuration.GetSection(settingsInfo.SectionName).Bind(settings);

        if (!string.IsNullOrEmpty(settingsInfo.EnvironmentPrefix))
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string? envVariable = Environment.GetEnvironmentVariable($"{settingsInfo.EnvironmentPrefix}_{property.Name.ToUpperInvariant()}");

                if (!string.IsNullOrEmpty(envVariable))
                {
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(property.PropertyType);
                    property.SetValue(settings, typeConverter.ConvertFromString(envVariable));
                }

            }
        }
        return settings;
    }
}
