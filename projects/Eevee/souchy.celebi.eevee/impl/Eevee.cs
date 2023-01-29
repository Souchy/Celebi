using Microsoft.CodeAnalysis.CSharp.Syntax;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System.Linq;

namespace souchy.celebi.eevee.impl
{
    /// <summary>
    /// 
    /// </summary>
    public static class Eevee
    {

        #region Properties
        public static IUIdGenerator uIdGenerator { get; } = new UIdGenerator();
        public static Dictionary<IID, IEventBus> eventBuses { get; } = new Dictionary<IID, IEventBus>(); // eventbus for each entity
        public static IEntityDictionary<IID, IFight> fights { get; } = new EntityDictionary<IID, IFight>();
        public static IDiamondModels models { get; } = new DiamondModels();
        #endregion

        #region Public Methods
        public static IID RegisterIID()
        {
            var id = uIdGenerator.next();
            RegisterIID(id);
            return id;
        }
        public static IEventBus RegisterIID(IID id)
        {
            uIdGenerator.take(id);
            if (!eventBuses.ContainsKey(id))
                eventBuses.Add(id, new EventBus());
            return eventBuses[id];
        }
        public static void DisposeIID(IEntity e)
        {
            if (e is IFight)
                fights.Remove(e.entityUid);
            eventBuses.Remove(e.entityUid); // disposes automatically
            uIdGenerator.dispose(e.entityUid);
        }
        #endregion
    }

    public static class EeveeExtensions
    {
        #region Extensions
        public static IFight GetFight(this IFightEntity e) => Eevee.fights.Get(e.fightUid);
        public static IEventBus GetEventBus(this IEntity e) => Eevee.eventBuses[e.entityUid];
        #endregion
    }
}
