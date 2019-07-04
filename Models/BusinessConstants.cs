using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Priority
    {
        High= 1,
        Medium ,
        Low 
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        Off = 1,
        Done,
        Ongoing,
        Hold,
        Inprogress
    }
}
