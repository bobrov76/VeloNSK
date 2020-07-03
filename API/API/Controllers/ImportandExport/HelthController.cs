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
    public class HelthController : ControllerBase
    {
        private readonly testContext _context;

        public HelthController(testContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("ImportHelthStatus")]
        public string ImportCustomer()
        {

            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Import\";
            string fileName = $"ImportHelthStatus{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Статус здоровья"];
                workSheet.Cells[1, 1].Value = "Статус здоровья";
                workSheet.Cells[1, 1].Style.Font.Size = 14;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                int totalRows = workSheet.Dimension.Rows;

                List<HelthStatus> customerList = new List<HelthStatus>();

                for (int i = 2; i <= totalRows; i++)
                {
                    customerList.Add(new HelthStatus
                    {
                        NameHealth = workSheet.Cells[i, 1].Value.ToString(),
                    });
                }
                _context.HelthStatus.AddRange(customerList);
                _context.SaveChanges();

                return "Файл успешно импортирован";
            }
        }

        [HttpGet]
        [Route("ExportHelthStatus")]
        public string ExportCustomer()
        {
            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Export\";
            string fileName = $"ExportHelthStatus{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                IList<HelthStatus> customerList = _context.HelthStatus.ToList();
                package.Workbook.Properties.Author = "VeloNSKAPI";
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Статус здоровья");
                worksheet.Cells[1, 1].Value = "Статус здоровья";
                worksheet.Cells[1, 1].Style.Font.Size = 14;
                worksheet.Cells[1, 1].Style.Font.Bold = true;

                int totalRows = customerList.Count();

                worksheet.Cells[2, 1].Value = "Статус здоровья";
                for (int row = 1; row <= totalRows + 2; row++)
                {
                    worksheet.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                    worksheet.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                    worksheet.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                    if (row % 2 == 0)
                    {
                        worksheet.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BurlyWood);
                    }

                }
                int i = 0;
                for (int row = 3; row <= totalRows + 2; row++)
                {
                    worksheet.Cells[row, 1].Value = customerList[i].NameHealth;
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                package.Save();
            }
            return "Файл успешно экспортирован";
        }
    }
}