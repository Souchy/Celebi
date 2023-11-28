using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.effects.move;

namespace souchy.celebi.eevee.neweffects.impl.schemas;

#region Move Translation
public record Walk() : MoveSchema
{
    public override IEffectSchema copy() => new Walk()
    {
        MoveTargetZone = MoveTargetZone.copy(),
    };
}
public record PushBy() : MoveSchema
{
    public int distance { get; set; } = 1;
    public override IEffectSchema copy() => new PushBy()
    {
        MoveTargetZone = MoveTargetZone.copy(),
        distance = distance
    };
}
public record PullBy() : MoveSchema
{
    public int distance { get; set; } = 1;
    public override IEffectSchema copy() => new PullBy()
    {
        MoveTargetZone = MoveTargetZone.copy(),
        distance = distance
    };
}
public record DashBy() : MoveSchema
{
    public int distance { get; set; } = 1;
    public override IEffectSchema copy() => new DashBy()
    {
        MoveTargetZone = MoveTargetZone.copy(),
        distance = distance
    };
}
public record DashAwayBy() : MoveSchema
{
    public int distance { get; set; } = 1;
    public override IEffectSchema copy() => new DashAwayBy()
    {
        MoveTargetZone = MoveTargetZone.copy(),
        distance = distance
    };
}
public record PushTo() : MoveSchema
{
    public override IEffectSchema copy() => new PushTo()
    {
        MoveTargetZone = MoveTargetZone.copy()
    };
}
public record PullTo() : MoveSchema
{
    public override IEffectSchema copy() => new PullTo()
    {
        MoveTargetZone = MoveTargetZone.copy()
    };
}
public record DashTo() : MoveSchema
{
    public override IEffectSchema copy() => new DashTo()
    {
        MoveTargetZone = MoveTargetZone.copy()
    };
}
#endregion

#region Move Teleportation
public record SwapSelfWith() : IEffectSchema
{
    public IEffectSchema copy() => new SwapSelfWith();
}
public record SwapTargetWith() : MoveSchema
{
    public override IEffectSchema copy() => new SwapTargetWith()
    {
        MoveTargetZone = MoveTargetZone.copy()
    };
}
/// <summary>
/// BoardTargeetType = cell
/// </summary>
public record TeleportSelfTo() : IEffectSchema
{
    public IEffectSchema copy() => new TeleportSelfTo();
}
/// <summary>
/// BoardTargetType = creature
/// </summary>
public record TeleportTargetTo() : MoveSchema
{
    public override IEffectSchema copy() => new TeleportTargetTo()
    {
        MoveTargetZone = MoveTargetZone.copy()
    };
}
public record TeleportSelfBy() : IEffectSchema
{
    public int distance { get; set; } = 1;
    public IEffectSchema copy() => new TeleportSelfBy()
    {
        distance = distance
    };
}
public record TeleportTargetBy() : MoveSchema
{
    public int distance { get; set; } = 1;
    public override IEffectSchema copy() => new TeleportTargetBy()
    {
        MoveTargetZone = MoveTargetZone.copy(),
        distance = distance
    };
}
public record TeleportSymmetricallySelfOverTarget() : IEffectSchema
{
    public IEffectSchema copy() => new TeleportSymmetricallySelfOverTarget();
}
public record TeleportSymmetricallyTargetOverSelf() : IEffectSchema
{
    public IEffectSchema copy() => new TeleportSymmetricallyTargetOverSelf();
}
public record TeleportSymmetricallyAoeOverTarget() : MoveSchema
{
    public override IEffectSchema copy() => new TeleportSymmetricallyAoeOverTarget()
    {
        MoveTargetZone = MoveTargetZone.copy(),
    };
}
public record TeleportToPreviousPosition() : IEffectSchema
{
    public IEffectSchema copy() => new TeleportToPreviousPosition();
}
public record TeleportToStartOfTurnPosition() : IEffectSchema
{
    public IEffectSchema copy() => new TeleportToStartOfTurnPosition();
}
public record TeleportToStartOfFightPosition() : IEffectSchema
{
    public IEffectSchema copy() => new TeleportToStartOfFightPosition();
}
#endregion
