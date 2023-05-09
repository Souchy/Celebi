using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Bson;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using System.Linq;
using System.Security.Principal;

namespace souchy.celebi.eevee
{
    /// <summary>
    /// 
    /// </summary>
    public static class Eevee
    {

        #region Properties
        //private static IUIdGenerator uIdGenerator { get; } = new UIdGenerator();
        //public static Dictionary<IID, IEventBus> eventBuses { get; } = new(); // eventbus for each entity
        public static IEntityDictionary<ObjectId, IFight> fights { get; } = EntityDictionary<ObjectId, IFight>.Create();
        public static IDiamondModels models { get; } = new DiamondModels();
        #endregion

        #region Public Methods
        //public static IID RegisterIID<T>()
        //{
        //    return UidExtensions.RegisterIID<T>();
        //}
        //public static IID RegisterIIDPermanent<T>()
        //{
        //    return UidExtensions.RegisterIID<T>(); // Todo
        //}
        public static ObjectId RegisterIIDTemporary()
        {
            var id = ObjectId.GenerateNewId();
            UidExtensions.RegisterEventBus(id);
            return id; //.ToString(); //UidExtensions.RegisterIID<T>(); // Todo
        }
        public static bool RegisterEventBus(IEntity e)
        {
            return UidExtensions.RegisterEventBus(e.entityUid);
        }
        //public static bool RegisterIID<T>(IID id)
        //{
        //    return UidExtensions.RegisterIID<T>(id);
        //}
        //public static void DisposeIID<T>(IID id)
        //{
        //    var t = typeof(T);
        //    if (t == typeof(IFight) && fights.Keys.Contains(id)) 
        //        fights.Remove(id);
        //    UidExtensions.DisposeIID<T>(id);
        //}
        public static void DisposeEventBus(IEntity e)
        {
            UidExtensions.DisposeEventBus(e.entityUid);
        }

        #endregion
    }

    public static class EeveeExtensions
    {
        #region Extensions
        public static IFight GetFight(this IFightEntity e) => Eevee.fights.Get(e.fightUid);
        #endregion
    }



}
