using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LichtknarrePlugin.Utils
{
    class Delay
    {
        public static void executeAfter(int after, Action action)
        {
            if (after <= 0 || action == null) return;

            var timer = new Timer { Interval = after, Enabled = false };

            timer.Elapsed += (sender, e) =>
            {
                timer.Stop();
                action.Invoke();
                timer.Dispose();
                GC.SuppressFinalize(timer);
            };

            timer.Start();
        }
    }
}
