using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnDotNet.Services
{
    public interface IMailSender
    {
        string SendMail(string mail, string title);
    }
}
