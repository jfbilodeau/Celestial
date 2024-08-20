using System.Text.Json.Serialization;

namespace Celestial.Models;

public class OpenAiSettings
{
    public string Url { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Deployment { get; set; } = string.Empty;
    
    [JsonIgnore]
    public bool IsValid => !string.IsNullOrWhiteSpace(Url) && !string.IsNullOrWhiteSpace(Key) && !string.IsNullOrWhiteSpace(Deployment);
}