using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Template.Core.Jobs.Interfaces
{
    internal interface ISubscriptionJob
    {
        //AutomaticRetry: Tekrar deneme durum ayarlarını belirtir
        //Attempts: Hata sonrası tekrar deneme sayısını belirtir
        //LogEvents: Loglama durumunu belirtir
        //OnAttemptsExceeded: Hatalı denemeler için yapılacak işlemi belirtir
        //JobDisplayName: İş parçacığının adını belirtir
        //Queue: Yer alacağı kuyruğu belirtir
        [AutomaticRetry(Attempts = 0, LogEvents = false, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        [JobDisplayName("Abonelik Gönderim İş Parçacığı")]
        [Queue("general")]
        void Run(PerformContext context);
    }
}
