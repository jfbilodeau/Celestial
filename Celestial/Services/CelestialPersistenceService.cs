using System.Text.Json;
using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using Celestial.Models;

namespace Celestial.Services;

public class CelestialPersistenceService(ILocalStorageService localStorage)
{
    private const string SettingsKey = "settings";
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true,
    };

    public async ValueTask SaveSettings(Settings settings)
    {
        var json = JsonSerializer.Serialize(settings, _options);
        
        await localStorage.SetItemAsync(SettingsKey, json);
        // await localStorage.SetItemAsync(SettingsKey, settings);
    }

    public async Task<Settings?> LoadSettings()
    {
        Settings? settings = null;
        
        var text = await localStorage.GetItemAsStringAsync(SettingsKey);
        
        if (string.IsNullOrWhiteSpace(text) == false)
        {
            text = text.Trim('"');
            text = text.Replace("\\u0022", "\"");
            
            settings = JsonSerializer.Deserialize<Settings>(text, _options);
        }

        // var settings = await localStorage.GetItemAsync<Settings>(SettingsKey);
        
        Console.WriteLine(settings);
        
        return settings;
    }
}