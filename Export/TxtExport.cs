using PersonRecord.Models;
using System.IO;

namespace PersonRecord.Export
{
    public class TxtExport : IExport
    {
        public void Export(List<User> people, string filePath)
        {
            using StreamWriter writer = new StreamWriter(filePath);

            foreach (var p in people)
            {
                writer.WriteLine($"{p.Name},{p.Surname},{p.Age}, {p.Job}");
            }
        }
    }
}
