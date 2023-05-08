using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util.math;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.espeon.eevee.impl.objects
{
    /*
     *
     * CreatureResource/Model : json file containing : passives, baseStats, baseSpells, skins..
     *
     * Creature : instance data containing : player, position, spells, statuses, stats
     *
     * CreatureNode : Sapphire node containing: script, model, shadow, teamRing, instanceData
     *
     * CreatureCustomization : player customization data containing : chosenStats, chosenSpells, chosenSkin
     *
     */

    public class Creature : ICreature
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();
        public IID fightUid { get; init; }
        public IID modelUid { get; set; }
        public IID originalOwnerUid { get; set; }
        public IID currentOwnerUid { get; set; }

        public IPosition position { get; init; } = new Position();
        public IID stats { get; set; }
        public List<IID> spells { get; set; } = new List<IID>();
        public List<IID> statuses { get; init; } = new List<IID>();
        public Dictionary<ContextType, IContext> contexts { get; set; } = new Dictionary<ContextType, IContext>();

        public Creature(ScopeID scopeId) //, IStats stats)
        {
            this.fightUid = scopeId;
            //this.entityUid = Scopes.GetUIdGenerator(fightUid).next();
            //Scopes.GetRequiredScoped<IFight>(fightUid).creatures.Add(entityUid, this);
            //this.stats = stats;
        }

        public void Dispose()
        {
            Eevee.DisposeIID(this);
            Scopes.DisposeIID(fightUid, entityUid);
            //stats.Dispose();
            statuses.Clear();
            spells.Clear();
            contexts.Clear();
        }
    }
}
