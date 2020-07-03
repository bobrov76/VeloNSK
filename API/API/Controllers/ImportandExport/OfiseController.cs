using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace API.Controllers.ImportandExport
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfiseController : ControllerBase
    {
        private readonly testContext _context;

        public OfiseController(testContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("ImportOfise")]
        public string ImportCustomer()
        {

            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Import\";
            string fileName = $"ImportOfise{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {

                ExcelWorksheet workSheet = package.Workbook.Worksheets["Офисы"];
                workSheet.Cells[1, 1].Value = "Офисы";
                workSheet.Cells[1, 1].Style.Font.Size = 14;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells["A1:J1"].Merge = true;
                int totalRows = workSheet.Dimension.Rows;

                List<Ofise> customerList = new List<Ofise>();

                for (int i = 2; i <= totalRows; i++)
                {
                    customerList.Add(new Ofise
                    {
                        NameOfis = workSheet.Cells[i, 1].Value.ToString(),
                        Descriptio = workSheet.Cells[i, 2].Value.ToString(),
                        TimeOpen = (TimeSpan)workSheet.Cells[i, 3].Value,
                        TimeClouse = (TimeSpan)workSheet.Cells[i, 4].Value,
                        Latitude = workSheet.Cells[i, 5].Value.ToString(),
                        Longitude = workSheet.Cells[i, 6].Value.ToString(),
                        Logo = workSheet.Cells[i, 7].Value.ToString(),
                    });
                }
                _context.Ofise.AddRange(customerList);
                _context.SaveChanges();

                return "Файл успешно импортирован";
            }
        }

        [HttpGet]
        [Route("ExportOfise")]
        public string ExportCustomer()
        {
            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Export\";
            string fileName = $"ExportOfise{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                IList<Ofise> customerList = _context.Ofise.ToList();
                package.Workbook.Properties.Author = "VeloNSKAPI";
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Офисы");
                worksheet.Cells[1, 1].Value = "Офисы";
                worksheet.Cells[1, 1].Style.Font.Size = 14;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells["A1:J1"].Merge = true;

                int totalRows = customerList.Count();

                worksheet.Cells[2, 1].Value = "Название офиса";
                worksheet.Cells[2, 2].Value = "Описание";
                worksheet.Cells[2, 3].Value = "Время открытия";
                worksheet.Cells[2, 4].Value = "Время закрытия";
                worksheet.Cells[2, 5].Value = "Ширин расположения";
                worksheet.Cells[2, 6].Value = "Долгота расположения";
                worksheet.Cells[2, 7].Value = "Изображение";
                for (int row = 1; row <= totalRows + 2; row++)
                {
                    for (int column = 1; column <= 7; column++)
                    {
                        worksheet.Column(column).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Column(column).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[row, column].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                        worksheet.Cells[row, column].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                        worksheet.Cells[row, column].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                        if (row % 2 == 0)
                        {
                            worksheet.Cells[row, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[row, column].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BurlyWood);
                        }
                    }
                }
                int i = 0;
                for (int row = 3; row <= totalRows + 2; row++)
                {
                    worksheet.Cells[row, 1].Value = customerList[i].NameOfis;
                    worksheet.Cells[row, 2].Value = customerList[i].Descriptio;
                    worksheet.Cells[row, 3].Value = customerList[i].TimeOpen;
                    worksheet.Cells[row, 4].Value = customerList[i].TimeClouse;
                    worksheet.Cells[row, 5].Value = customerList[i].Latitude;
                    worksheet.Cells[row, 6].Value = customerList[i].Longitude;
                    worksheet.Cells[row, 7].Value = customerList[i].Logo;
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                package.Save();
            }
            return "Файл успешно экспортирован";
        }
    }
}