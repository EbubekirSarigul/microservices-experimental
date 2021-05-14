using Newtonsoft.Json;

namespace MicroserviceTraining.Framework.Extensions
{
    public static class StringExtensions
    {
        public static T Deserialize<T>(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
