using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.HelpClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        Lincs lincs = new Lincs();
        // GET: api/Images
        [HttpGet]
        public IEnumerable<string> Get()
        {
           return Get_Photos_Galery();            
        }

        public string[] Get_Photos_Galery()//Передача значений картинок
        {
            string[] imges = System.IO.Directory.GetFiles(lincs.Images_Folder_Strings());

            for (int i = 0; i < imges.Length; i++)
            {
                imges[i] = imges[i].Remove(0, lincs.Images_Folder_Strings().Length + 1);
                imges[i] = imges[i].Insert(0, lincs.API_Images_Strings() + "");
                
            }
            return imges;
        }
        
    }
}
