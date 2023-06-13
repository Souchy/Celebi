using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.neweffects.impl.effects.creature;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.impl.objects
{
    public class Creature : ICreature
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }
        public ObjectId fightUid { get; set; }

        public ObjectId originalOwnerUid { get; set; }
        public ObjectId currentOwnerUid { get; set; }
        public ObjectId? summoner { get; set; }
        public IPosition position { get; init; } = new Position();

        public ObjectId statsId { get; set; }
        public IEntitySet<ObjectId> spellIds { get; set; } = new EntitySet<ObjectId>();
        public IEntitySet<ObjectId> statuses { get; init; } = new EntitySet<ObjectId>();
        public Dictionary<ContextType, IContext> contexts { get; set; } = new();

        private Creature() { }
        private Creature(ObjectId id, ObjectId fightId)
        {
            entityUid = id;
            this.fightUid = fightId;
        }
        public static ICreature Create(ObjectId fightId) => new Creature(Eevee.RegisterIIDTemporary(), fightId);

        public bool isSummon() => this.summoner != null;
        public ICreature? GetSummoner() => isSummon() ? null : this.GetFight().creatures.Get(this.summoner.Value);
        public IPlayer GetOriginalOwner() => this.GetFight().players.Get(originalOwnerUid);
        public IPlayer GetCurrentOwner() => this.GetFight().players.Get(currentOwnerUid);
        public IStats GetNaturalStats() => this.GetFight().stats.Get(statsId);
        /// <summary>
        /// If an IAction is specified, it will filter out conditional stats that are not valid
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IStats GetTotalStats(IAction action) 
        {
            // copy base stats
            var naturalStats = this.GetNaturalStats().copy(true);
            // add status stats
            foreach(IStatusInstance status in GetStatuses().SelectMany(s => s.instances))
            {
                foreach(IEffect eff in status.GetEffects().Where(e => e.Schema is AddStats))
                {
                    // TODO: need to also check for triggers/filters (ex: 50% res vs summons, need the Action with the source/target/spell)
                    var props = eff.GetProperties<AddStats>();
                    foreach(var st in props.stats.Values)
                    {
                        naturalStats.Add(st);
                    }
                    //var naturalStat = naturalStats.Get(props.stat.statId);
                    //naturalStat.Add(props.stat);
                }
            }
            // reset conditional stats to 0
            if(action != null)
            {
                var boardSource = this.GetFight().creatures.Get(action.caster);
                var boardTarget = this.GetFight().board.GetCreatureOnCell(action.targetCell);
                naturalStats.ForEach(stat =>
                {
                    if (stat is IStatSimple simple)
                    {
                        var success = simple.statId.GetCharactType().conditions.All(c =>
                        {
                            return c.check(action, null, boardSource, boardTarget);
                        });
                        if (!success)
                            simple.value = 0;
                    }
                });
            }
            return naturalStats;
        }
        public IEnumerable<ISpell> GetSpells() => spellIds.Values.Select(i => this.GetFight().spells.Get(i));
        public IEnumerable<IStatusContainer> GetStatuses() => statuses.Values.Select(i => this.GetFight().statuses.Get(i));


        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            throw new NotImplementedException();
        }
    }
}
