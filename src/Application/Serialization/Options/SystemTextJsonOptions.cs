using System.Text.Json;
using EPharma.Application.Interfaces.Serialization.Options;

namespace EPharma.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}