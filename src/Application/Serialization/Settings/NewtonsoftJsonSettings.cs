
using DynaCore.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace DynaCore.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}