using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.effects;
using souchy.celebi.eevee.neweffects.impl.effects.creature;

namespace souchy.celebi.eevee.neweffects.impl
{
    public enum EffectCategory
    {
        Creature,
        Res,
        Move,
        Spell,
        Status,
        Meta, 
        Special,
    }

    /// <summary>
    /// I do feel like some effects might deserve an Actor to apply the effect to /// actually that's just the zone filter isnt it?
    ///     ex: - supplice "soigne l'attaquant de 20% des dommages subis" (status sur la cible soigne l'attaquant) 
    ///         - fangeux "soigne 50% des dommages subis"
    ///         - perfusion (sacri recoit les dégâts -> soigne les cibles qui ont le status)
    ///         - prygen "soigne 100% des dommages subis"
    ///         
    ///         - pillage (lanceur soigne alliés dégâts occasionné autour de la cible)
    ///         - tournoyant (lanceur soigne alliés dégâts occasionné autour du lanceur)
    ///         - diffusion (lanceur soigne alliés dégâts occasionné ~~reçus~~)
    ///         - sacrificiel "Soigne 20 % des dommages occasionnés." desc: "Soigne les alliés dans l'état Stimulé à hauteur de 20% des dommages subis."
    /// </summary>
    public enum EffT
    {
        // these things don't have apply() scripts
        // they have conditions elsewhere in the code, like GetTotalStats(), 
        #region Status-only
            AddStat, // any stat bonus, in status or instant.          // i think need status, nothing instant
                // includes resource for recovery that is not a heal?
            //SetState,     // i think need status // a state is a stat
            LearnSpell,     // i think need status
            ForgetSpell,    // i think need status // maybe we dont even need Script objects for status-only effects that compound like stats/spells
            ChangeAppearance, // chance the skin model: berserk, harmonie, momification, ougi rage, transfo osa, masque zobal
            ChangeAnimationSet, // panda saoul, forgelance en garde
            ReduceDamageReceived, // rempart/fortification flat reduction ? 

            #region Spell -> obviously status-only effects
                AddSpellRange,
                AddSpellEffectBaseDamage,
                ChangeSpellRangeZone,
                ChangeSpellEffectZone,
            #endregion
            #region Board
                BuildObstacle, // maybe teraforming effects are status-only on cells?
                DestroyObstacle,
                DigHole,
                FillHole,
            #endregion
        #endregion

        #region Creature
            Kill,
            Revive,
            EndTurn, // roublardise, holmgang, nécronyx,    // could have other effects  affect the timeline order
            SpawnSummon, // 
            UnspawnSummon, // 
            SpawnSummonDouble, // controllable sram's double, replica of caster
            SpawnSummonDoubleIllusion, // roublardise, replica of caster but unplayable and dies in 1 hit
            RevealCreatureSpells, // show the target's spells list to know what they can cast, like revealing their deck
        #endregion
        #region Move
            #region Translation
                Walk,
                //TranslateBy,
                //TranslateSelfTo,
                //TranslateTargetTo, // une poussée/attirance pourrait être codé par  { ChangeActor { TranslateTo } }
                PushBy,
                PullBy, // 
                DashBy,
                DashAwayBy,
                PushTo, // need a SeconaryZone in the Schema properties
                PullTo, // 
                DashTo,
                //DashAwayTo,

                // translate effects can originate from the targetCell or the sourceCell
                // -> public ActorType reference {get; set;}
            #endregion
            #region Teleportation
                SwapSelfWith,   // transpo, penitence, faille temporelle,  
                SwapTargetWith, // méprise, déambulation

                TeleportSelfTo,     // tp feca, prémonition, 
                TeleportTargetTo,   // completely different from Self because it needs a SecondaryZone in the schema
                TeleportSelfBy,         // 
                TeleportTargetBy,       // 
                TeleportSymmetricallySelfOverTarget, // tp xel
                TeleportSymmetricallyTargetOverSelf, // frappe/perturbation
                TeleportSymmetricallyAoeOverTarget,  // paradoxe, poussière, engrenage
                TeleportToPreviousPosition,          // rs, gelure, 
                TeleportToStartOfTurnPosition,       // renvoi, rembobinage // rembo peut être codé par un Status avec l'effect trigger OnTurnEnd
                TeleportToStartOfFightPosition,
            #endregion

