using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public static class Mail
    {
        public static  void SendMessage( string email,string subject,string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("medicallhospitall@gmail.com");
            mail.To.Add(new MailAddress(email));
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("medicallhospitall@gmail.com", "lztluawrqthzoytx");
            smtp.Send(mail);
        }
    }
}
