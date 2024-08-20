using Blazored.LocalStorage;
using Celestial.Kernel;
using Celestial.Models;

namespace Celestial.Services;

public class CelestialService
{
    private readonly CelestialPersistenceService _persistence;
    
    public Settings Settings { get; } = new Settings();
    public Semmy Semmy { get; private set; }
    
    public CelestialService(CelestialPersistenceService persistence)
    {
        _persistence = persistence;
    }

    public async ValueTask SaveSettings()
    {
        await _persistence.SaveSettings(Settings);
    }

    public async Task LoadSettings()
    {
        var settings = await _persistence.LoadSettings() ?? new Settings();

        Settings.OpenAi.Url = settings.OpenAi.Url;
        Settings.OpenAi.Key = settings.OpenAi.Key;
        Settings.OpenAi.Deployment = settings.OpenAi.Deployment;
    }

    public bool Initialize()
    {
        try
        {
            Semmy = new Semmy(this);
        }
        catch
        {
            return false;
        }

        return true;
    }
}