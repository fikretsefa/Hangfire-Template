using Hangfire.Template.Core.Jobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hangfire.Template.Core.Jobs.Implementations
{
    internal class CancelJob : ICancelJob
    {
        public void Run(IJobCancellationToken cancellationToken)
        {
            Thread.Sleep(TimeSpan.FromMinutes(2));
        }
    }
}
