using Newtonsoft.Json;

namespace DAL.helper
{
    internal class JsonSerializersettings
    {
        public static implicit operator JsonSerializersettings(JsonSerializerSettings v)
        {
            throw new NotImplementedException();
        }
    }
}