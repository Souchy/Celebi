using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.neweffects.impl.schemas;


#region Resource
#region Damage 
public abstract record AbstractDamageSchema() : IEffectSchema
{
    public ElementType element { get; set; } = ElementType.None;
    public int baseDamage { get; set; } = 0;
    public int percentPenetration { get; set; } = 0;
    public int percentVariance { get; set; } = 0;

    public abstract IEffectSchema copy();
}
public record DirectDamage() : AbstractDamageSchema()
{
    public override IEffectSchema copy() => new DirectDamage()
    {
        element = element,
        baseDamage = baseDamage,
        percentPenetration = percentPenetration,
        percentVariance = percentVariance
    };
}
// Indirect Damage ignore defensive stats so they can't have penetration either
public record IndirectDamage() : AbstractDamageSchema()
{
    public override IEffectSchema copy() => new IndirectDamage()
    {
        element = element,
        baseDamage = baseDamage,
        percentPenetration = percentPenetration,
        percentVariance = percentVariance
    };
} // no pen

// PercentLife damage don't benefit from offensive stats (affinities), but they calculate defensive stats (resistance, penetration)
public record DirectDamagePercentLifeMax() : AbstractDamageSchema()
{
    public override IEffectSchema copy() => new DirectDamagePercentLifeMax()
    {
        element = element,
        baseDamage = baseDamage,
        percentPenetration = percentPenetration,
        percentVariance = percentVariance
    };
}
public record IndirectDamagePercentLifeMax() : AbstractDamageSchema()
{
    public override IEffectSchema copy() => new IndirectDamagePercentLifeMax()
    {
        element = element,
        baseDamage = baseDamage,
        percentPenetration = percentPenetration,
        percentVariance = percentVariance
    };
} // no pen

public record RedirectDamage() : IEffectSchema
{
    public int percentRedirect { get; set; } = 0;
    public IEffectSchema copy() => new RedirectDamage()
    {
        percentRedirect = percentRedirect,
    };
}

public record DamagePerDynamicResourceUsedForSpell() : IEffectSchema
{
    public CharacteristicId charId { get; set; } = Resource.Life.ID;
    public int baseDamagePerCharacUsed { get; set; } = 0;
    public IEffectSchema copy() => new DamagePerDynamicResourceUsedForSpell()
    {
        charId = charId,
        baseDamagePerCharacUsed = baseDamagePerCharacUsed
    };
}

public record DamagePerContextualStat() : AbstractDamageSchema
{
    public CharacteristicId statId { get; set; } = Contextual.DamageTaken.ID;
    /// <summary>
    /// multiplies the damageTaken or whatever ContextualStat
    /// </summary>
    //public int multiplier => this.baseDamage;
    /// <summary>
    /// Ex: take 1000 damage, deal 100% of that back
    /// </summary>
    public bool isMultiplierPercentage = false;
    public override IEffectSchema copy() => new DamagePerContextualStat()
    {
        element = element,
        baseDamage = baseDamage,
        percentPenetration = percentPenetration,
        percentVariance = percentVariance,
        statId = statId,
        isMultiplierPercentage = isMultiplierPercentage,
    };
}
#endregion
#region Heal
public record Heal() : IEffectSchema
{
    public ElementType element { get; set; } = ElementType.None;
    public int baseHeal { get; set; } = 0;
    public int percentVariance { get; set; } = 0;

    public virtual IEffectSchema copy() => new Heal()
    {
        element = element,
        baseHeal = baseHeal,
        percentVariance = percentVariance,
    };
}
public record HealPerContextualStat() : Heal
{
    public CharacteristicId statId { get; set; } = Contextual.DamageTaken.ID;
    /// <summary>
    /// multiplies the damageTaken or whatever ContextualStat
    /// </summary>
    //public int multiplier => this.baseHeal;
    /// <summary>
    /// Ex: take 1000 damage, heal 100% of that
    /// </summary>
    public bool isMultiplierPercentage = false;

    public override IEffectSchema copy() => new HealPerContextualStat()
    {
        element = element,
        baseHeal = baseHeal,
        percentVariance = percentVariance,
        statId = statId,
        isMultiplierPercentage = isMultiplierPercentage,
        //multiplier = multiplier
    };
}
public record HealPercentLifeMax() : IEffectSchema
{
    public ElementType element { get; set; } = ElementType.None;
    public int percentHeal { get; set; } = 0;
    public ActorType percentOfWhoseLife { get; set; } = ActorType.Target;
    public IEffectSchema copy() => new HealPercentLifeMax()
    {
        element = element,
        percentHeal = percentHeal,
        percentOfWhoseLife = percentOfWhoseLife
    };
}
/// <summary>
///  child of DamageEffect
///  (ex: pillage, piège fangeux, mot interdit..)
///  (DmgEff selects targets, then the heal starts from their positions and heals according to its new TargetAcquisitionZone)
///  That means you need to calculate Area += foreach(zone.area(target))
/// </summary>
public record HealPercentDamageDoneByEffect() : IEffectSchema
{
    public ElementType element { get; set; } = ElementType.None;
    public int percentHeal { get; set; } = 0;
    public IEffectSchema copy() => new HealPercentDamageDoneByEffect()
    {
        element = element,
        percentHeal = percentHeal,
    };
}
#endregion
#region Both
public record DirectDamageStealLife() : AbstractDamageSchema()
{
    public override IEffectSchema copy() => new DirectDamageStealLife()
    {
        element = element,
        baseDamage = baseDamage,
        percentPenetration = percentPenetration,
        percentVariance = percentVariance,
    };
}

public record TransferLife() : IEffectSchema
{
    public IZone transferFrom = new Zone();
    public int value { get; set; } = 0;
    public int percentVariance { get; set; } = 0;
    public IEffectSchema copy() => new TransferLife()
    {
        transferFrom = transferFrom.copy(),
        value = value,
        percentVariance = percentVariance
    };
}
public record TransferPercentLifeMax() : IEffectSchema
{
    public IZone transferFrom = new Zone();
    public int percentValue { get; set; } = 0;
    public IEffectSchema copy() => new TransferPercentLifeMax()
    {
        transferFrom = transferFrom.copy(),
        percentValue = percentValue,
    };
}
#endregion
#endregion