using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.effects.spell;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.special
{
    /// <summary>
    /// Cast a sub spell and you can change the caster and its origin
    /// </summary>
    public class EffectCastSubSpell : Effect, IEffectCastSubSpell
    {
        public IValue<IID> spellModelId { get; set; }
        /// <summary>
        /// Who will cast the spell
        /// </summary>
        public ActorType newCaster { get; set; }
        /// <summary>
        /// From where will the spell be cast (ex: frappe xel vs teleportation xel serait possible, mais mieux de faire 2 effets différents pour ça)
        /// </summary>
        public ActorType newCasterOrigin { get; set; }


        public EffectCastSubSpell() { }
        public EffectCastSubSpell(IID id) : base(id) { }
        public static IEffectCastSubSpell Create() => new EffectCastSubSpell(Eevee.RegisterIID());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
