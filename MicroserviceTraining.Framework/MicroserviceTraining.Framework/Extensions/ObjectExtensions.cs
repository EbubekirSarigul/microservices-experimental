using Newtonsoft.Json;

namespace MicroserviceTraining.Framework.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object @object)
        {
            if (@object == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(@object);
        }
    }
}
