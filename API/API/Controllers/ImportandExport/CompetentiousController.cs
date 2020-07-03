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
    public class CompetentiousController : ControllerBase
    {
        private readonly testContext _context;
        string rootFolders = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Import\";
        public CompetentiousController(testContext context)
        {
            _context = context;
        }
        public class FileUPloadAPI
        {
            public IFormFile files { get; set; }
        }

        [HttpPost]
        public async Task<string> Post([FromForm] FileUPloadAPI objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(rootFolders))
                    {
                        Directory.CreateDirectory(rootFolders);
                    }
                    using (FileStream fileStream = System.IO.File.Create(rootFolders + objFile.files.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault()))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        string masage = ImportCustomer(objFile.files.FileName);
                        return masage;
                    }
                }
                else { return "Error file uploads"; }
            }
            catch (Exception ex) { return ex.Message.ToString(); }

        }


        public string ImportCustomer(string patchname)
        {
            string result_msage = "";
            int count_i = 0;
            int count_row = 0;
            string fileName = patchname;

            
            FileInfo file = new FileInfo(Path.Combine(rootFolders, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Пользователи"];
                workSheet.Cells[1, 1].Value = "Компетенции";
                workSheet.Cells[1, 1].Style.Font.Size = 14;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells["A1:B1"].Merge = true;
                int totalRows = workSheet.Dimension.Rows;

                List<Competentions> customerList = new List<Competentions>();

                for (int i = 3; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null && workSheet.Cells[i, 2].Value != null)
                    {
                                                   
                            customerList.Add(new Competentions
                            {
                                IdDistantion= Convert.ToInt32(workSheet.Cells[i, 1].Value),
                                Date= Convert.ToDateTime(workSheet.Cells[i, 2].Value)
                            });
                            count_i = i - 3;
                            result_msage = "Все строки успешно сохранены, всего" + count_i + " строк";
                       
                    }
                    else
                    {
                        result_msage = "Сохранено: " + (count_i) + " строк. " + (count_row - count_i) + " строка не заполнена, проверьте и попробуйте снова";
                        break;
                    }
                }
                _context.Competentions.AddRange(customerList);
                _context.SaveChanges();

                return result_msage;
            }
        }



        [HttpGet]
        [Route("ExportSimple")]
        public string GetSimple()
        {
            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Simple\";
            string fileName = "CompetentionsExportSimple.xlsx";
            if (!System.IO.File.Exists(rootFolder+fileName))
            { 
            
            }
            else
            {
                System.IO.File.Delete(rootFolder + fileName);
            }
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                IList<Distantion> customerList = _context.Distantion.ToList();
                package.Workbook.Properties.Author = "VeloNSKAPI";
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Пользователи");
                worksheet.Cells[1, 1].Value = "Компетенции";
                worksheet.Cells[1, 1].Style.Font.Size = 14;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells["A1:B1"].Merge = true;

                worksheet.Cells[1, 5].Value = "Дистанции";
                worksheet.Cells[1, 5].Style.Font.Size = 14;
                worksheet.Cells[1, 5].Style.Font.Bold = true;
                worksheet.Cells["E1:G1"].Merge = true;

                int totalRows = customerList.Count();
                worksheet.Cells[2, 1].Value = "ID дистанции";
                worksheet.Cells[2, 2].Value = "Дата проведения";

                worksheet.Cells[2, 5].Value = "ID дистанции";                
                worksheet.Cells[2, 6].Value = "Название дистанции";
                worksheet.Cells[2, 7].Value = "Длинна(М)";
                int i = 0;
                for (int row = 3; row <= totalRows + 2; row++)
                {
                    worksheet.Cells[row, 5].Value = customerList[i].IdDistantion;
                    worksheet.Cells[row, 6].Value = customerList[i].NameDistantion;
                    worksheet.Cells[row, 7].Value = customerList[i].Lengs;
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                package.Save();
            }
            return "http://90.189.158.10/Simple/"+fileName;

        }


        [HttpGet]
        [Route("ExportCompetentious")]
        public string ExportCustomer()
        {
            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Export\";
            string fileName = $"ExportDistantions{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            ExcelPackage package = new ExcelPackage(file);
            
                IList<Competentions> customerList = _context.Competentions.ToList();
                package.Workbook.Properties.Author = "VeloNSKAPI";
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Пользователи");
                worksheet.Cells[1, 1].Value = "Компетенции";
                worksheet.Cells[1, 1].Style.Font.Size = 14;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells["A1:C1"].Merge = true;

                int totalRows = customerList.Count();

                worksheet.Cells[2, 1].Value = "Название дистанции";
                worksheet.Cells[2, 2].Value = "Дата проведения";
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
                    IList<Distantion> distantions = _context.Distantion.ToList();
                    var distantions_name = distantions.Where(p => p.IdDistantion == customerList[i].IdDistantion);                
                    if (distantions_name.Count()!=0)
                    {
                        foreach (var item in distantions_name)
                        {
                            worksheet.Cells[row, 1].Value = item.NameDistantion;
                        }                    
                        worksheet.Cells[row, 2].Value = customerList[i].Date.ToString("y.yy.yyyy"); 
                    }
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                package.Save();
            
            return "http://90.189.158.10/Export/" + fileName;
        
        }
    }
}