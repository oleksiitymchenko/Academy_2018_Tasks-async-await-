using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading.Tasks;


namespace homework_5_bsa2018.BLL
{
    public class Helpers
    {
        private Timer _timer;

        public Helpers()
        {
            _timer = new Timer(interval: 300);
        }

        public async Task<string> FakeDelay()
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            _timer.Elapsed += (obj, args) =>
              { 
                  tcs.SetResult("Result");
                  _timer.Enabled = false;
              };

            _timer.Enabled = true;

            return await tcs.Task;
        }
    }
}
