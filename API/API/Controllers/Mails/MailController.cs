using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.HelpClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Mails
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        PasswordGenerate passwordGenerate = new PasswordGenerate();
        private testContext _context;
        public MailController(testContext context)
        {
            _context = context;
        }
        public class FileUPloadAPI
        {
            public string Id { get; set; }
            public string masage { get; set; }
        }
        // GET: api/Mail
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Mail/5
      //  [HttpGet("{id}")]
        public async Task<string> ReplisPassword(int id)
        {
            var participant = await _context.UserInfo.FirstOrDefaultAsync(x => x.IdUsers == Convert.ToInt32(id));
            if (participant != null)
            {
                string password = passwordGenerate.GetPassword();
                string hash = passwordGenerate.Get_Password_Two_Autentification(password);
                EmailService emailService = new EmailService();
                emailService.SendEmail(participant.Email, "Восстановление пароля", "Ваш новый пароль: " + password);
                var customer = _context.UserInfo.Where(c => c.IdUsers == id).FirstOrDefault();
                customer.Password = hash;
                _context.SaveChanges();
                return "True";
            }
           
             return "Fals";
        }

        private async Task<string> DoubleAutentifity(int id)
        {
            var participant = await _context.UserInfo.FirstOrDefaultAsync(x => x.IdUsers == Convert.ToInt32(id));
            if (participant != null)
            {
                string password = passwordGenerate.Get_Passwd_Small();
                EmailService emailService = new EmailService();
                emailService.SendEmail(participant.Email, "Двуфакторная аутентификация", password);
                return password;
            }
            // если пользователя не найдено
            return "Fals";
        }

        private async Task<string> NewUser(int id)
        {
            var participant = await _context.UserInfo.FirstOrDefaultAsync(x => x.IdUsers == Convert.ToInt32(id));
            if (participant != null)
            {
                string password = passwordGenerate.Get_Passwd_Small();
                EmailService emailService = new EmailService();
                emailService.SendEmail(participant.Email, "Новый пользователь", "Вы успешно зарегистрировались в приложении VeloNSK.Ваш логин: "+participant.Login);
                return "True";
            }
            // если пользователя не найдено
            return "Fals";
        }

        // POST: api/Mail
        [HttpPost]
        public async Task<string> PostAsync([FromForm] FileUPloadAPI masages)
        {
            try
            {
                string masage = "";
                switch (masages.masage)
                {
                    case "ReplisPassword": masage = await ReplisPassword(Convert.ToInt32(masages.Id)); break;
                    case "DoubleAutentifity": masage = await DoubleAutentifity(Convert.ToInt32(masages.Id)); break;
                    case "NewUser": masage = await NewUser(Convert.ToInt32(masages.Id)); break;
                }
                return masage;
            }
            catch { return "Error"; }
        }

        // PUT: api/Mail/5
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
