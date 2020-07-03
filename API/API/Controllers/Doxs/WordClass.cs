using GemBox.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Controllers.Doxs
{
    public class WordClass
    {
       

        //public string criate_gramots(int id)
        //{
        //    IList<ResultParticipation> resultParticipations = _context.ResultParticipation.ToList();
        //    IList<Participation> participations = _context.Participation.ToList();
        //    IList<Distantion> distantions = _context.Distantion.ToList();
        //    IList<Competentions> competentions = _context.Competentions.ToList();
        //    IList<UserInfo> infoUsers = _context.UserInfo.ToList();
        //    var info = from r in resultParticipations
        //               join p in participations on r.IdParticipation equals p.IdParticipation
        //               join c in competentions on p.IdCompetentions equals c.IdCompetentions
        //               join d in distantions on c.IdDistantion equals d.IdDistantion
        //               join u in infoUsers on p.IdUser equals u.IdUsers
        //               select new
        //               {                         
        //                   r.ResultTime,
        //                   r.Mesto,
        //                   d.NameDistantion,
        //                   u.Name,
        //                   u.Fam,
        //                   u.Patronimic,
        //                   u.IdUsers,
        //                   c.Date
        //               };            
        //        var itog = info.FirstOrDefault(x => x.IdUsers == id);
        //    string fio = itog.Fam + " " + itog.Name + " " + itog.Patronimic;
        //    string name_gramots = "Сертификат";
        //    if (itog.Mesto <= 3) {name_gramots = "Диплом"; }



        //        DocumentModel doc = null;
        //    try
        //    {
        //        string path_file = @"C:\Users\Yulia\Desktop\VeloAPI\resours\folders\temp.docx";
        //        string destFileName = @"C:\Users\Yulia\Desktop\VeloAPI\resours\" + $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.docx";

        //        FileInfo fn = new FileInfo(path_file);
        //        fn.CopyTo(destFileName, true);

        //        // Путь до файла
        //        string[] data = new string[]{ name_gramots,fio,itog.NameDistantion,itog.Date.ToShortTimeString()};
        //        // Обязательная строка, указываем, что мы используем лимитированную версию
        //        ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        //        // Загружаем в память наш документ
        //        doc = DocumentModel.Load(destFileName);
        //        // Коллекция закладок
        //        BookmarkCollection wBookmarks = doc.Bookmarks;
        //        // ContentRange - это область содержимого в документе
        //        ContentRange wRange;
        //        int i = 0;
        //        // Пробегаем по всем закладкам в документе
        //        foreach (Bookmark mark in doc.Bookmarks)
        //        {
        //            // Получаем содержимое закладки
        //            wRange = mark.GetContent(false);
        //            // Загружаем туда нужный текст
        //            wRange.LoadText(data[i].ToString());
        //            i++;
        //        }
        //        // Сохраняем изменения в нашем документе
        //        doc.Save(destFileName);
        //        doc = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        doc = null;
        //        Console.WriteLine("Во время выполнения программы произошла ошибка! Текст ошибки: {0}", ex.Message);
        //        Console.ReadLine();
        //    }
        //    return "ok";
        //}
    }
}
