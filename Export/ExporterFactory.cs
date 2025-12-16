using PersonRecord.Models.Enum;

namespace PersonRecord.Export
{
    public interface IExporterFactory
    {
        IExport CreateExporter(ExportFormat format);
    }

    public class ExporterFactory : IExporterFactory
    {
        public IExport CreateExporter(ExportFormat format)
        {
            return format switch
            {
                ExportFormat.Txt => new TxtExport(),
                ExportFormat.Json => new JsonExport(),
                ExportFormat.Csv => new CsvExporter(),
            };
        }
    }
}