using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects.face;

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
        public FightSettings settings { get; set; }
        public ITimeline timeline { get; set; }
        public IBoard board { get; set; }
        public IEntityDictionary<ObjectId, IPlayer> players { get; init; }
        public IEntityDictionary<ObjectId, ICreature> creatures { get; init; }
        public IEntityDictionary<ObjectId, ISpell> spells { get; init; }
        public IEntityDictionary<ObjectId, IStatusContainer> statuses { get; init; }

        public IEntityDictionary<ObjectId, ICell> cells { get; init; }

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
        public IEntityDictionary<ObjectId, IEffect> effects { get; init; }
        public IEntityDictionary<ObjectId, IStats> stats { get; init; }


        public IBoardEntity GetBoardEntity(ObjectId entityId) {
            if(this.timeline.creatureIds.Contains(entityId)) { // this.board.creatureIds
                return creatures.Get(entityId);
            }
            return cells.Get(entityId);
            //if(this/*.board*/.cells.Values.Contains(entityId)) {
            //    return cells.Get(entityId);
            //}
            return null;
        }

    }
}
