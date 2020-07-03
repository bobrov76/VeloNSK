using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.HelpClass
{
    public class Import
    {
        private readonly testContext _context;
        string rootFolders = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Import\";
        public Import(testContext context)
        {
            _context = context;
        }
            

        public string ImportCustomer(string patchname)
        {

            
            FileInfo file = new FileInfo(Path.Combine(rootFolders, patchname));

            if (File.Exists(file.ToString()))
            {
                File.Delete(file.ToString());
            }

            
            
                // file.Open(@"C:\Users\Yulia\Desktop\VeloAPI\resours\Import\"+fileName,FileAccess.Read, FileShare.ReadWrite);
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet workSheet = package.Workbook.Worksheets["Пользователи"];
                    workSheet.Cells[1, 1].Value = "Дистанции";
                    workSheet.Cells[1, 1].Style.Font.Size = 14;
                    workSheet.Cells[1, 1].Style.Font.Bold = true;
                    workSheet.Cells["A1:C1"].Merge = true;
                    int totalRows = workSheet.Dimension.Rows;

                    List<Distantion> customerList = new List<Distantion>();

                    for (int i = 2; i <= totalRows; i++)
                    {
                        customerList.Add(new Distantion
                        {
                            NameDistantion = workSheet.Cells[i, 1].Value.ToString(),
                            Lengs = Convert.ToDecimal(workSheet.Cells[i, 2].Value),
                            Discriptions = workSheet.Cells[i, 3].Value.ToString()
                        });
                    }
                    _context.Distantion.AddRange(customerList);
                    _context.SaveChanges();
                }

                        return "Файл успешно импортирован";
        }
    }
}
