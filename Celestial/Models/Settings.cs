using Celestial.Services;

namespace Celestial.Models;

public class Settings
{
    public OpenAiSettings OpenAi { get; set;  } = new OpenAiSettings();
}