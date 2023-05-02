using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.effectResults
{
    public interface IEffectPreview
    {
        public IID sourceID { get; set; }
        public IID targetID { get; set; }

        public IID spellID { get; set; }
        public IID effectModelID { get; set; }
        public IID effectInstanceID { get; set; }

        /// <summary>
        /// effects triggered before this 
        /// </summary>
        public List<IEffectPreview> triggeredBefore { get; set; }
        /// <summary>
        /// effects trigggered after this
        /// </summary>
        public List<IEffectPreview> triggeredAfter { get; set; }

        /// <summary>
        /// apply all triggered effects as well as this one
        /// </summary>
        /// <param name="fight"></param>
        public void apply(IFight fight);
        /// <summary>
        /// apply just this effect
        /// </summary>
        /// <param name="fight"></param>
        public void apply0(IFight fight);

    }
}
