using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon.common;

public partial class StatBoolEditor : CenterContainer, EditorInitiator<IStatBool>
{

    private IStatBool stat { get; set; }

    #region Nodes
    [NodePath]
    public CheckButton Value { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        Value.ButtonUp += () => stat.value = !stat.value;
	}

    public void init(IStatBool s)
    {
        this.stat?.GetEntityBus().unsubscribe(this);
        this.stat = s;
        this.stat.GetEntityBus().subscribe(this);
        onStatChanged(this.stat);
    }

    [Subscribe]
    public void onStatChanged(IStatBool stat)
    {
        Value.ButtonPressed = stat.value;
    }

}
