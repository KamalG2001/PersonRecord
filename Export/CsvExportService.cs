using PersonRecord.Models;
using System.IO;

namespace PersonRecord.Export
{
    public class CsvExportService : IExport
    {
        public void Export(List<User> people, string filePath)
        {
            var test = new User();
            using var writer = new StreamWriter(filePath);

            writer.WriteLine("Name,Surname,Age,Job");
            foreach (var p in people)
            {
                writer.WriteLine($"{p.Name},{p.Surname},{p.Age},{p.Job}");
            }
        }
    }
}
