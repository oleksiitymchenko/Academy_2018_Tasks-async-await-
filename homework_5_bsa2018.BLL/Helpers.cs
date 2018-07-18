using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;

namespace homework_5_bsa2018.BLL
{
    public class Helpers
    {
        private Timer timer;
        private IService<FlightDTO> _service;

        public Helpers(IService<FlightDTO> service, int interval) 
        {
            timer = new Timer(interval: interval);
            timer.AutoReset = false;
            _service = service;
        }

        public async Task<IEnumerable<FlightDTO>> GetFlightsDelay()
        {
            var tcs = new TaskCompletionSource<IEnumerable<FlightDTO>>();

            timer.Enabled = true;

            ElapsedEventHandler callback = 
                async (obj, args) =>
            {
            tcs.SetResult(await _service.GetAllAsync());
                timer.Enabled = false;
            };

            timer.Elapsed += callback;
            
            return await tcs.Task;
        }
    }
}
