using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl
{
    /// <summary>
    /// 
    /// </summary>
    public static class Eevee
    {
        //private static readonly Eevee singleton = new Eevee();
        //private Eevee() { }
        #region Backing fields
        //private readonly IUIdGenerator _uIdGenerator = new UIdGenerator();
        //private readonly Dictionary<IID, IEventBus> _eventBuses = new Dictionary<IID, IEventBus>(); // eventbus for each entity
        //private readonly Dictionary<IID, IFight> _fights = new Dictionary<IID, IFight>();
        //private readonly IDiamondModels _models = new DiamondModels();
        #endregion

        #region Properties
        //public static IUIdGenerator uIdGenerator { get => singleton._uIdGenerator; }
        //public static Dictionary<IID, IEventBus> eventBuses { get => singleton._eventBuses; }
        //public static Dictionary<IID, IFight> fights { get => singleton._fights; }
        //public static IDiamondModels models { get => singleton._models; }
        public static IUIdGenerator uIdGenerator { get; } = new UIdGenerator();
        public static Dictionary<IID, IEventBus> eventBuses { get; } = new Dictionary<IID, IEventBus>(); // eventbus for each entity
        public static Dictionary<IID, IFight> fights { get; } = new Dictionary<IID, IFight>();
        public static IDiamondModels models { get; } = new DiamondModels();
        #endregion

        #region Public Methods
        public static IID RegisterIID()
        {
            var id = uIdGenerator.next();
            eventBuses.Add(id, new EventBus());
            return id;
        }
        public static void DisposeIID(IEntity e)
        {
            eventBuses.Remove(e.entityUid);
            if (e is IFight)
                fights.Remove(e.entityUid);
            uIdGenerator.dispose(e.entityUid);
        }
        #endregion
    }

    public static class EeveeExtensions
    {
        #region Extensions
        public static IFight GetFight(this IFightEntity e) => Eevee.fights[e.fightUid];
        public static IEventBus GetEventBus(this IEntity e) => Eevee.eventBuses[e.entityUid];
        #endregion
    }
}
