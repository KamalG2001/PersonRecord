using PersonRecord.Models;


namespace PersonRecord.Export
{
    public interface IExport
    {
        void Export(List<User> people, string filePath);
    }
}
