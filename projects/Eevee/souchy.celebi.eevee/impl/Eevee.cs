using Microsoft.CodeAnalysis.CSharp.Syntax;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System.Linq;
using System.Security.Principal;

namespace souchy.celebi.eevee.impl
{
    /// <summary>
    /// 
    /// </summary>
    public static class Eevee
    {

        #region Properties
        //private static IUIdGenerator uIdGenerator { get; } = new UIdGenerator();
        //public static Dictionary<IID, IEventBus> eventBuses { get; } = new(); // eventbus for each entity
        public static IEntityDictionary<IID, IFight> fights { get; } = EntityDictionary<IID, IFight>.Create();
        public static IDiamondModels models { get; } = new DiamondModels();
        #endregion

        #region Public Methods
        /// <summary>
        /// BE CAREFUL THIS IS REGISTERS ON <IEntity> TYPE
        /// </summary>
        //public static IID RegisterIID() => RegisterIID<IEntity>();
        /// <summary>
        /// BE CAREFUL THIS IS REGISTERS ON <IEntity> TYPE
        /// </summary>
        //public static bool RegisterIID(IID id) => RegisterIID<IEntity>(id);
        /// <summary>
        /// BE CAREFUL THIS IS REGISTERS ON <IEntity> TYPE
        /// </summary>
        //public static void DisposeIID(IID id) => DisposeIID<IEntity>(id);

        public static IID RegisterIID<T>()
        {
            var id = UidExtensions.RegisterIID<T>(); // .getType<T>()
            return id;
        }
        public static bool RegisterIID<T>(IID id)
        {
            return UidExtensions.RegisterIID<T>(id); //.getType<T>()
        }
        public static void DisposeIID<T>(IID id)
        {
            var t = typeof(T); //var t = UidExtensions.getType<T>();
            if (t == typeof(IFight) && fights.Keys.Contains(id)) //  UidType.IFightEntity
                fights.Remove(id);
            UidExtensions.DisposeIID<T>(id); //t.DisposeIID(id);
        }
        #endregion
    }

    public static class EeveeExtensions
    {
        #region Extensions
        public static IFight GetFight(this IFightEntity e) => Eevee.fights.Get(e.fightUid);
        /// <summary>
        /// Be careful to only use IIDs made for event buses (like i18n string IIDs or entityUid)
        /// </summary>
        //public static IEventBus GetEventBus(this IID uid)
        //{
            // if there's an error here,
            // you should fix the source rather than handling it here
        //    return Eevee.eventBuses[uid]; 
        //}
        #endregion
    }



}
