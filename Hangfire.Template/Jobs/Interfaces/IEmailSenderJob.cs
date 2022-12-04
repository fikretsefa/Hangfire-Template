using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Template.Core.Jobs.Interfaces
{
    internal interface IServerCheckerJob
    {
        //JobDisplayName: İş parçacığının adını belirtir
        [JobDisplayName("Sunucu Kontrol İş Parçacığı")]
        void Run(string domain);
    }
}
