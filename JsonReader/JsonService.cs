using PersonRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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
