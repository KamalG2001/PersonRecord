using PersonRecord.Models;
using System.Text.Json;
using static PersonRecord.JsonReader.IJsonService;

namespace PersonRecord.JsonReader
{
    public class JsonService : IMapperService
    {
        public List<User> MapFromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new List<User>();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<User>>(json, options)
                   ?? new List<User>();
        }
    }
}
