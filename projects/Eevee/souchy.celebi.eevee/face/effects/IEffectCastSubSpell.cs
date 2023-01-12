﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects
{
    public interface IEffectCastSubSpell : IEffect
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
    }
}