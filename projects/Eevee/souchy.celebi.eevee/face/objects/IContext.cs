using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.effects.compiledeffects;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects
{
    /// <summary>
    /// Context Stats for every entity (creature, cell). 
    /// Can be during a Fight, Round, Turn or Action
    /// </summary>
    public interface IContext
    {
        public ContextType Type { get; set; }

        public Dictionary<string, object> valuesStored { get; set; }

        public Dictionary<ResourceType, int> resourceUsed { get; set; }
        public Dictionary<ResourceType, int> resourceGained { get; set; }
        public Dictionary<ResourceType, int> resourceLost { get; set; }
        public Dictionary<ResourceType, int> resourceIncreased { get; set; } // Given
        public Dictionary<ResourceType, int> resourceReduced { get; set; } // Taken

        public Dictionary<ElementType, int> damageDone { get; set; }
        public Dictionary<ElementType, int> damageReceived { get; set; }
        public Dictionary<ElementType, int> healingDone { get; set; }
        public Dictionary<ElementType, int> healingReceived { get; set; }

        public List<SpellCastHistory> spellsCast { get; set; }
        public List<CellMovedHistory> cellsMoved { get; set; }
        // ?
        //public List<EffectHistory> effectModelsCast { get; set; }
        //public List<EffectHistory> effectInstancesCast { get; set; } 
    }

    public class SpellCastHistory
    {
        public IID spellID { get; set; }
        public IID cellID { get; set; }
        // ?
        //public List<ICompiledEffect> compiledEffects { get; set; } 
    }
    public class CellMovedHistory
    {
        public IID cellID { get; set; }
        public MoveType moveType { get; set; }
    }

    // ?
    /*
    public class EffectHistory
    {
        public IID spellID { get; set; }
        public IID effectID { get; set; }
        /// <summary>
        /// number of times it is present in spell casts
        /// </summary>
        public int numberOfCasts { get; set; }
        /// <summary>
        /// total = number of cast * number of targets every cast
        /// (can be the same targets multiple times)
        /// </summary>
        public int numberOfApplications { get; set; }
        public int numberOfDifferentTargets { get; set; }
        /// <summary>
        /// ex: how many ap reduced total...
        /// </summary>
        public int valueOutputed { get; set; }
    }
    */

}
