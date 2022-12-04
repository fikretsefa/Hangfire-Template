using Hangfire.Server;
using Hangfire.Template.Core.Jobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Template.Core.Jobs.Implementations
{
    internal class SubscriptionJob : ISubscriptionJob
    {
        public void Run(PerformContext context)
        {
            BackgroundJob.ContinueJobWith<IEmailSenderJob>(context.BackgroundJob.Id, m => m.Run("fikret.sefa@gmail.com","Abone olmak ister misiniz?"));
        }
    }
}
