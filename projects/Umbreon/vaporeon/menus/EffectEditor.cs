using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.effects;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon;
using Umbreon.vaporeon.common;
using Umbreon.vaporeon.components;

public partial class EffectEditor : Control, IEffectNodesContainer // EditorInitiator<IEffect>, 
{

    private IEffect effect { get; set; }

    #region Impl EffectContainer
    public IEntityList<IID> parentList { get; private set; }
    public IEntityList<IID> GetEffectIds() => this.effect.effectIds;
    public IEnumerable<IEffect> GetEffectsEnum() => this.effect.GetEffects();
    public Control GetContainer() => this.EffectsChildren;
    #endregion

    #region Nodes - Main bar 
    [NodePath] public Button BtnSave { get; set; }
    [NodePath] public Label EntityID { get; set; }
    [NodePath] public OptionButton BtnType { get; set; }
    #endregion

    #region Nodes
    [NodePath] public GridContainer Values { get; set; }
    [NodePath] public ZoneEditorMini ZoneEditorMini { get; set; }
    [NodePath] public Button BtnStatus { get; set; }
    [NodePath] public GridContainer StatusValues { get; set; }
    [NodePath] public Button BtnConditions { get; set; }
    [NodePath] public VBoxContainer Conditions { get; set; }
    [NodePath] public VBoxContainer SourceConditions { get; set; }
    [NodePath] public VBoxContainer TargetConditions { get; set; }
    [NodePath] public VBoxContainer TriggerConditions { get; set; }
    #endregion

    #region Effects Children
    [NodePath] public Button BtnAddEffectChild { get; set; }
    [NodePath] public VBoxContainer EffectsChildren { get; set; }

    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        foreach (var e in VaporeonUtil.effectTypes)
            BtnType.AddItem(e.Name);
        EffectsChildren.QueueFreeChildren();

        BtnType.ItemSelected += onClickSelectEffectType;
        BtnAddEffectChild.ButtonUp += this.onClickAddChild; 

        // save
        BtnSave.ButtonUp += publishSave;
        // tabs
        BtnStatus.Toggled += (t) => StatusValues.Visible = t;
        BtnConditions.Toggled += (t) => Conditions.Visible = t;
    }

    public void init(IEffect e, IEntityList<IID> parent = null)
    {
        unload();
        this.effect = e;
        this.parentList = parent;
        load();
    }
    private void unload()
    {
        if (effect == null) return;
        //if (effect != null && effect.GetEntityBus() == null)
        //    GD.PrintErr($"EffectMini: effect has no entity bus {effect}");
        // unsub vaporeon
        effect?.GetEntityBus()?.unsubscribe(this.GetVaporeon(), IEventBus.save);
        // unsub this
        effect?.GetEntityBus()?.unsubscribe(this);
        effect?.effectIds.GetEntityBus()?.unsubscribe(this);
        //
        this.Values.RemoveAndQueueFreeChildren();
        this.EffectsChildren.RemoveAndQueueFreeChildren();
        this.StatusValues.RemoveAndQueueFreeChildren();
        // TODO conditions
        // ...

        // unsub vaporeon (save event)
        //effect.GetEntityBus().unsubscribe(this, IEventBus.save);
        //// sub this
        //effect.GetEntityBus().subscribe(this);

    }
    private void load()
    {
        if (effect == null) return;
        // sub vaporeon
        effect.GetEntityBus().subscribe(this.GetVaporeon(), IEventBus.save);
        // sub this
        effect.GetEntityBus().subscribe(this);
        effect.effectIds.GetEntityBus().subscribe(this);
        //
        this.EntityID.Text = "#" + effect.entityUid;
        //this.LblChildCount.Text = $"({effect.effectIds.Values.Count})";
        // select type
        var indexType = VaporeonUtil.effectTypes.IndexOf(effect.GetType());
        BtnType.Select(indexType);
        // props
        PropertiesComponent.GenerateGrid(effect, Values);
        // status props
        if (effect.statusProperties != null)
            PropertiesComponent.GenerateGrid(effect.statusProperties, StatusValues, publishSave);
        // zone
        ZoneEditorMini.init(effect.zone);
        // create children
        this.fillEffects();
        // TODO conditions
        // ...

        //// sub vaporeon (save event)
        //effect.GetEntityBus().subscribe(this, IEventBus.save);
    }

    private void onClickSelectEffectType(long index)
    {
        Type effectType = VaporeonUtil.effectTypes[(int) index];
        if (effectType == this.effect?.GetType()) // ignore if we didn't actually change effect type
            return;
        // create
        var creator = effectType.GetMethod(nameof(EffectBase.Create));
        IEffect newEffect = (IEffect) creator.Invoke(null, null);
        effect.CopyBasicTo(newEffect);
        // swap in eevee models
        Eevee.models.effects.Remove(this.effect.entityUid);
        Eevee.models.effects.Add(newEffect.entityUid, newEffect);
        // replace in parent (add/remove without event that would replace the node and mess the order)
        if (parentList != null)
        {
            parentList.Replace(effect.entityUid, newEffect.entityUid);
        }
        // re-init this node with new effect
        init(newEffect, parentList);
    }

    public void publishSave()
    {
        effect.GetEntityBus().publish(IEventBus.save, effect);
    }

}
