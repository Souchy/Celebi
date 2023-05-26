using MongoDB.Bson.Serialization.IdGenerators;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.neweffects.face;

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
            typeof(IStringEntity), 
            typeof(IEffect), 
            typeof(IStats), typeof(IStat), typeof(IStatusInstance),
             // TODO those idk yet
            typeof(ICondition), typeof(ITrigger), typeof(IZone),
            // LAST ONE for things like fight entities (creature, spell, stats, etc)
            typeof(IEntity)
        };

        private static Dictionary<ObjectId, EventBus> eventBuses = new();

        //public static IID RegisterIID<T>() 
        //{
        //    var idType = getType(typeof(T));
        //    return RegisterIID(idType);
        //}
        //private static IID RegisterIID(Type idType)
        //{
        //    var id = generators[idType].next();
        //    eventBuses[idType].Add(id, new EventBus());
        //    return id;
        //}
        //public static bool RegisterIID<T>(IID id)
        //{
        //    var idType = getType(typeof(T));
        //    var success = generators[idType].take(id);
        //    eventBuses[idType].Add(id, new EventBus());
        //    return success;
        //}
        //public static void DisposeIID<T>(IID id)
        //{
        //    var idType = getType(typeof(T));
        //    eventBuses[idType][id].Dispose();
        //    eventBuses[idType].Remove(id);
        //    generators[idType].dispose(id);
        //}
        public static bool RegisterEventBus(ObjectId id)
        {
            lock(eventBuses)
            {
                if (eventBuses.ContainsKey(id))
                    throw new ArgumentException("Id already exists");
                eventBuses.Add(id, new EventBus());
            }
            return true;
        }
        public static bool DisposeEventBus(ObjectId id)
        {
            lock(eventBuses)
            {
                if (!eventBuses.ContainsKey(id))
                    return false;
                eventBuses[id].Dispose();
                eventBuses.Remove(id);
            }
            return true;
        }
        public static IEventBus GetEntityBus(this IEntity e)
        {
            lock (eventBuses)
            {
                if (eventBuses.ContainsKey(e.entityUid))
                    return eventBuses[e.entityUid];
            }
            return null; // when NewtonsoftJson deserializes objects, it sets properties which calls the event bus before the entities' id are registered
            //throw new Exception("You made a mistake in type or method called. Maybe call iid.GetEventBus<T>()");
        }
    }

    public class ObjectIIdGenerator : IUIdGenerator
    {
        public IID next()
        {
            //ObjectIdGenerator.Instance.ge
            //BsonObjectIdGenerator.Instance.
            //StringObjectIdGenerator.Instance.
            throw new NotImplementedException();
        }
        public bool take(IID id)
        {
            throw new NotImplementedException();
        }
        public void dispose(IID id)
        {
            throw new NotImplementedException();
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
