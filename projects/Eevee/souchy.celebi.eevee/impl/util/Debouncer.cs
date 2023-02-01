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
        private const int Timeout = 3 * 1000;
        private static Dictionary<string, (Timer timer, Action action)> timers = new();

        public static void debounce(string path, Action action)
        {
            lock(timers)
            {
                if(timers.ContainsKey(path))
                {
                    var timer = timers[path].timer;
                    timer.Interval = Timeout;
                    //timer.Stop();
                    //timer.Start();
                } 
                else
                {
                    var timer = new Timer();
                    var group = (timer, action);
                    timers.Add(path, group);
                    //timers[path] = group;

                    timer.Interval = Timeout;
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

        private static void Debouncer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
