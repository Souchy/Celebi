using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.face.objects
{
    /// <summary>
    /// En fait faut juste traiter les effets préprogrammés dans les spells/status comme des instances
    /// au même titre que des nouvelles instances pour les status on runtime
    /// </summary>
    public interface IEffect : IEntityModeled, IFightEntity, IEffectsContainer
    {

        //#region Dynamic Status creation
        /// <summary>
        /// Properties to create a status instance from an effect.
        /// When this is not null, create a status instead of applying the effect.
        /// </summary>
        //public StatusProperties statusProperties { get; set; }
        //#endregion

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }


        public IZone zone { get; set; }
        public IEntityList<ITrigger> triggers { get; set; }


        // <<Children property is now in IEffectsContainer>>
        // Reason for child effects: 
        //      1- have more precise ScopeContext
        //      2- condition for a group of effects
        // We have EffectBase as empty effects
        // We have EffectCopyZone that gives its zone to its children
        // We have EffectGetValue that gets a value and stores it in ScopeContext for its children
        // We have EffectRebase that casts its children from a different location/caster
        // We have EffectRandom that casts a random child effect


        public IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets);
        public IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets); // IActionContext context);

        //public IEnumerable<IEffect> GetChildren() => effectIds.Values.Select(i => Eevee.models.effects.Get(i));

        /// <summary>
        /// Get unfiltered entities in this effect's area
        /// </summary>
        public IEnumerable<IBoardEntity> GetPossibleBoardTargets(IFight fight, IPosition targetCell);

        /// <summary>
        /// Copy basic properties to passed effect. (not model nor model-specific properties)
        /// </summary>
        public void CopyBasicTo(IEffect e);

        /// <summary>
        /// StatusProperties to create a Status instance with this effect
        /// Why:
        ///     To create better statuses (different duration/delay per effect, easy to show in spell description, easy to create...)
        /// How it works:
        ///     Create a StatusInstance with all the spell effects that have StatusProperties
        ///     Each effect inside the status can have its own duration/delay/fusing :D
        /// Identification:
        ///     By SpellID / SpellModelID only instead of StatusModelID
        ///     Statuses made with StatusModel can use the model id
        /// </summary>
        //public sealed class StatusProperties
        //{
        //    /// <summary>
        //    /// Duration is also the Max Duration
        //    /// </summary>
        //    public IValue<int> Duration { get; set; }
        //    public IValue<int> Delay { get; set; }
        //    public IValue<int> MaxStacks { get; set; }
        //    public IValue<StatusFusingStrategy> StatusFusingStrategy { get; set; }
        //}
    }


}