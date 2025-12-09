using PersonRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRecord.Export
{
    public class ExportService
    {
        private readonly IExport _export;

        public ExportService(IExport export)
        {
            _export = export;
        }

        public void Export(List<User> user, string filePath)
        {
            _export.Export(user, filePath);
        }
    }
}
