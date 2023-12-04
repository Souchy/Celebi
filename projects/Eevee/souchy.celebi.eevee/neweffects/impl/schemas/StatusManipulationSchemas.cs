using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.neweffects.impl.schemas;

#region Status Create
public interface ICreateStatusSchema : IEffectsContainer
{
}

public record CreateStatusCreature() : IEffectSchema, ICreateStatusSchema
{
    public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
    /// <summary>
    /// Effects to add to the status
    /// </summary>
    public IEntityList<ObjectId> EffectIds { get; set; } = new EntityList<ObjectId>();
    public IEnumerable<IEffect> GetEffects() => this.EffectIds.Values.Select(i => Eevee.models.effects.Get(i));

    public IEffectSchema copy() => new CreateStatusCreature()
    {
        statusStats = (StatusModelStats)statusStats.copy()
    };
}
public record CreateTrap() : IEffectSchema, ICreateStatusSchema
{
    public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
    /// <summary>
    /// Effects to add to the status
    /// </summary>
    public IEntityList<ObjectId> EffectIds { get; set; } = new EntityList<ObjectId>();
    public IEnumerable<IEffect> GetEffects() => this.EffectIds.Values.Select(i => Eevee.models.effects.Get(i));
    public IEffectSchema copy() => new CreateTrap()
    {
        statusStats = (StatusModelStats)statusStats.copy()
    };
}
public record CreateGlyph() : IEffectSchema, ICreateStatusSchema
{
    public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
    /// <summary>
    /// Effects to add to the status
    /// </summary>
    public IEntityList<ObjectId> EffectIds { get; set; } = new EntityList<ObjectId>();
    public IEnumerable<IEffect> GetEffects() => this.EffectIds.Values.Select(i => Eevee.models.effects.Get(i));
    public IEffectSchema copy() => new CreateGlyph()
    {
        statusStats = (StatusModelStats)statusStats.copy()
    };
}
public record CreateGlyphAura() : IEffectSchema, ICreateStatusSchema
{
    public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
    /// <summary>
    /// Effects to add to the status
    /// </summary>
    public IEntityList<ObjectId> EffectIds { get; set; } = new EntityList<ObjectId>();
    public IEnumerable<IEffect> GetEffects() => this.EffectIds.Values.Select(i => Eevee.models.effects.Get(i));
    public IEffectSchema copy() => new CreateGlyphAura()
    {
        statusStats = (StatusModelStats)statusStats.copy()
    };
}
#endregion

#region Status Add
public record AddStatusCreature() : IEffectSchema
{
    public StatusIID modelId { get; set; } = new();
    public IEffectSchema copy() => new AddStatusCreature()
    {
        modelId = modelId,
    };
}
public record AddTrap() : IEffectSchema
{
    public StatusIID modelId { get; set; } = new();
    public IEffectSchema copy() => new AddTrap()
    {
        modelId = modelId,
    };
}
public record AddGlyph() : IEffectSchema
{
    public StatusIID modelId { get; set; } = new();
    public IEffectSchema copy() => new AddGlyph()
    {
        modelId = modelId,
    };
}
public record AddGlyphAura() : IEffectSchema
{
    public StatusIID modelId { get; set; } = new();
    public IEffectSchema copy() => new AddGlyphAura()
    {
        modelId = modelId,
    };
}
#endregion

#region Status Remove
public record RemoveStatusCreature() : IEffectSchema
{
    public ICondition statusFilter { get; set; }
    public int durationToRemove { get; set; }
    public IEffectSchema copy() => new RemoveStatusCreature()
    {
        statusFilter = statusFilter.copy(),
        durationToRemove = durationToRemove
    };
}
public record RemoveTrap() : IEffectSchema
{
    public ICondition statusFilter { get; set; }
    public int durationToRemove { get; set; }
    public IEffectSchema copy() => new RemoveTrap()
    {
        statusFilter = statusFilter.copy(),
        durationToRemove = durationToRemove
    };
}
public record RemoveGlyph() : IEffectSchema
{
    public ICondition statusFilter { get; set; } // IStatusCondition
    public int durationToRemove { get; set; }
    public IEffectSchema copy() => new RemoveGlyph()
    {
        statusFilter = statusFilter.copy(),
        durationToRemove = durationToRemove
    };
}
#endregion