using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.effects
{
    public class EffectType
    {

        /*
         * List of Effect Model Type
         *      DirectDamage
         *      Swap
         *      Teleport
         *      ...
         *      
         * Effect:
         *      modelUid
         *      properties<string, value>
         *          key: value
         *          ["element": air]
         *          ["dmg": 10]
         *      conditions
         *      zone
         *      triggers
         *      children
         *      apply => model.apply(this)
         *      preview => model.preview(this)
         *      
         *      
         * EffectModelX:
         *      modelUid
         *      nameId
         *      descriptionId
         *      EffectModelProperty[] !!!?
         *          [ {name = 'element', type = ElementType }, 
         *            {name = 'dmg', type = int }
         *          ]
         *      scriptApply(effect) !!!
         *      scriptPreview(effect) !!!
         *      
         *  EffectModelXScript:
         *      scriptApply(effect) !!!
         *      scriptPreview(effect) !!!
         *      
         * EffectDmgProperties:   // Schema
         *      ele: Element
         *      dmg: int
         *      
         *  EffectModelProperty:
         *      string name
         *      type classtype
         *      
         *      
         *      
         *  EffectDamage.apply -> Element ele = effect.properties.get('element')
         *  EffectDamage.apply -> Element ele = effect.properties.get(nameof(DamageModelProperties.element))
         *  EffectDamage.apply -> DamageModelProperties props = (DamageModelProperties) effect.properties;
         *      EffectModel.propertiesType = typeof(DamageModelProperties)
         */


        /*
         * Spell boosts
        public static ACTION_BOOST_SPELL_RANGE_MIN: number = 280;
        public static ACTION_BOOST_SPELL_RANGE_MAX: number = 281;
        public static ACTION_BOOST_SPELL_RANGEABLE: number = 282;
        public static ACTION_BOOST_SPELL_DMG: number = 283;
        public static ACTION_BOOST_SPELL_HEAL: number = 284;
        public static ACTION_BOOST_SPELL_AP_COST: number = 285;
        public static ACTION_BOOST_SPELL_CAST_INTVL: number = 286;
        public static ACTION_BOOST_SPELL_CC: number = 287;
        public static ACTION_BOOST_SPELL_CASTOUTLINE: number = 288;
        public static ACTION_BOOST_SPELL_NOLINEOFSIGHT: number = 289;
        public static ACTION_BOOST_SPELL_MAXPERTURN: number = 290;
        public static ACTION_BOOST_SPELL_MAXPERTARGET: number = 291;
        public static ACTION_BOOST_SPELL_CAST_INTVL_SET: number = 292;
        public static ACTION_BOOST_SPELL_BASE_DMG: number = 293;
        public static ACTION_DEBOOST_SPELL_RANGE_MAX: number = 294;
        public static ACTION_DEBOOST_SPELL_RANGE_MIN: number = 295;
        public static ACTION_DEBOOST_SPELL_AP_COST: number = 296;
        public static ACTION_DEBOOST_OCCUPIED_CELL: number = 297;
         */

        /*
         * 
         * 
            {
                "targetMask": "A,E643,*E2761",
                "diceNum": 23759,
                "visibleInBuffUi": false,
                "baseEffectId": 0,
                "visibleInFightLog": false,
                "targetId": 0,
                "effectElement": -1,
                "effectUid": 267185,
                "dispellable": 1,
                "triggers": "I",
                "spellId": 12967,
                "duration": 0,
                "random": 0,
                "effectId": 1160,
                "delay": 0,
                "diceSide": 1,
                "visibleOnTerrain": true,
                "visibleInTooltip": false,
                "rawZone": "P1",
                "forClientOnly": true,
                "value": 999,
                "order": 1,
                "group": 0
            }
         */


    }
}
