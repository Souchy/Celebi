﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.creature;
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
        public IPosition position { get; init; } = new Position();

        public ObjectId stats { get; set; }
        public IEntitySet<ObjectId> spells { get; set; } = new EntitySet<ObjectId>();
        public IEntitySet<ObjectId> statuses { get; init; } = new EntitySet<ObjectId>();
        public Dictionary<ContextType, IContext> contexts { get; set; } = new();


        private Creature() { }
        private Creature(ObjectId id, ObjectId fightId)
        {
            entityUid = id;
            this.fightUid = fightId;
        }
        public static ICreature Create(ObjectId fightId) => new Creature(Eevee.RegisterIIDTemporary(), fightId);

        public IPlayer GetOriginalOwner() => this.GetFight().players.Get(originalOwnerUid);
        public IPlayer GetCurrentOwner() => this.GetFight().players.Get(currentOwnerUid);
        public IStats GetBaseStats() => this.GetFight().stats.Get(stats);
        /// <summary>
        /// If an IAction is specified, it will filter out conditional stats that are not valid
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IStats GetStats(IAction action) 
        {
            // base stats
            var statsBag = this.GetBaseStats().anonymousCopy();
            // status stats effects
            foreach(IStatusInstance status in GetStatuses().SelectMany(s => s.instances))
            {
                foreach(IEffectAddStat eff in status.GetEffects().Where(e => e is IEffectAddStat))
                {
                    // TODO: need to also check for triggers/filters (ex: 50% res vs summons, need the Action with the source/target/spell)
                    var stat = statsBag.Get(eff.statId);
                    stat.Add(eff.stat);
                }
            }
            // reset conditional stats to 0
            if(action != null)
            {
                var boardSource = this.GetFight().creatures.Get(action.caster);
                var boardTarget = this.GetFight().board.GetCreatureOnCell(action.targetCell);
                statsBag.ForEach(stat =>
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
            return statsBag;
        }
        public IEnumerable<ISpell> GetSpells() => spells.Values.Select(i => this.GetFight().spells.Get(i));
        public IEnumerable<IStatusContainer> GetStatuses() => statuses.Values.Select(i => this.GetFight().statuses.Get(i));


        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            throw new NotImplementedException();
        }
    }
}
