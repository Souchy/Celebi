using souchy.celebi.eevee.face.objects;
using System;
using System.Collections.Generic;
namespace souchy.celebi.eevee.impl.objects.effectReturn
{
    /// <summary>
    /// Examples:
    /// - EffectGetValue? EffectAddStat?: return the stat / value according to conditions
    /// - EffectMathAdd: return the sum of x + y where both can be numbers or other effects
    /// - 
    /// </summary>
    //public interface IEffectReturnValue
    //{
    //    public IEffect effect { get; set; }
    //    public object value { get; set; }
    //}
    public record IEffectReturnValue(IEffect effect, object value);
}
