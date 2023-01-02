using Espeon.souchy.celebi.espeon;
using Espeon.souchy.celebi.espeon.eevee.impl.controllers;
using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util.math;

namespace Espeon.souchy.celebi.espeon.eevee.impl.objects
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
     * 
     * 
     */


    public class Creature : ICreature
    {
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }
        public IID modelUid { get; set; }
        public IID playerUid { get; set; }

        public IPosition position { get; init; } = new Position();
        public IStats stats { get; set; }
        public List<IID> spells { get; set; } = new List<IID>();
        public List<IID> statuses { get; init; } = new List<IID>();
        public Dictionary<ContextType, IContext> contextsStats { get; set; } = new Dictionary<ContextType, IContext>();


        public Creature(ScopeID scopeId, IStats stats) 
        {
            this.fightUid = scopeId;
            this.entityUid = Espeon.GetUIdGenerator(fightUid).next();
            Espeon.GetRequiredScoped<IRedInstances>(fightUid).creatures.Add(entityUid, this);
            this.stats = stats;
        }

        public void Dispose()
        {
            Espeon.DisposeIID(fightUid, entityUid);
            stats.Dispose();
            statuses.Clear();
            spells.Clear();
            contextsStats.Clear();
        }
    }
}