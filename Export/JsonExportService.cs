using PersonRecord.Models;
using System.IO;
using System.Text.Json;

namespace PersonRecord.Export
{
    public class JsonExportService : IExport
    {
        public void Export(List<User> people, string filePath)
        {
            var options = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize(people, options);
            File.WriteAllText(filePath, json);
        }
    }
}
