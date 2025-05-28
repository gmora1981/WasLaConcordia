using System;
using System.Timers;
    

namespace LaConcordia.Auth
{
    public class TokenRenewer : IDisposable
    {
        public TokenRenewer(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        System.Timers.Timer timer;
        private readonly ILoginService loginService;

        public void Initiate()
        {
            
            timer = new System.Timers.Timer();
            timer.Interval = 1800000; //240000 son 4 minutes    1800000 son 30 min
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("timer elapsed");
            loginService.TryRenewToken();
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
