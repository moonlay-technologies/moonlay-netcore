using Newtonsoft.Json;

namespace System
{
    public static class JsonExtension
    {
        public static string Serialize<T>(this T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T Deserialize<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
