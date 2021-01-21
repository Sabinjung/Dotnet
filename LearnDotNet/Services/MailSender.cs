using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace LearnDotNet.Services
{
    public class MailSender : IMailSender
    {
        public string SendMail(string mail, string title)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("sabin.j.kzetri@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(mail);
            message.To.Add(to);

            message.Subject = "A note has been created";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = title;
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("sabin.j.kzetri@gmail.com", "efmuxixcxgpnbngf");

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

            return "Done";
        }
    }
}
