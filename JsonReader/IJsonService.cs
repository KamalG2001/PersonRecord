using PersonRecord.Models;

namespace PersonRecord.JsonReader
{
    public class IJsonService
    {
        public interface IMapperService
        {
            List<User> MapFromJson(string json);
        }
    }
}
