using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.util
{

    /// <summary>
    /// Dont use outside of Eevee
    /// </summary>
    public static class UidExtensions
    {
        private static Type[] modelTypes = new Type[] { 
            // models
            typeof(ICreatureModel), typeof(ISpellModel), typeof(IEffectModel), typeof(IStatusModel), typeof(IMap), 
            // skins
            typeof(ICreatureSkin), typeof(ISpellSkin), typeof(IEffectSkin),
            // other
            typeof(string),
            typeof(IEffect), // need a separate idgenerator to json save instances in spellmodels & statusmodels
             // TODO those idk yet
            typeof(ICondition), typeof(ITrigger), typeof(IZone),
            // LAST ONE for things like fight entities (creature, spell, stats, etc)
            typeof(IEntity)
        };
        private static Dictionary<Type, IUIdGenerator> generators = new(); 
        private static Dictionary<Type, Dictionary<IID, EventBus>> eventBuses = new(); 
        private static Type getType(Type objType)
        {
            foreach (var modelType in modelTypes)
                if (modelType.IsAssignableFrom(objType))
                    return modelType;
            throw new Exception("Unknown id type");
        }
        static UidExtensions()
        {
            foreach (var modelType in modelTypes)
            {
                generators[modelType] = new UIdGenerator();
                eventBuses[modelType] = new();
            }
        }
        public static IID RegisterIID<T>() 
        {
            var idType = getType(typeof(T));
            var id = generators[idType].next();
            eventBuses[idType].Add(id, new EventBus());
            return id;
        }
        public static bool RegisterIID<T>(IID id)
        {
            var idType = getType(typeof(T));
            var success = generators[idType].take(id);
            eventBuses[idType].Add(id, new EventBus());
            return success;
        }
        public static void DisposeIID<T>(IID id)
        {
            var idType = getType(typeof(T));
            id.GetEventBus<T>().Dispose();
            eventBuses[idType].Remove(id);
            generators[idType].dispose(id);
        }
        /// <summary>
        /// mostly used for i18n strings since they're not entities, they dont have a type to latch to
        /// </summary>
        public static EventBus GetEventBus<T>(this IID id)
        {
            var idType = getType(typeof(T));
            return eventBuses[idType][id];
        }
        public static EventBus GetEntityBus(this IEntity e)
        {
            var t = getType(e.GetType());
            if (eventBuses[t].ContainsKey(e.entityUid))
                return eventBuses[t][e.entityUid];
            //foreach(var modelType in modelTypes)
            //    if (modelType.IsAssignableFrom(e.GetType()) && eventBuses[modelType].ContainsKey(e.entityUid))
            //        return eventBuses[modelType][e.entityUid];

            //return eventBuses[typeof(IEntity)][e.entityUid];
            //if (eventBuses[typeof(IEntity)].ContainsKey(e.entityUid))
            //    return eventBuses[typeof(IEntity)][e.entityUid];
            throw new Exception("You made a mistake in type or method called. Maybe call iid.GetEventBus<T>()");
        }
    }


    public class UIdGenerator : IUIdGenerator
    {
        private int counter = 0;
        private HashSet<int> ids = new HashSet<int>();

        public IID next()
        {
            lock(this)
            {
                if(int.MaxValue == ids.Count) // uint.max
                {
                    throw new Exception("Too many IDs");
                }
                while (ids.Contains(counter))
                {
                    counter++;
                    if (counter == int.MaxValue) counter = 0;
                }
                ids.Add(counter);
                return (IID) counter;
            }
        }

        public bool take(IID id)
        {
            lock(this)
            {
                if (ids.Add(id))
                {
                    counter = id + 1;
                    return true;
                }
                else
                    return false;
            }
        }

        public void dispose(IID i)
        {
            lock(this)
            {
                ids.Remove(i);
            }
        }

    }
}
