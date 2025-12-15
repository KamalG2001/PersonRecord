using PersonRecord.Models;
using System.Text.Json;

namespace PersonRecord.JsonReader
{
    public class IJsonService
    {
        public interface IMapperService
        {
            List<User> MapFromJson(string json);
        }

        public class JsonService : IMapperService
        {
            public List<User> MapFromJson(string jsonContent)
            {
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var users = JsonSerializer.Deserialize<List<User>>(jsonContent, options);
                    return users ?? new List<User>();
                }
                catch (Exception ex)
                {
                    return new List<User>();
                }
            }
        }
    }
}
