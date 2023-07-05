using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;

namespace SurveyMonkey.EmailSender
{
    public class MailSendService
    {
        
        SmtpClient _smtpClient;
        string mail;
        public MailSendService()
        {
            var appSettings = ConfigurationManager.AppSettings;
            mail = appSettings.Get("mail");
            var password = appSettings.Get("password");
            _smtpClient.Credentials = new System.Net.NetworkCredential(mail, password);
            _smtpClient.Port = 587;
            _smtpClient.Host = "smtp.office365.com";
            _smtpClient.EnableSsl = true;
        }

        public async Task SendReport(string to)
        {

        }
    }
}
