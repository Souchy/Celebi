using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.controllers
{
    /// <summary>
    /// ----------------- PREVIOUSLY RedInstances ----------------------
    /// 
    /// so idk, we can do 2 things:
    /// - 1 Red instance per fight
    /// - 1 Red singleton for all fights
    /// 
    /// We could say all of this could go inside of IFight
    /// </summary>
    public interface IFight : IEntity 
    {
        public ITimeline timeline { get; set; }
        public IBoard board { get; set; }
        public IEntityDictionary<IID, IPlayer> players { get; init; }
        public IEntityDictionary<IID, ICreature> creatures { get; init; }
        public IEntityDictionary<IID, ISpell> spells { get; init; }
        public IEntityDictionary<IID, IStatus> statuses { get; init; }
        public IEntityDictionary<IID, ICell> cells { get; init; }
        public IEntityDictionary<IID, IStats> stats { get; init; }

        /// <summary>
        /// Status effects
        /// ///Instanced Effects for 
        ///     - variable statuses: like fourberie, colère d'iop
        /// as opposed to 
        ///     - static statuses: like passives
        ///     
        /// But maybe it could still be done with only static effects and adding stats idk
        /// 
        /// </summary>
        public IEntityDictionary<IID, IEffect> effects { get; init; }

    }
}
