
using Hangfire.Template.Core.Jobs.Interfaces;
using Hangfire.Template.Core.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Hangfire.Template.Core.Services.Implementations
{
    public class JobStarter : IJobStarter
    {
        //private readonly RecurringJobSettings _recurringJobSettings;
        //private readonly OneTimeJobSettings _onetimeJobSettings;

        //public JobStarter(IOptions<JobSettings> jobSettingsOptions)
        //{
        //    _recurringJobSettings = jobSettingsOptions.Value.RecurringJobSettings;
        //    _onetimeJobSettings = jobSettingsOptions.Value.OneTimeJobSettings;
        //}

        //Tekrarlı iş parçacıklarının ayarlarını içerir
        //private RepetitiveJob ServerCheckerJob => _recurringJobSettings.ServerCheckerJob;

        //Tek seferlik gönderim sağlanacak iş parçacıklarının ayarlarını içerir.
        //private Job HangfireStartedNotificationJob => _onetimeJobSettings.HangfireStartedNotificationJob;
        public void StartJobs()
        {
            //Fire-and-forget
            //Çalıştırıldıktan sonra yalnızca bir kez ve hemen yürütülür.
            BackgroundJob.Enqueue<IHangfireStartedNotificationJob>(m => m.Run());
            BackgroundJob.Enqueue(() => Debug.WriteLine("Fire and forget lamda usage"));

            //Delayed
            //Sizin belirleyeceğiniz süreye bağlı olarak yalnızca belirtilen zaman aralığından sonra yürütülür.
            BackgroundJob.Schedule<IEmailSenderJob>(m => m.Run("fikret.sefa@gmail.com", "Aramıza hoşgeldin!"), TimeSpan.FromSeconds(10));
            BackgroundJob.Schedule<ICancelJob>(m => m.Run(JobCancellationToken.Null), TimeSpan.FromMinutes(2));
            //Job Id ile iş parçacığı silen başka bir iş parcacığı 🙄
            var jobId = BackgroundJob.Schedule<IEmailSenderJob>(m => m.Run("fikret.sefa@gmail.com", "Bu iş parçacığı çalışmadan silinecek!"), TimeSpan.FromSeconds(10));
            BackgroundJob.Schedule<IDeleteJob>(m => m.Run(jobId), TimeSpan.FromSeconds(5));

            //Recurring
            //Belirli aralıklara bağlı olarak birçok kez yürütülür. 
            //Bir iş parçacığına bağlı yürütülebilir.
            //https://crontab.guru/ ile cron hesaplayarak manuel giriş sağlayabilirsiniz.

            //RecurringJob.AddOrUpdate<IServerCheckerJob>(ServerCheckerJob.JobId, m => m.Run(IPGlobalProperties.GetIPGlobalProperties().DomainName.ToString()), ServerCheckerJob.IntervalPattern, TimeZoneInfo.Local, ServerCheckerJob.Queue);
            //veya 
            RecurringJob.AddOrUpdate<IServerCheckerJob>("ServerCheckerJob", m => m.Run("www.google.com"), "* * * * *", TimeZoneInfo.Local, "integration");
            RecurringJob.AddOrUpdate("MontlyRecurringJob", () => Debug.WriteLine("Ben her ay çalşırım"), Cron.Monthly);
            RecurringJob.AddOrUpdate<ISubscriptionJob>("SubscriptionJob", m => m.Run(null), "* * * * *", TimeZoneInfo.Local, "integration");
        }

    }
}
