using Newtonsoft.Json;

namespace Effectory.Shared.JsonConfiguration
{
    public static class JsonConfigurationExtensions
    {
        public static JsonSerializerSettings WithPrivateSetterContractResolver(
            this JsonSerializerSettings settings,
            ConstructorHandling constructorHandling = ConstructorHandling.Default)
        {
            settings.ConstructorHandling = constructorHandling;

            settings.ContractResolver = new PrivateSetterResolver();
            return settings;
        }
    }
}
