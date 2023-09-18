using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kr4.Model.Entities;
using Kr4.Services.Interface;
using Spire.Xls;

namespace Kr4.Services
{
    public class ExcelExportService : IExcelExportService
    {
        public void ExportToExcel(IEnumerable<IAstronomicalObject> listForExport, string path )
        {
            if(path == String.Empty)
                return;

            if(listForExport is IEnumerable<Planet>)
                ExportPlanetsToExcel(listForExport.Cast<Planet>().ToList(),path);
            else if (listForExport is IEnumerable<Star>)
                ExportStarsToExcel(listForExport.Cast<Star>().ToList(), path);
            else if (listForExport is IEnumerable<Galaxy>)
                ExportGalaxiesToExcel(listForExport.Cast<Galaxy>().ToList(), path);
        }

        private void ExportGalaxiesToExcel(List<Galaxy> galaxies, string filePath)
        {
            Workbook workbook = new Workbook();

            var worksheet = workbook.Worksheets.Add("Galaxies");


            worksheet.Range[1, 1].Value = "Id";
            worksheet.Range[1, 2].Value = "Type";
            worksheet.Range[1, 3].Value = "Name";
            worksheet.Range[1, 4].Value = "DistanceFromEarth";
            worksheet.Range[1, 5].Value = "Age";


            for (int i = 0; i < galaxies.Count; i++)
            {
                var galaxy = galaxies[i];
                worksheet.Range[i + 2, 1].Value = galaxy.Id.ToString();
                worksheet.Range[i + 2, 2].Value = galaxy.Type?.Name;
                worksheet.Range[i + 2, 3].Value = galaxy.Name;
                worksheet.Range[i + 2, 4].Value = galaxy.DistanceFromEarth.ToString();
                worksheet.Range[i + 2, 5].Value = galaxy.Age.ToString();
            }

            worksheet.AllocatedRange.AutoFitColumns();
            CellStyle style = workbook.Styles.Add("newStyle");
            style.Font.IsBold = true;
            worksheet.Range[1, 1, 1, 4].Style = style;

            workbook.SaveToFile(filePath, ExcelVersion.Version2016);
        }


        private void ExportPlanetsToExcel(List<Planet> planets, string filePath)
        {
            Workbook workbook = new Workbook();

            var worksheet = workbook.Worksheets.Add("Planets");


            worksheet.Range[1, 1].Value = "Id";
            worksheet.Range[1, 2].Value = "Size";
            worksheet.Range[1, 3].Value = "OrbitalPeriod";
            worksheet.Range[1, 4].Value = "Name";
            worksheet.Range[1, 5].Value = "DistanceFromEarth";
            worksheet.Range[1, 6].Value = "Age";


            for (int i = 0; i < planets.Count; i++)
            {
                var planet = planets[i];
                worksheet.Range[i + 2, 1].Value = planet.Id.ToString();
                worksheet.Range[i + 2, 2].Value = planet.Size.ToString();
                worksheet.Range[i + 2, 3].Value = planet.OrbitalPeriod.ToString();
                worksheet.Range[i + 2, 4].Value = planet.Name;
                worksheet.Range[i + 2, 5].Value = planet.DistanceFromEarth.ToString();
                worksheet.Range[i + 2, 6].Value = planet.Age.ToString();
            }

            worksheet.AllocatedRange.AutoFitColumns();
            CellStyle style = workbook.Styles.Add("newStyle");
            style.Font.IsBold = true;
            worksheet.Range[1, 1, 1, 4].Style = style;


            workbook.SaveToFile(filePath, ExcelVersion.Version2016);
        }

        private void ExportStarsToExcel(List<Star> stars, string filePath)
        {
            Workbook workbook = new Workbook();
            var worksheet = workbook.Worksheets.Add("Stars");


            worksheet.Range[1, 1].Value = "Id";
            worksheet.Range[1, 2].Value = "Class";
            worksheet.Range[1, 3].Value = "Luminosity";
            worksheet.Range[1, 4].Value = "Name";
            worksheet.Range[1, 5].Value = "DistanceFromEarth";
            worksheet.Range[1, 6].Value = "Age";


            for (int i = 0; i < stars.Count; i++)
            {
                var star = stars[i];
                worksheet.Range[i + 2, 1].Value = star.Id.ToString();
                worksheet.Range[i + 2, 2].Value = star.Class?.Name;
                worksheet.Range[i + 2, 3].Value = star.Luminosity.ToString();
                worksheet.Range[i + 2, 4].Value = star.Name;
                worksheet.Range[i + 2, 5].Value = star.DistanceFromEarth.ToString();
                worksheet.Range[i + 2, 6].Value = star.Age.ToString();
            }

            worksheet.AllocatedRange.AutoFitColumns();
            CellStyle style = workbook.Styles.Add("newStyle");
            style.Font.IsBold = true;
            worksheet.Range[1, 1, 1, 4].Style = style;

            
            workbook.SaveToFile(filePath, ExcelVersion.Version2016);

        }
    }
}

