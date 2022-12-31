using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.objects
{
    /// <summary>
    /// Context Stats for every entity (creature, cell)
    /// </summary>
    public interface IContext
    {
        //public ContextType Type { get; set; }
        public Dictionary<ResourceType, int> resourceUsed { get; set; }
        public Dictionary<ResourceType, int> resourceGained { get; set; }
        public Dictionary<ResourceType, int> resourceLost { get; set; }
        public Dictionary<ResourceType, int> resourceGiven { get; set; }
        public Dictionary<ResourceType, int> resourceTaken { get; set; }

        public Dictionary<ElementType, int> damageDone { get; set; }
        public Dictionary<ElementType, int> damageReceived { get; set; }
        public Dictionary<ElementType, int> healingDone { get; set; }
        public Dictionary<ElementType, int> healingReceived { get; set; }

        public int cellsWalked { get; set; }

    }


}
