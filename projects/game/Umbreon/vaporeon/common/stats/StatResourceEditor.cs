using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon.common;

public partial class StatResourceEditor : CenterContainer, EditorInitiator<IStatResource>
{

    private IStatResource stat { get; set; }

    #region Nodes
    [NodePath] public SpinBox CurrentValue { get; set; }
    [NodePath] public SpinBox CurrentMax { get; set; }
    [NodePath] public SpinBox InitialMax { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        CurrentValue.ValueChanged += (val) => stat.current = (int) val;
        CurrentMax.ValueChanged += (val) => stat.currentMax = (int) val;
        InitialMax.ValueChanged += (val) => stat.initialMax = (int) val;
    }

    public void init(IStatResource s)
    {
        this.stat?.GetEntityBus().unsubscribe(this);
        this.stat = s;
        this.stat.GetEntityBus().subscribe(this);
        onStatChanged(this.stat);
    }

    [Subscribe]
    public void onStatChanged(IStatResource stat)
    {
        CurrentValue.Value = stat.current;
        CurrentMax.Value = stat.currentMax;
        InitialMax.Value = stat.initialMax;
    }
}
