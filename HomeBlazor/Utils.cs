using Newtonsoft.Json;

namespace HomeBlazor
{
    public static class Utils
    {
        public static string Serialize(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
