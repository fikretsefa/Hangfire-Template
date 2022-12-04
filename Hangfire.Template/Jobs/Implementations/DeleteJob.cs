using Hangfire.Template.Core.Jobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Hangfire.Template.Core.Jobs.Implementations
{
    internal class DeleteJob : IDeleteJob
    {
        public void Run(string jobId)
        {
            BackgroundJob.Delete(jobId);
            //RecurringJob.RemoveIfExists("some-id");
            Debug.WriteLine($"{jobId} sahip iş parçacığı silindi");
        }
    }
}
