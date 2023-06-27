using MongoDB.Bson;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.umbreon.sapphire
{

    // Go copier/améliorer AmmoliteFX de java +/-


    /// <summary>
    /// TODO
    /// - is it a Node3D ?
    /// 
    /// - A spell FX contains multiple parts:
    ///     - Can have different FX for each effect
    ///     - so depending on effect conditions or skins, you can execute different FX
    ///     - a spell cast needs to specify which effects need to execute and where
    ///     
    /// 
    /// SpellModel
    ///     EffectModel
    ///         EffectSkin -> we need this
    /// </summary>
    public abstract class FXSpell
    {

        // here, depending on the effects activated, we'll execute different vfx 
        public abstract void cast(ICreature source, ICell targetCell, ISpell spell, List<EffectApplication> fxList);

    }

    public class FireballFX : FXSpell, OnCast, OnTurnStart
    {
         public ISpellModel spellmodel; //?

        public override void cast(ICreature source, ICell targetCell, ISpell spell, List<EffectApplication> fxList)
        {
            var spellmodel = spell.GetModel();
            var effects = spellmodel.GetEffects().ToList();
        }

        public void onCast(ICreature source, ICell targetCell, Dictionary<ObjectId, List<IBoardEntity>> effectsTargets)
        {
            var effects = spellmodel.GetEffects().ToList();
            var effIdx = 0;
            var dmgEffect = effects[effIdx].entityUid;

            // 1st effect (direct damage)
            if (effectsTargets.ContainsKey(dmgEffect))
            {
                // execute projectile + explosion
            }

        }

        // implement trigger interface
        public void onTurnStart(ICreature source, ICell targetCell, Dictionary<ObjectId, List<IBoardEntity>> effectsTargets)
        {
            var effects = spellmodel.GetEffects().ToList();
            var effIdx = 1;
            var statusEffect = effects[effIdx].entityUid;

            // second effect (status)
            if (effectsTargets.ContainsKey(statusEffect))
            {
                // execute fire status vfx, but this needs to be OnTurnStart
            }
        }

    }

    public interface OnCast
    {
        public void onCast(ICreature source, ICell targetCell, Dictionary<ObjectId, List<IBoardEntity>> effectsTargets);
    }
    public interface OnTurnStart 
    {
        public void onTurnStart(ICreature source, ICell targetCell, Dictionary<ObjectId, List<IBoardEntity>> effectsTargets);
    }

    /// <summary>
    /// For each effect in the spell's hierarchy, we need to know if it's activated and what are the targets
    /// So create one of these for each effect, then put the targets inside. 
    /// We'll know just by index i guess
    /// </summary>
    public class EffectApplication
    {
        //public IEffect effect;
        public List<IBoardEntity> boardEntities;

        public List<EffectApplication> children;
    }


}
