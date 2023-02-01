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
            typeof(IStringEntity), //typeof(string),
            typeof(IEffect), // need a separate idgenerator to json save instances in spellmodels & statusmodels
             // TODO those idk yet
            typeof(ICondition), typeof(ITrigger), typeof(IZone),
            // LAST ONE for things like fight entities (creature, spell, stats, etc)
            typeof(IEntity)
        };
        private static Dictionary<Type, IUIdGenerator> generators = new(); 
        private static Dictionary<Type, Dictionary<IID, EventBus>> eventBuses = new(); 
        static UidExtensions()
        {
            foreach (var modelType in modelTypes)
            {
                generators[modelType] = new UIdGenerator();
                eventBuses[modelType] = new();
            }
        }
        private static Type getType(Type objType)
        {
            foreach (var modelType in modelTypes)
                if (modelType.IsAssignableFrom(objType))
                    return modelType;
            throw new Exception("Unknown id type");
        }
        public static IID RegisterIID<T>() 
        {
            var idType = getType(typeof(T));
            return RegisterIID(idType);
        }
        private static IID RegisterIID(Type idType)
        {
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
            eventBuses[idType][id].Dispose();
            eventBuses[idType].Remove(id);
            generators[idType].dispose(id);
        }
        public static IEventBus GetEntityBus(this IEntity e)
        {
            var t = getType(e.GetType());
            if (eventBuses[t].ContainsKey(e.entityUid))
                return eventBuses[t][e.entityUid];
            return null; // when NewtonsoftJson deserializes objects, it sets properties which calls the event bus before the entities' id are registered
            //throw new Exception("You made a mistake in type or method called. Maybe call iid.GetEventBus<T>()");
        }
    }


    public class UIdGenerator : IUIdGenerator
    {
        private int counter = 1;
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
