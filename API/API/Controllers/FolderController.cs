using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.HelpClass;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        Folders_Class folders = new Folders_Class();
        Lincs lincs = new Lincs();
        public FolderController(IWebHostEnvironment environment) 
        {
            _environment = environment;
        }

        public class FileUPloadAPI
        {
            public IFormFile files {get; set;}
            public string Id { get; set; }
        }
        
        [HttpGet("{id}")]
        public string CriateFile(string id)
        {
            folders.Create_Directory(lincs.Users_Folder_Strings(), @"User" + id);
            folders.Create_Directory(lincs.Users_User_Folder_Strings() + id, @"Photo");
            folders.Create_Directory(lincs.Users_User_Folder_Strings() + id, @"Doc");
            return "Файлы созданы";
        }

        [HttpPost]
        public async Task<string> Post([FromForm] FileUPloadAPI objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(lincs.Users_User_Folder_Strings() + objFile.Id + @"\Photo\"))
                    {
                        Directory.CreateDirectory(lincs.Users_User_Folder_Strings() + objFile.Id + @"\Photo\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(lincs.Users_User_Folder_Strings() + objFile.Id + @"\Photo\" + objFile.files.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault()))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "http://90.189.158.10/Users/User" + objFile.Id + "/Photo/" + objFile.files.FileName;
                    }
                }
                else { return "Error file uploads"; }
            }
            catch (Exception ex){ return ex.Message.ToString(); }
        
        }

        [Route("galeri")]
        [HttpPost]
        public async Task<string> Posts([FromForm] FileUPloadAPI objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(lincs.Images_Folder_Strings()+@"\"))
                    {
                        Directory.CreateDirectory(lincs.Images_Folder_Strings() + @"\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(lincs.Images_Folder_Strings() + @"\" + objFile.files.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault()))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "http://90.189.158.10/ImagesGaleri/" + objFile.files.FileName;
                    }
                }
                else { return "Error file uploads"; }
            }
            catch (Exception ex) { return ex.Message.ToString(); }

        }
    }
}