using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.impl.objects
{
    /*
     * Normal Spell
     * Instant Spell
     * 
     * 
     * MTG Phases: 
     *      - Starting turn phase: 
     *              A reset resources, 
     *              A can cast instant spells
     *              B can cast instant spells to counter you
     *      - Normal turn: 
     *              A can cast normal spells
     *      - Ending turn phase: 
     *              A can cast instant spells
     *              B can cast instant spells to counter you
     * 
     * 
     * 
     */


    public class Spell : ISpell
    {
        public uint modelId { get; set; }
        public uint entityUid { get; init; }

        public ITargetFilter targetFilter { get; set; }
        public ICondition condition { get; set; }
        public List<ICost> costs { get; set; }
        public List<IEffect> effects { get; set; }
        public int maxCastsPerTurn { get; set; }
        public int maxCastsPerTarget { get; set; }
        public int cooldown { get; set; }
        public int cooldownInitial { get; set; }
        public int cooldownGlobal { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}