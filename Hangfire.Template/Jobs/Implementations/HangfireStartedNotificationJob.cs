using Hangfire.Template.Core.Jobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Hangfire.Template.Core.Jobs.Implementations
{
    internal class HangfireStartedNotificationJob : IHangfireStartedNotificationJob
    {
        public void Run()
        {
            Debug.WriteLine("Hangfire started");
        }
    }
}
