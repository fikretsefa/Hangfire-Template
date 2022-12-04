using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Template.Core.Settings
{
    //Tek seferlik gönderim sağlanacak iş parçacıkları içerir.
    public class OneTimeJobSettings
    {
        public Job HangfireStartedNotificationJob { get; set; }
    }
}
