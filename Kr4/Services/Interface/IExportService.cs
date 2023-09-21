using Kr4.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kr4.Services.Interface
{
    public interface IExcelExportService
    {
        void ExportToExcel(IEnumerable<IAstronomicalObject> listForExport, string path);
    }
}
