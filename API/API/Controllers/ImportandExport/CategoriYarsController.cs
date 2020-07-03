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
    public class CategoriYarsController : ControllerBase
    {
        private readonly testContext _context;

        public CategoriYarsController(testContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("ImportCategoriYars")]
        public string ImportCustomer()
        {
            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Import\";
            string fileName = $"ImportCategoriYars{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {

                ExcelWorksheet workSheet = package.Workbook.Worksheets["Категория возраста участника"];
                workSheet.Cells[1, 1].Value = "Категория возраста участника";
                workSheet.Cells[1, 1].Style.Font.Size = 14;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells["A1:B1"].Merge = true;
                int totalRows = workSheet.Dimension.Rows;

                List<CategoriYars> customerList = new List<CategoriYars>();

                for (int i = 2; i <= totalRows; i++)
                {
                    customerList.Add(new CategoriYars
                    {
                        Ot = (short)workSheet.Cells[i, 1].Value,
                        Do = (short)workSheet.Cells[i, 1].Value,

                    });
                }
                _context.CategoriYars.AddRange(customerList);
                _context.SaveChanges();

                return "Файл успешно импортирован";
            }
        }

        [HttpGet]
        [Route("ExportCategoriYars")]
        public string ExportCustomer()
        {
            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Export\";
            string fileName = $"ExportCategoriYars{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                IList<CategoriYars> customerList = _context.CategoriYars.ToList();
                package.Workbook.Properties.Author = "VeloNSKAPI";
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Категория возраста участника");
                worksheet.Cells[1, 1].Value = "Категория возраста участника";
                worksheet.Cells[1, 1].Style.Font.Size = 14;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells["A1:B1"].Merge = true;

                int totalRows = customerList.Count();

                worksheet.Cells[2, 1].Value = "От лет";
                worksheet.Cells[2, 2].Value = "До лет";

                for (int row = 1; row <= totalRows + 2; row++)
                {
                    for (int column = 1; column <= 2; column++)
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
                    worksheet.Cells[row, 1].Value = customerList[i].Ot;
                    worksheet.Cells[row, 2].Value = customerList[i].Do;
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                package.Save();
            }
            return "Файл успешно экспортирован";
        }
    }
}