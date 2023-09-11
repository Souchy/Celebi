using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects
{
    /// <summary>
    /// TODO: Wont need this anymore I believe with ContextualProperty stats. <br></br>
    /// Some concepts are interesting but we can do otherwise with just stats. <br></br>
    /// -- <br></br>
    /// Context Stats for every entity (creature, cell). <br></br>
    /// Can be during a Fight, Round, Turn or Action
    /// </summary>
    public interface IContext
    {
        public ContextType Type { get; set; }

        public Dictionary<string, object> valuesStored { get; set; }

        /*
        //public Dictionary<ResourceEnum, int> resourceUsed { get; set; }
        public Dictionary<ResourceType, int> resourceGained { get; set; }
        public Dictionary<ResourceType, int> resourceLost { get; set; }
        public Dictionary<ResourceType, int> resourceIncreased { get; set; } // Given
        public Dictionary<ResourceType, int> resourceReduced { get; set; } // Taken

        public Dictionary<ElementType, int> damageDone { get; set; }
        public Dictionary<ElementType, int> damageReceived { get; set; }
        public Dictionary<ElementType, int> healingDone { get; set; }
        public Dictionary<ElementType, int> healingReceived { get; set; }
        */

        public List<SpellCastHistory> spellsCast { get; set; }
        public List<CellMovedHistory> cellsMoved { get; set; }

        // ? effect history instead of dictionaries of damageDone, etc, just <EffectType, List<CompiledEffect>> ?
        /// <summary>
        /// List of effects related to this creature. <br></br>
        /// Can be the source of the target, it's in the CompiledEffect
        /// </summary>
        public Dictionary<Type, List<IEffectPreview>> compiledEffects { get; set; }
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

}
