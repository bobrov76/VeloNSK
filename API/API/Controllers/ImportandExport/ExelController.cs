using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.HelpClass;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExelController : ControllerBase
    {

        private readonly testContext _context;
        string rootFolders = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Import\";
        public ExelController(testContext context)
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
        PasswordGenerate passwordGenerate = new PasswordGenerate();
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
                workSheet.Cells[1, 1].Value = "Участники";
                workSheet.Cells[1, 1].Style.Font.Size = 14;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells["A1:J1"].Merge = true;
                int totalRows = workSheet.Dimension.Rows;

                List<UserInfo> customerList = new List<UserInfo>();

                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null && workSheet.Cells[i, 2].Value != null && 
                            workSheet.Cells[i, 3].Value != null && workSheet.Cells[i, 4].Value != null && 
                                workSheet.Cells[i, 5].Value != null && workSheet.Cells[i, 6].Value != null && 
                                    workSheet.Cells[i, 7].Value != null && workSheet.Cells[i, 8].Value != null && 
                                        workSheet.Cells[i, 9].Value != null && workSheet.Cells[i, 10].Value != null)
                    {
                        var distantions_test = _context.UserInfo.Where(p => p.Login== workSheet.Cells[i, 1].Value.ToString());
                        if (distantions_test.Count() == 0)
                        {
                            
                               string logo = "http://90.189.158.10/folders/nophoto.png"; 
                            
                            customerList.Add(new UserInfo
                            {
                                Login = workSheet.Cells[i, 1].Value.ToString(),
                                Password = passwordGenerate.Get_Password_Two_Autentification(workSheet.Cells[i, 2].Value.ToString()),
                                Rol = workSheet.Cells[i, 3].Value.ToString(),
                                Fam = workSheet.Cells[i, 4].Value.ToString(),
                                Name = workSheet.Cells[i, 5].Value.ToString(),
                                Patronimic = workSheet.Cells[i, 6].Value.ToString(),
                                Years = (short)workSheet.Cells[i, 7].Value,
                                Logo = logo,
                                Email = workSheet.Cells[i, 8].Value.ToString(),
                                Isman = (bool)workSheet.Cells[i, 9].Value,
                                IdHelth = (short)workSheet.Cells[i, 10].Value,

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
                _context.UserInfo.AddRange(customerList);
                _context.SaveChanges();

                return result_msage;
            }
        }

        [HttpGet]
        [Route("ExportUser")]
        public string ExportCustomer()
        {
            string rootFolder = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Export\";
            string fileName = $"ExportUsers{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                IList<UserInfo> customerList = _context.UserInfo.ToList();
                package.Workbook.Properties.Author = "VeloNSKAPI";
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Пользователи");
                worksheet.Cells[1, 1].Value = "Участники";
                worksheet.Cells[1, 1].Style.Font.Size = 14;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells["A1:I1"].Merge = true;

                int totalRows = customerList.Count();

                worksheet.Cells[2, 1].Value = "Login";
                worksheet.Cells[2, 2].Value = "Роль";
                worksheet.Cells[2, 3].Value = "Фамилия";
                worksheet.Cells[2, 4].Value = "E-mail";
                worksheet.Cells[2, 5].Value = "Имя";
                worksheet.Cells[2, 6].Value = "Отчество";
                worksheet.Cells[2, 7].Value = "Возраст";
                worksheet.Cells[2, 8].Value = "Пол";
                worksheet.Cells[2, 9].Value = "Статус здоровья";
                for (int row = 1; row <= totalRows + 2; row++)
                {
                    for (int column = 1; column <= 9; column++)
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
                string pol = "";
                for (int row = 3; row <= totalRows + 2; row++)
                {
                    worksheet.Cells[row, 1].Value = customerList[i].Login;
                    worksheet.Cells[row, 2].Value = customerList[i].Rol;
                    worksheet.Cells[row, 3].Value = customerList[i].Fam;
                    worksheet.Cells[row, 4].Value = customerList[i].Email;
                    worksheet.Cells[row, 5].Value = customerList[i].Name;
                    worksheet.Cells[row, 6].Value = customerList[i].Patronimic;
                    worksheet.Cells[row, 7].Value = customerList[i].Years;
                    if (customerList[i].Isman) { pol = "Мужской"; }
                    else { pol = "Женский"; }
                    worksheet.Cells[row, 8].Value = pol;
                    HelthStatus person = _context.HelthStatus.FirstOrDefault(x => x.IdHealth == customerList[i].IdHelth);
                    worksheet.Cells[row, 9].Value = person.NameHealth;
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                package.Save();
            }
            return "http://90.189.158.10/Export/" + fileName;
        }
    }
}