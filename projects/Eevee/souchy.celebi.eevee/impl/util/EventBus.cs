﻿using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using System.Reflection;
using System.Xml.Linq;

namespace souchy.celebi.eevee.impl.util
{
    /*
     * Use cases:
     * 
     * 1:
     *      sub to a particular Class instance and its property, ex: [creature.stats] .life  
     *      ->  [Subscribe(nameof(StatType.Life))
     *          healthbar.onLifeChanged(Stats stats)
     *          
     *      -> so to sub to only 1 particular Stat instance, you need to sub to the creature's/stat's EventBus
     *      
     *      -> can put all event buses in IFight: Dictionary<IID, EventBus> buses;
     * 
     * 2: 
     *      sub to any Class instance and their specific property, ex: [stats] .life
     *      
     * 3:
     *      sub to any object, ex: creature
     *      
     * 
     */


    /*
     * Solution:
     * 
     * - Have 1 EventBus per Entity 
     * - Store buses in Dictionary<IID, EventBus> buses; in IFight
     * - Can sub to specific entities or the whole fight entity
     * - 
     * 
     */




    /// <summary>
    /// Subscribe attribute
    /// Can be used on methods. 
    /// 
    /// The attribute can target a string path (ex: nameof(StatType.Life), "my:scope:path", nameof(CreatureModel.nameId))
    /// The path is only used to pipeline events, it can be anything, doesn't mean anything.
    /// The method can have parameters to serve as event objects. 
    /// The parameters must match the same as the parametrs in publish()
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SubscribeAttribute : Attribute {
        public string path;
        public SubscribeAttribute() { }
        public SubscribeAttribute(string path)
        {
            this.path = path;
        }
    }

    public class Subscription
    {
        public object subscriber;
        public MethodInfo method;

        public string path;
        public Type? eventParameterType;
        public List<Type> eventParameterTypes = new List<Type>();
    }

    public class EventBus : IEventBus
    {
        private List<Subscription> subs { get; set; } = new List<Subscription>();

        public void subscribe(object subscriber, params string[] methodNames)
        {
            lock (subs)
            {
                var type = subscriber.GetType();
                var at = typeof(SubscribeAttribute);
                var methods = subscriber.GetType()
                    .GetMethods()
                    .Where(m => methodNames.Length == 0 || methodNames.Contains(m.Name))
                    .Where(f => f.GetCustomAttributes(at, true).Any());
                foreach (var m in methods)
                {
                    var @params = m.GetParameters();
                    var attr = (SubscribeAttribute) m.GetCustomAttribute(at, true);

                    var sub = new Subscription();
                    sub.subscriber = subscriber;
                    sub.method = m;
                    sub.path = attr.path;
                    sub.eventParameterTypes = @params.Select(p => p.ParameterType).ToList();

                    subs.Add(sub);
                }
            }
        }

        public void unsubscribe(object subscriber, params string[] methodNames)
        {
            lock(subs)
            {
                subs.RemoveAll(s => s.subscriber.Equals(subscriber) && (methodNames.Length == 0 || methodNames.Contains(s.method.Name)));
            }
        }

        public void publish(string path = "", params object[] param)
        {
            lock (subs)
            {
                foreach (var sub in subs)
                {
                    if (sub.path == path && sub.eventParameterTypes.Count == param.Length)
                    {
                        var match = true;
                        for(int i = 0; i < param.Length; i++)
                        {
                            match &= sub.eventParameterTypes[i].IsAssignableFrom(param[i].GetType());
                        }
                        if(match)
                            sub.method.Invoke(sub.subscriber, param);
                    }
                }
            }
        }

        public void Dispose()
        {
            lock(subs)
            {
                subs.Clear();
            }
        }
    }

}