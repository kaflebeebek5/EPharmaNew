
using EPharma.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace EPharma.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}