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
    public class FoldersController : ControllerBase
    {
        Lincs lincs = new Lincs();
        Folders_Class folders = new Folders_Class();
        // GET: api/Folders
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Folders/5
        [HttpGet("{id}", Name = "Get")]
        public string DeliteFile(string id)
        {
            folders.Delete_Directory(lincs.Users_User_Folder_Strings()+ id);
            return "Файлы удалены";
        }

        // POST: api/Folders
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Folders/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
