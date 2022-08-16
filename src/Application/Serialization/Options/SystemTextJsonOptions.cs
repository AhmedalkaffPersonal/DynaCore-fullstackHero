using System.Text.Json;
using DynaCore.Application.Interfaces.Serialization.Options;

namespace DynaCore.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}