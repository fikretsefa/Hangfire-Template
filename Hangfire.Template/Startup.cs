using Hangfire.SqlServer;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Template.Core.Settings;
using Hangfire.Template.Core.Services.Interfaces;
using Hangfire.Template.Core.Services.Implementations;
using Hangfire.Template.Core.Jobs.Interfaces;
using Hangfire.Template.Core.Jobs.Implementations;
using System.Diagnostics;

namespace Hangfire.Template
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Hangfire
            // Add job settings
            services.Configure<JobSettings>(Configuration.GetSection("JobSettings"));

            //Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("Hangfire.Connection"), new SqlServerStorageOptions
                {
                    SchemaName = "hangfire",
                    PrepareSchemaIfNecessary = true,
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            services.AddHangfireServer();

            // Add the processing server as IHostedService
            var serverList = Configuration.GetSection("HangfireSettings:ServerList").Get<List<HangfireServer>>();
            foreach (var server in serverList)
            {
                services.AddHangfireServer(options =>
                {
                    options.ServerName = server.Name;
                    options.WorkerCount = server.WorkerCount;
                    options.Queues = server.QueueList;
                    options.SchedulePollingInterval = TimeSpan.FromSeconds(10);
                });
            }

            // Add Jobs
            services.AddSingleton<IJobStarter, JobStarter>();
            services.AddScoped<IHangfireStartedNotificationJob, HangfireStartedNotificationJob>();
            services.AddScoped<IEmailSenderJob, EmailSenderJob>();
            services.AddScoped<ICancelJob, CancelJob>();
            services.AddScoped<IDeleteJob, DeleteJob>();
            services.AddScoped<IServerCheckerJob, ServerCheckerJob>();
            services.AddScoped<ISubscriptionJob, SubscriptionJob>();


            #endregion

            // Add framework services.
            services.AddMvc();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });


            // Add hangfire dashboard
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                AppPath = "https://www.google.com",
                DashboardTitle = "Adventure Works Scheduled Jobs",
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = Configuration.GetSection("HangfireSettings:UserName").Value,
                        Pass = Configuration.GetSection("HangfireSettings:Password").Value
                    }
                }
            });
            backgroundJobs.Enqueue(() => Debug.WriteLine("Hangfire configurations are set"));

            // Start custom Hangfire Jobs
            var startJob = app.ApplicationServices.GetService<IJobStarter>();
            startJob.StartJobs();
        }
    }

}
