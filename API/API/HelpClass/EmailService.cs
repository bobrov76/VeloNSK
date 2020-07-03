using System.Net;
using System.Net.Mail;

namespace API.HelpClass
{
    public class EmailService
    {
        public void SendEmail(string email, string subject, string message)
        {
            string servise_mail = "velo.nsk2020@mail.ru";
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(servise_mail);
            // кому отправляем
            MailAddress to = new MailAddress(email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // текст письма                   
            m.Body = message;
            // письмо представляет код html
            m.Subject = subject;
            m.IsBodyHtml = false;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.ru", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(servise_mail, "Vadim762905");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}

