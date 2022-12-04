using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Template.Core.Jobs.Interfaces
{
    internal interface IHangfireStartedNotificationJob
    {
        //AutomaticRetry: Tekrar deneme durum ayarlarını belirtir
        //Attempts: Hata sonrası tekrar deneme sayısını belirtir
        //LogEvents: Loglama durumunu belirtir
        //OnAttemptsExceeded: Hatalı denemeler için yapılacak işlemi belirtir
        //JobDisplayName: İş parçacığının adını belirtir
        [AutomaticRetry(Attempts = 0, LogEvents = false, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        [JobDisplayName("Hangfire Başlangıç Bildirim İş Parçacığı")]
        void Run();
    }
}
