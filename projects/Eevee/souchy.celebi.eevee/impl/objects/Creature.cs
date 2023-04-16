﻿using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;

namespace souchy.celebi.eevee.impl.objects
{
    public class Creature : ICreature
    {
        public IID entityUid { get; set; }
        public IID modelUid { get; set; }
        public IID fightUid { get; set; }

        public IID originalOwnerUid { get; set; }
        public IID currentOwnerUid { get; set; }
        public IPosition position { get; init; } = new Position();

        public IID stats { get; set; }
        public IEntitySet<IID> spells { get; set; } = new EntitySet<IID>();
        public IEntitySet<IID> statuses { get; init; } = new EntitySet<IID>();
        public Dictionary<ContextType, IContext> contexts { get; set; } = new();


        private Creature() { }
        private Creature(IID id) => entityUid = id;
        public static ICreature Create() => new Creature(Eevee.RegisterIID<ICreature>());

        public IPlayer GetOriginalOwner() => this.GetFight().players.Get(originalOwnerUid);
        public IPlayer GetCurrentOwner() => this.GetFight().players.Get(currentOwnerUid);
        public IStats GetBaseStats() => this.GetFight().stats.Get(stats);
        public IStats GetStats(IAction action, TriggerEvent trigger)
        {
            var statsBag = this.GetBaseStats().anonymousCopy();
            foreach(var status in GetStatuses())
            {
                foreach(IEffectAddStat eff in status.GetEffects().Where(e => e is IEffectAddStat))
                {
                    // TODO: need to also check for triggers/filters (ex: 50% res vs summons, need the Action with the source/target/spell)
                    var stat = statsBag.Get(eff.statId);
                    stat.Add(eff.stat);
                }
            }
            return statsBag;
        }
        public IEnumerable<ISpell> GetSpells() => spells.Values.Select(i => this.GetFight().spells.Get(i));
        public IEnumerable<IStatusInstance> GetStatuses() => statuses.Values.Select(i => this.GetFight().statuses.Get(i));


        public void Dispose()
        {
            Eevee.DisposeIID<ICreature>(entityUid);
            throw new NotImplementedException();
        }
    }
}
