using PersonRecord.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRecord.Export
{
    public interface IExporterFactory
    {
        IExport CreateExporter(ExportFormat format);
    }
}
