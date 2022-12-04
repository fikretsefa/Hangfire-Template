using Hangfire.Template.Core.Jobs.Interfaces;
using System;
using System.Diagnostics;

namespace Hangfire.Template.Core.Jobs.Implementations
{
    internal class EmailSenderJob : IEmailSenderJob
    {
        public void Run(string email, string msg)
        {
            Debug.WriteLine($"[{email}] adresine sahip kullanıcıya [{msg}] mesajı gönderildi.");
        }
    }
}
