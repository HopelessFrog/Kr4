using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model.Entities;
using Syncfusion.XlsIO;

namespace Kr4.Services
{
    public class ExcelExportService
    {
        public void ExportToExcel(List<IAstronomicalObject> listForExport, string path )
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;

                //Reads input Excel stream as a workbook
                IWorkbook workbook = application.Workbooks.Open(File.OpenRead(path));
                IWorksheet sheet = workbook.Worksheets[0];

            }
        }
        }
}
