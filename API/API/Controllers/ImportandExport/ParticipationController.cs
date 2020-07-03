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
    public class ParticipationController : ControllerBase
    {
        private readonly testContext _context;
        public ParticipationController(testContext context)
        {
            _context = context;
        }
        
        string rootFolders = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Import\";
       
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
                workSheet.Cells[1, 1].Value = "Соревнования";
                workSheet.Cells[1, 1].Style.Font.Size = 14;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells["A1:C1"].Merge = true;
                int totalRows = workSheet.Dimension.Rows;
                count_row = totalRows;
                List<Participation> customerList = new List<Participation>();

                for (int i = 3; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null && workSheet.Cells[i, 2].Value != null)
                    {
                        decimal length = Convert.ToDecimal(workSheet.Cells[i, 2].Value);
                        var distantions_test = _context.Distantion.Where(p => p.NameDistantion == workSheet.Cells[i, 1].Value.ToString() && p.Lengs == length);
                        if (distantions_test.Count() == 0)
                        {
                            if (workSheet.Cells[i, 3].Value == null)
                            {
                                workSheet.Cells[i, 3].Value = "Описание отсутствует";
                            }
                            customerList.Add(new Participation
                            {
                                IdUser = Convert.ToInt32(workSheet.Cells[i, 1].Value),
                                IdCompetentions = Convert.ToInt32(workSheet.Cells[i, 1].Value),
                                IdStatusVerification = Convert.ToBoolean(workSheet.Cells[i, 2].Value),
                                
                            });
                            count_i = i - 3;
                            result_msage = "Все строки успешно сохранены, всего" + count_i + " строк";
                        }

                    }
                    else
                    {
                        result_msage = "Сохранено: " + (count_i) + " строк. " + (count_row - count_i) + " строка не заполнена, проверьте и попробуйте снова";
                        break;
                    }
                }
                _context.Participation.AddRange(customerList);
                _context.SaveChanges();
            }

            return result_msage;
        }

        [HttpGet]
        [Route("ExportParticipation")]
        public string ExportCustomer()
        {
            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Export\";
            string fileName = $"ExportParticipation{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                IList<Participation> customerList = _context.Participation.ToList();
                package.Workbook.Properties.Author = "VeloNSKAPI";
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Соревнования");
                worksheet.Cells[1, 1].Value = "Участники";
                worksheet.Cells[1, 1].Style.Font.Size = 14;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells["A1:J1"].Merge = true;

                int totalRows = customerList.Count();

                worksheet.Cells[2, 1].Value = "Пользователь";
                worksheet.Cells[2, 2].Value = "Наименование дистанции";
                worksheet.Cells[2, 3].Value = "Дата проведения";
                worksheet.Cells[2, 4].Value = "Статус доступа к соревнованию";
                for (int row = 1; row <= totalRows + 2; row++)
                {
                    for (int column = 1; column <= 4; column++)
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

                IList<Competentions> competentions = _context.Competentions.ToList();
                IList<Distantion> dist = _context.Distantion.ToList();
                IList<UserInfo> userinfo = _context.UserInfo.ToList();
                var info = from p in customerList
                           join c in competentions on p.IdCompetentions equals c.IdCompetentions
                           join d in dist on c.IdDistantion equals d.IdDistantion
                           join u in userinfo on p.IdUser equals u.IdUsers
                           select new
                           {   p.IdParticipation,
                               u.Login,
                               c.Date,
                               p.IdStatusVerification,
                               d.NameDistantion
                           };

                for (int row = 3; row <= totalRows + 2; row++)
                {
                    info = info.Where(x=>x.IdParticipation == customerList[i].IdParticipation);
                    foreach (var item in info)
                    {
                        worksheet.Cells[row, 1].Value = item.Login;
                        worksheet.Cells[row, 3].Value = item.NameDistantion;
                        worksheet.Cells[row, 2].Value = item.Date;
                        worksheet.Cells[row, 4].Value = item.IdStatusVerification;
                    }
                    
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                package.Save();
            }
            return "http://90.189.158.10/Export/" + fileName;
        }


    }
}