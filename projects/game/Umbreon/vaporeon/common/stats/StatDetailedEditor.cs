using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.umbreon.vaporeon.common;
using System;

public partial class StatDetailedEditor : CenterContainer, EditorInitiator<IStatDetailed>
{

    private IStatDetailed stat { get; set; }

    #region Nodes
    [NodePath] public SpinBox BaseFlat { get; set; }
    [NodePath] public SpinBox IncPerc { get; set; }
    [NodePath] public SpinBox IncFlat { get; set; }
    [NodePath] public SpinBox MorePerc { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        BaseFlat.ValueChanged += (val) => stat.baseFlat = (int) val;
        IncPerc.ValueChanged  += (val) => stat.increasedPercent = (int) val;
        IncFlat.ValueChanged  += (val) => stat.increasedFlat = (int) val;
        MorePerc.ValueChanged += (val) => stat.morePercent = (int) val;
    }

    public void init(IStatDetailed s)
    {
        this.stat?.GetEntityBus().unsubscribe(this);
        this.stat = s;
        this.stat.GetEntityBus().subscribe(this);
        onStatChanged(this.stat);
    }

    [Subscribe]
    public void onStatChanged(IStatDetailed stat)
    {
        BaseFlat.Value = stat.baseFlat;
        IncPerc.Value = stat.increasedPercent;
        IncFlat.Value = stat.increasedFlat;
        MorePerc.Value = stat.morePercent;
    }

}
