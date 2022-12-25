using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util.math;

namespace souchy.celebi.eevee.impl.objects
{
    /*
     * 
     * CreatureResource/Model : json file containing : passives, baseStats, baseSpells, skins..
     * 
     * CreatureInstance : instance data containing : player, position, spells, statuses, stats
     * 
     * CreatureNode : Sapphire node containing: script, model, shadow, teamRing, instanceData
     * 
     * CreatureCustomization : player customization data containing : chosenStats, chosenSpells, chosenSkin
     * 
     * 
     * 
     */


    public class CreatureInstance : ICreatureInstance
    {
        public uint entityUid { get; init; }
        public uint modelId { get; set; }
        public IPlayer player { get; set; }
        public IPosition position { get; init; } = new Position();
        public List<IStatus> statuses { get; init; } = new List<IStatus>();
        public IStats stats { get; set; }
        public List<int> spellModelIds { get; set; } = new List<int>();

        private readonly IUIdGenerator _uIdGenerator;

        public CreatureInstance(IUIdGenerator uIdGenerator)
        {
            this._uIdGenerator = uIdGenerator;
            this.entityUid = uIdGenerator.next();
        }

        public void Dispose()
        {
            this._uIdGenerator.dispose(entityUid);
        }
    }
}