        #endregion


        #region Meta
            ChangeActor, // change the actor of the effect 
            //ChangeSourceLocation, // Rebase, //Reposition,  // cast the effect from the target location // pas sur de voir l'intérêt vs Zone.actor
                // on peut utiliser un EmptyText aussi je pense, les children vont alors sourcer à la position des target du parent
                // ex: poison proximité: EmptyText filter trees 64 -> AddStatus aoe2
            CastSubSpell,   // détonateur -> bombe -> explosion (changeSourceActor -> castSubSpell)
            RandomChild,
            RandomPointsInZone, // take a acquisitionzone then take only x random targets in that zone
            EmptyText,
        #endregion
        #region Status
            AddStatusCreature,
            RemoveStatusCreature, // dispell
            //AddStatusCell, // éclipse, prémonition
            //RemoveStatusCell,
            AddTrap,
            AddGlyph,       // glyphe d'aveuglement (partie retrait), éclipse, prémonition, // need some glyphs with skin some invisible
            AddGlyphAura,   // glyphe d'aveuglement (partie dégât)

            AddAddStatStatus, // creates a status with AddStat ? // all ap buffs should go through a Status so it's visible 
                // mot stimu/galva, flou, 
            AddStealStatStatus, // creates 2 status with stolen resources? 1 for target, 1 for caster
        #endregion
        #region Res
            DirectDamage, // use triggers for OnResourceUse (poison paralysant), OnResourceLost (male vaudoo), OnPushed (fleche tyra), etc
            DirectDamagePercentLifeMax,
            DirectDamageStealLife, // bain de sang, pillage, concentration de chakra, folie sanguinaire

            IndirectDamage,               // poison insidieeux, injection, épidémie, brousaille, 
            IndirectDamagePercentLifeMax, // les mobs ont des poison 10%hp max
            RedirectDamage, // OnDamageReceived -> conquete, diffusion, sacrifice, (ex: répartit 50% des dégât subis en zone)

            Heal,   // heal flat
            HealPercentLifeMax, // heal %max
            HealPercentLifeDamageReceived, // OnReceiveEffect(damage) ->
                                             // diffusion, proie, feu de mine, supplice,
                                             // perfusion, arbre de vie, mot sacrificiel
            HealPercentLifeDamageDone, // OnApplyEffect(damage) ->
                                       // pillage, perquisition, piege fangeux
                                       // mot interdit, espièpgle, tournoyant

            TransferLife,           // flat life transfer
            TransferPercentLifeMax, // 10% transfusion
            DamagePerDynamicResourceUsedForSpell, // ex: spell uses all remaining ap, could use 2 or 5. this does (x damage * 2 or * 5) etc
            DamagePerManaReducedInTurn, // ex: flou retire -2pa en zone, this does (x damage * y targets * 2 ap lost)
            DamagePerMovementReducedInTurn,
        #endregion
        #region Fight
            SwapOut,
            SwapIn,
        #endregion
    }

    public record EffectType(EffectCategory Category, int LocalId, string BaseName, IEffectSchema schemaPrototype) //CharacteristicCategory Category, int LocalId, string BaseName, params ICondition[] conditions)
    {
        public IID ID { get; init; } = (IID) ((int) Category * 1000 + LocalId);
        public IID nameModelUid { get; set; } = (IID) (nameof(EffectType) + "." + BaseName);

        public IStringEntity GetName() => Eevee.models.i18n.Values.FirstOrDefault(s => s.modelUid == nameModelUid); //i18n.Get(NameID);
    }

    public record EffectTypeMove : EffectType
    {
        private static int counter = 0;
        public EffectTypeMove(string BaseName, IEffectSchema schemaPrototype)
            : base(EffectCategory.Move, counter++, BaseName, schemaPrototype)
        {
        }
        public static EffectTypeMove pushTo = new EffectTypeMove(nameof(pushTo), new PushTo());
    }
    public record EffectTypeCreature : EffectType
    {
        private static int counter = 0;
        public EffectTypeCreature(string BaseName, IEffectSchema schemaPrototype) 
            : base(EffectCategory.Creature, counter++, BaseName, schemaPrototype)
        {
        }
        public static EffectTypeCreature addStat = new EffectTypeCreature(nameof(addStat), new AddStatSchema());
    }


}
