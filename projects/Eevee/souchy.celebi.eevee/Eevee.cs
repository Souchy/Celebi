using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Bson;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects.face;
using System.Collections.Immutable;
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
        public static IEntityDictionary<ObjectId, IFight> fights { get; } = EntityDictionary<ObjectId, IFight>.Create();
        public static IDiamondModels models { get; } = new DiamondModels();
        public static ImmutableDictionary<Type, IEffectScript> effectScripts { get; } = typeof(Eevee).Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEffectScript)) && !t.IsAbstract && t.IsClass)
                .Select(t => (IEffectScript) Activator.CreateInstance(t))
                .ToImmutableDictionary(s => s.SchemaType, s => s);
        #endregion

        #region Public Methods
        public static ObjectId RegisterIIDTemporary()
        {
            var id = ObjectId.GenerateNewId();
            UidExtensions.RegisterEventBus(id);
            return id;
        }
        public static bool RegisterEventBus(IEntity e)
        {
            return UidExtensions.RegisterEventBus(e.entityUid);
        }
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
