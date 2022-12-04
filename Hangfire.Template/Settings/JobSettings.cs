using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Template.Core.Settings
{
    public class JobSettings
    {
        public RecurringJobSettings RecurringJobSettings { get; set; }
        public OneTimeJobSettings OneTimeJobSettings { get; set; }
    }

    public class Job
    {
        public string JobId { get; set; }
        public string Queue { get; set; }
    }

    public class RepetitiveJob : Job
    {
        public string IntervalPattern { get; set; }
    }
}
