using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.effects;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon.common;

public partial class StatusEditor : PanelContainer, EditorInitiator<IStatusModel>
{
    private IStatusModel status { get; set; }


    #region Nodes - Main bar 
    [NodePath] public Button BtnSave { get; set; }
    [NodePath] public Label EntityID { get; set; }
    [NodePath] public SpinBox Delay { get; set; }
    [NodePath] public SpinBox Duration { get; set; }
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
        Delay.ValueChanged += (val) => status.delay.value = (int) val;
        Duration.ValueChanged += (val) => status.duration.value = (int) val;
        BtnSave.ButtonUp += () => status?.GetEntityBus().publish(IEventBus.save, status);
        BtnAddEffectChild.ButtonUp += onClickAddEffectChild;
    }

    #region Init
    public void init(IStatusModel model)
    {
        if(status != null) unload();
        this.status = model;
        load();
    }
    private void unload()
    {
        // unsub vaporeon
        status.GetEntityBus().unsubscribe(this.GetVaporeon(), IEventBus.save);
    }
    private void load()
    {
        // sub vaporeon
        status.GetEntityBus().subscribe(this.GetVaporeon(), IEventBus.save);
        // id & delay & duration
        EntityID.Text = "#" + status.entityUid;
        Delay.Value = status.delay.value;
        Duration.Value = status.duration.value;
    }
    #endregion

    #region GUI Handlers 
    private void onClickAddEffectChild()
    {
        var effect = EffectBase.Create();
        Eevee.models.effects.Add(effect.entityUid, effect);
        status.effectIds.Add(effect.entityUid);
        // need eventful hashset for effect ids
    }
    #endregion

}
