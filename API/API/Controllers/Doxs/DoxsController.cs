using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GemBox.Document;
using GemBox.Document.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Doxs
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoxsController : ControllerBase
    {
        public class FileUPloadAPI
        {
            public string Id { get; set; }
            public string masage { get; set; }
            public string Id_patisipant { get; set; }
        }
        private readonly testContext _context;
        public DoxsController(testContext context)
        {
            _context = context;
        }
        WordClass ww = new WordClass();
           // GET: api/Doxs
           [HttpGet]
        public string Get()
        {
            
            return "ok";
        }
        public string criate_gramots(int id,int res_pat)
        {
            IList<ResultParticipation> resultParticipations = _context.ResultParticipation.ToList();
            IList<Participation> participations = _context.Participation.ToList();
            IList<Distantion> distantions = _context.Distantion.ToList();
            IList<Competentions> competentions = _context.Competentions.ToList();
            IList<UserInfo> infoUsers = _context.UserInfo.ToList();
            var info = from r in resultParticipations
                       join p in participations on r.IdParticipation equals p.IdParticipation
                       join c in competentions on p.IdCompetentions equals c.IdCompetentions
                       join d in distantions on c.IdDistantion equals d.IdDistantion
                       join u in infoUsers on p.IdUser equals u.IdUsers
                       select new
                       {
                           r.ResultTime,
                           r.Mesto,
                           d.NameDistantion,
                           u.Name,
                           u.Fam,
                           u.Patronimic,
                           r.IdParticipation,
                           r.IdResultParticipation,
                           u.IdUsers,
                           u.Login,
                           c.Date
                       };
            var itog = info.FirstOrDefault(x => x.IdUsers == id && x.IdResultParticipation==res_pat);
            string fio = itog.Fam + " " + itog.Name + " " + itog.Patronimic;
            string name_gramots = "Сертификат";
            if (itog.Mesto <= 3) { name_gramots = "Диплом"; }
            DocumentModel doc = null;
            try
            {
                string path_file = @"C:\Users\Yulia\Desktop\VeloAPI\resours\folders\temp.docx";
                string folder_name = itog.Login.Replace("+", string.Empty).Replace(" ", string.Empty).Replace("-", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);
                    string destFileName = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Users\User" + folder_name + @"\Doc\" + (itog.IdUsers + itog.NameDistantion + itog.Date).Replace(" ", string.Empty).Replace(":", string.Empty).Replace(".", string.Empty) + ".docx";
                if (!System.IO.File.Exists(destFileName))
                {
                    FileInfo fn = new FileInfo(path_file);
                    fn.CopyTo(destFileName, true);
                    // Путь до файла
                    string[] data = new string[] { name_gramots, fio, itog.Mesto.ToString(), itog.NameDistantion, itog.Date.ToShortDateString() };
                    // Обязательная строка, указываем, что мы используем лимитированную версию
                    ComponentInfo.SetLicense("FREE-LIMITED-KEY");
                    // Загружаем в память наш документ
                    doc = DocumentModel.Load(destFileName);
                    // Коллекция закладок
                    BookmarkCollection wBookmarks = doc.Bookmarks;
                    // ContentRange - это область содержимого в документе
                    ContentRange wRange;
                    int i = 0;
                    // Пробегаем по всем закладкам в документе
                    foreach (Bookmark mark in doc.Bookmarks)
                    {
                        // Получаем содержимое закладки
                        wRange = mark.GetContent(false);
                        // Загружаем туда нужный текст
                        wRange.LoadText(data[i].ToString());
                        i++;
                    }
                    // Сохраняем изменения в нашем документе
                    doc.Save(destFileName);
                    doc = null;

                    return "http://90.189.158.10/Users/User"+ folder_name + "/Doc/"+(itog.IdUsers + itog.NameDistantion + itog.Date).Replace(" ", string.Empty).Replace(":", string.Empty).Replace(".", string.Empty) + ".docx";
                }
                else { return "http://90.189.158.10/Users/User" + folder_name + "/Doc/" + (itog.IdUsers + itog.NameDistantion + itog.Date).Replace(" ", string.Empty).Replace(":", string.Empty).Replace(".", string.Empty) + ".docx"; }
            }
            catch 
            {
                doc = null;
                return null;
            }            
        }


        public string criate_daidg(int id,int id_par)
        {
            IList<ResultParticipation> resultParticipations = _context.ResultParticipation.ToList();
            IList<Participation> participations = _context.Participation.ToList();
            IList<Distantion> distantions = _context.Distantion.ToList();
            IList<Competentions> competentions = _context.Competentions.ToList();
            IList<UserInfo> infoUsers = _context.UserInfo.ToList();
            var info = from p in participations 
                       join c in competentions on p.IdCompetentions equals c.IdCompetentions
                       join d in distantions on c.IdDistantion equals d.IdDistantion
                       join u in infoUsers on p.IdUser equals u.IdUsers
                       select new
                       {                          
                           d.NameDistantion,
                           u.Name,
                           u.Fam,
                           u.Patronimic,
                           p.IdParticipation,
                           u.IdUsers,
                           u.Login,
                           c.Date
                       };
            var itog = info.FirstOrDefault(x => x.IdUsers == id && x.IdParticipation== id_par);
            string fio = itog.Fam + " " + itog.Name + " " + itog.Patronimic;
            
            DocumentModel doc = null;
            try
            {
                string path_file = @"C:\Users\Yulia\Desktop\VeloAPI\resours\folders\baige.docx";
                string folder_name = itog.Login.Replace("+", string.Empty).Replace(" ", string.Empty).Replace("-", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);
                string destFileName = @"C:\Users\Yulia\Desktop\VeloAPI\resours\Users\User" + folder_name + @"\Doc\Baidg" + (itog.IdUsers + itog.NameDistantion + itog.Date).Replace(" ", string.Empty).Replace(":", string.Empty).Replace(".", string.Empty) + ".docx";
                if (!System.IO.File.Exists(destFileName))
                {
                    FileInfo fn = new FileInfo(path_file);
                    fn.CopyTo(destFileName, true);                   
                    // Путь до файла
                    string[] data = new string[] { " " + fio, itog.NameDistantion, " "+itog.Date.ToString("f")};
                    // Обязательная строка, указываем, что мы используем лимитированную версию
                    ComponentInfo.SetLicense("FREE-LIMITED-KEY");
                    // Загружаем в память наш документ
                    doc = DocumentModel.Load(destFileName);
                    // Коллекция закладок
                    BookmarkCollection wBookmarks = doc.Bookmarks;
                    // ContentRange - это область содержимого в документе
                    ContentRange wRange;
                    int i = 0;
                    // Пробегаем по всем закладкам в документе
                    foreach (Bookmark mark in doc.Bookmarks)
                    {
                        // Получаем содержимое закладки
                        wRange = mark.GetContent(false);
                        // Загружаем туда нужный текст
                        wRange.LoadText(data[i].ToString());
                        i++;
                    }
                    // Сохраняем изменения в нашем документе
                    doc.Save(destFileName);
                    doc = null;
                return "http://90.189.158.10/Users/User" + folder_name + "/Doc/Baidg" + (itog.IdUsers + itog.NameDistantion + itog.Date).Replace(" ", string.Empty).Replace(":", string.Empty).Replace(".", string.Empty) + ".docx";
                }
                else { return "http://90.189.158.10/Users/User" + folder_name + "/Doc/Baidg" + (itog.IdUsers + itog.NameDistantion + itog.Date).Replace(" ", string.Empty).Replace(":", string.Empty).Replace(".", string.Empty) + ".docx"; }
            }
            catch 
            {
                doc = null;
                return null;
            }
        }

        private string criate_itog(int id_par) 
        {
            IList<ResultParticipation> resultParticipations = _context.ResultParticipation.ToList();
            IList<Participation> participations = _context.Participation.ToList();
            IList<Distantion> distantions = _context.Distantion.ToList();
            IList<Competentions> competentions = _context.Competentions.ToList();
            IList<UserInfo> infoUsers = _context.UserInfo.ToList();
            var info = from p in participations
                       join c in competentions on p.IdCompetentions equals c.IdCompetentions
                       join d in distantions on c.IdDistantion equals d.IdDistantion
                       join u in infoUsers on p.IdUser equals u.IdUsers
                       join itg in resultParticipations on p.IdParticipation equals itg.IdParticipation
                       select new
                       {
                           d.NameDistantion,
                           u.Name,
                           u.Fam,
                           u.Patronimic,
                           itg.Mesto,
                           itg.ResultTime,
                           c.IdCompetentions,
                           p.IdParticipation
                       };
            info = info.Where(x => x.IdParticipation == id_par);
            var itg_name = info.FirstOrDefault(x => x.IdParticipation == id_par);
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            int rowCount = info.Count();
            int columnCount = 3;

            var document = new DocumentModel();

            var section = new Section(document);
            document.Sections.Add(section);
            section.Blocks.Add(new Paragraph(document, "Итоги соревнований"));            
           
            // Create a table with 100% width.
            var table = new Table(document);

            for (int r = 0; r < rowCount; r++)
            {
                // Create a row and add it to table.
                var row = new TableRow(document);
                table.Rows.Add(row);                
                string[] hand_data = new string[] { "ФИО", "Название дистанции", "Занятое место" };

                for (int c = 0; c < columnCount; c++)
                {

                    var cell = new TableCell(document);
                    row.Cells.Add(cell);                    
                        var paragraph = new Paragraph(document, hand_data[c]);//что выводим
                        cell.Blocks.Add(paragraph);
                }
            }
            table.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
            section.Blocks.Add(table);
            foreach (var item in info)
            {
               

                for (int r = 0; r < rowCount; r++)
                {
                // Create a row and add it to table.
                var row = new TableRow(document);
                table.Rows.Add(row);
                
                    string fio = item.Fam + " " + item.Name + " " + item.Patronimic;
                    string[] data = new string[] { fio, item.NameDistantion, item.Mesto.ToString() };
                    string[] hand_data = new string[] { "ФИО","Название дистанции","Занятое место" };

                    for (int c = 0; c < columnCount; c++)
                    {
                        var cell = new TableCell(document);
                        row.Cells.Add(cell);
                            var paragraph = new Paragraph(document, data[c]);//что выводим
                            cell.Blocks.Add(paragraph);    
                    }
                }
            }
            section.Blocks.Add(new Paragraph(document, " "));
            section.Blocks.Add(new Paragraph(document, "Колличество участников : "+ info.Count()));
            string name = itg_name.NameDistantion.Replace(" ", string.Empty) + $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.docx";

            document.Save(@"C:\Users\Yulia\Desktop\VeloAPI\resours\folders\Itogi\" + "ItidiSorevnovantia"+name);
            return "http://90.189.158.10/resours/folders/Itogi/" + name;
        }

        // GET: api/Doxs/5        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string path = criate_itog(id);  // criate_daidg(id,10); //criate_gramots(id);            
            return path;
        }

        // POST: api/Doxs
        [HttpPost]
        public async Task<string> PostAsync([FromForm] FileUPloadAPI masages)
        {
            try
            {
                string masage = "";
                switch (masages.masage)
                {
                    case "criate_gramots":   masage = criate_gramots(Convert.ToInt32(masages.Id), Convert.ToInt32(masages.Id_patisipant)); break;
                    case "criate_daidg": masage = criate_daidg(Convert.ToInt32(masages.Id), Convert.ToInt32(masages.Id_patisipant)); break;
                    case "criate_itog": masage = criate_itog(Convert.ToInt32(masages.Id_patisipant)); break;
                }
                return masage;
            }
            catch { return "Error"; }
        }
    }
}
