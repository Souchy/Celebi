using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon.common;

public partial class EffectEditor : Control, EditorInitiator<IEffect>
{

    private IEffect effect { get; set; }


    #region Nodes - Main bar 
    [NodePath] public Button BtnSave { get; set; }
    [NodePath] public Label EntityID { get; set; }
    [NodePath] public OptionButton BtnType { get; set; }
    #endregion

    #region Nodes
    [NodePath] public GridContainer ParametersGrid { get; set; }
    [NodePath] public ZoneEditorMini ZoneEditorMini { get; set; }
    //[NodePath] Source Conditions
    //[NodePath] Target Conditions
    //[NodePath] Trigger Conditions
    #endregion

    #region Effects Children
    [NodePath] public Button BtnAddEffectChild { get; set; }
    [NodePath] public VBoxContainer EffectsChildren { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        EffectsChildren.QueueFreeChildren();

    }

    public void init(IEffect effect)
    {
        if(this.effect != null) unload();
        this.effect = effect;
        load();
    }
    private void unload()
    {
        //effect.GetEntityBus().unsubscribe(this);
    }
    private void load()
    {

    }


}
