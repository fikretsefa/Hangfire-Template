using Hangfire.Template.Core.Jobs.Interfaces;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Hangfire.Template.Core.Jobs.Implementations
{
    internal class ServerCheckerJob : IServerCheckerJob
    {
        public void Run(string domain)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(domain);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
               
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }


            if (pingable)
                Debug.WriteLine("Servera erişim sağlanıyor");
            else
                Debug.WriteLine("Servera erişim sağlanmıyor");
        }
    }
}
