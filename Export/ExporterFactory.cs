using PersonRecord.Models.Enum;

namespace PersonRecord.Export
{
   
    public class ExporterFactory : IExporterFactory
    {
        public IExport CreateExporter(ExportFormat format)
        {
            return format switch
            {
                ExportFormat.Txt => new TxtExportService(),
                ExportFormat.Json => new JsonExportService(),
                ExportFormat.Csv => new CsvExportService(),
            };
        }
    }
}