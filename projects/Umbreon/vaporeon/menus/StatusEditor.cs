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
using souchy.celebi.eevee.face.objects;
using Umbreon.vaporeon.components;

public partial class StatusEditor : PanelContainer, EditorInitiator<IStatusModel>, IEffectNodesContainer
{
    private IStatusModel status { get; set; }

    #region Impl EffectContainer
    public IEntityList<IID> parentList { get; private set; } = null;
    public IEntityList<IID> GetEffectIds() => this.status.effectIds;
    public IEnumerable<IEffect> GetEffectsEnum() => this.status.GetEffects();
    public Control GetContainer() => this.EffectsChildren;
    #endregion

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
        BtnAddEffectChild.ButtonUp += this.onClickAddChild;
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
        // unsub this
        status.effectIds.GetEntityBus().unsubscribe(this);
    }
    private void load()
    {
        // sub vaporeon
        status.GetEntityBus().subscribe(this.GetVaporeon(), IEventBus.save);
        // sub this
        status.effectIds.GetEntityBus().subscribe(this);
        // id & delay & duration
        EntityID.Text = "#" + status.entityUid;
        Delay.Value = status.delay.value;
        Duration.Value = status.duration.value;
        //
        this.fillEffects();
    }
    #endregion

    #region GUI Handlers 
    #endregion

}
