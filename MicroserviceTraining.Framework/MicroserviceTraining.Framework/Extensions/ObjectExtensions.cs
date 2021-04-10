using Newtonsoft.Json;

namespace MicroserviceTraining.Framework.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object @object)
        {
            return JsonConvert.SerializeObject(@object);
        }
    }
}
