using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;

namespace souchy.celebi.eevee.impl.util
{
    public class Debouncer
    {
        private static Dictionary<string, (Timer timer, Action action)> timers = new();

        /// <summary>
        /// Debounce with a default timer of 3 seconds
        /// </summary>
        public static void debounce(string path, Action action) 
            => debounce(path, 3 * 1000, action);

        /// <summary>
        /// Debounce
        /// </summary>
        /// <param name="path">String to identify this action (subsequents calls to the same path will reset the timer on this path)</param>
        /// <param name="timeoutMs">Timeout in milliseconds</param>
        /// <param name="action">Action to execute on timeout</param>
        public static void debounce(string path, int timeoutMs, Action action)
        {
            lock(timers)
            {
                if(timers.ContainsKey(path))
                {
                    var timer = timers[path].timer;
                    timer.Interval = timeoutMs;
                    //timer.Stop();
                    //timer.Start();
                } 
                else
                {
                    var timer = new Timer();
                    var group = (timer, action);
                    timers.Add(path, group);
                    //timers[path] = group;

                    timer.Interval = timeoutMs;
                    timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                    {
                        lock(timers) { 
                            timers.Remove(path);
                            timer.Stop();
                            group.action();
                        }
                    };
                    timer.Start();
                }
            }
        }


    }
}
