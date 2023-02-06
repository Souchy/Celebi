using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.face.objects
{

    /// <summary>
    /// En fait faut juste traiter les effets préprogrammés dans les spells/status comme des instances
    /// au même titre que des nouvelles instances pour les status on runtime
    /// </summary>
    public interface IEffect : IEntityModeled, IFightEntity
    {
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }


        public IZone zone { get; set; }
        public IEntityList<ITrigger> triggers { get; set; }

        
        // Reason for child effects: 
        //      1- have more precise ScopeContext
        //      2- condition for a group of effects
        // We have EffectBase as empty effects
        // We have EffectCopyZone that gives its zone to its children
        // We have EffectGetValue that gets a value and stores it in ScopeContext for its children
        // We have EffectRebase that casts its children from a different location/caster
        // We have EffectRandom that casts a random child effect
        public IEntityList<IID> children { get; set; }             


        public ICompiledEffect compile(IFight fight, IID source, IID targetCell); // IActionContext context);


        public IEnumerable<IEffect> GetChildren() => children.Values.Select(i => Eevee.models.effects.Get(i));

    }
}