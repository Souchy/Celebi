using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.util;
using System;

public partial class EffectEditor : Control
{

    public IEffect effect { get => this.GetVaporeon().CurrentEffect;  }


    #region Nodes
    [NodePath("ParametersBox/ParametersGrid")]
    public GridContainer ParametersGrid { get; set; }
    [NodePath]
    public Label EffectID { get; set; }
    [NodePath]
    public LineEdit ModelID { get; set; }
    #endregion


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
	}

    [Subscribe(nameof(Vaporeon.CurrentEffect))]
    public void onModelChange(IEffect model)
    {
    }


}
