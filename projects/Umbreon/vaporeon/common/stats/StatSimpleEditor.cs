using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon.common;

public partial class StatSimpleEditor : CenterContainer, EditorInitiator<IStatSimple>
{

    private IStatSimple stat { get; set; }

    #region Nodes
    [NodePath] public SpinBox Value { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        Value.ValueChanged += (val) => stat.value = (int) val;
    }

    public void init(IStatSimple s)
    {
        this.stat?.GetEntityBus().unsubscribe(this);
        this.stat = s;
        this.stat.GetEntityBus().subscribe(this);
        onStatChanged(this.stat);
    }

    [Subscribe]
    public void onStatChanged(IStatSimple stat)
    {
        Value.Value = stat.value;
    }
}
