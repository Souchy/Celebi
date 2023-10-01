using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.neweffects.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{

    public abstract class TriggerOnEffect : TriggerSchema
    {
        public List<TriggerOnEffectFilter> effectTypesInclude { get; set; }
        public List<TriggerOnEffectFilter> effectTypesExclude { get; set; }
    }
    public class TriggerOnEffectCast : TriggerOnEffect
    {
    }
    public class TriggerOnEffectReceive : TriggerOnEffect
    {
    }


    public class TriggerOnEffectFilter
    {
        public EffT effectType { get; set; }
        /// <summary>
        /// Example just 1 value like 5 or "hello".
        /// Example for an Damage EffectSchema: valueFilter = DataTypeDictionary
        ///     [ 
        ///         {"damageElement"} = {5}, ///  (5 = fire elementid) 
        ///         {"damage"} = {min: 10, max: 15} 
        ///     ]
        /// Example for an ReduceResource EffectSchema: valueFilter = DataTypeDictionary[ {"resource"} = {mana}, {"value"} = {3} ]
        ///     (match the properties names?)
        /// </summary>
        public DataType valueFilter { get; set; }
    }

}
