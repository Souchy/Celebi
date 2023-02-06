using Godot;
using Godot.Sharp.Extras;
using PlayFab.EconomyModels;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.effects;
using souchy.celebi.eevee.impl.util;
using System;
using System.Reflection;
using Umbreon.vaporeon;
using Umbreon.vaporeon.common;

public partial class EffectMini : PanelContainer
{
    private IEffect effect { get; set; }
    private IEffect parent { get; set; }

    #region Btns
    [NodePath] public Button BtnHide { get; set; }
    [NodePath] public Button BtnMoveUp { get; set; }
    [NodePath] public OptionButton BtnType { get; set; }
    [NodePath] public Button BtnEdit { get; set; }
    [NodePath] public Button BtnAddChild { get; set; }
    [NodePath] public Button BtnRemove { get; set; }
    #endregion

    #region Nodes
    [NodePath] public HBoxContainer Content { get; set; }
    [NodePath] public GridContainer Values { get; set; }
    [NodePath] public ZoneEditorMini ZoneEditorMini { get; set; }
    [NodePath] public VBoxContainer Children { get; set; }
    #endregion


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        foreach (var e in VaporeonUtil.effectTypes)
            BtnType.AddItem(e.Name);
        BtnType.Select(0);

        BtnHide.ButtonUp += onClickHide;
        BtnMoveUp.ButtonUp += onClickMoveUp;
        BtnType.ItemSelected += onClickSelectEffectType;
        BtnEdit.ButtonUp += onClickEdit;
        BtnAddChild.ButtonUp += onClickAddChild;
        BtnRemove.ButtonUp += onClickRemove;
    }

    #region Init
    public void init(IEffect e, IEffect parent = null)
    {
        unload();
        this.effect = e;
        this.parent = parent;
        load();
    }
    private void unload()
    {
        this.Values.QueueFreeChildren();
    }
    private void load()
    {
        BtnMoveUp.Disabled = (parent == null);
        // sub vaporeon
        //effect.GetEntityBus().subscribe(this.GetVaporeon(), IEventBus.save);
        // sub this
        effect.GetEntityBus().subscribe(this);
        // select type
        var indexType = VaporeonUtil.effectTypes.IndexOf(effect.GetType());
        BtnType.Select(indexType);
        // props
        PropertiesComponent.GenerateGrid(effect, Values);
        // zone
        ZoneEditorMini.init(effect.zone);
    }
    #endregion

    #region GUI Handlers
    private void onClickHide()
    {
        Content.Visible = !Content.Visible;
        Children.Visible = !Children.Visible;
    }
    private void onClickMoveUp()
    {
        //parent.children.indexof(effect)
    }
    private void onClickEdit() => this.GetVaporeon().openEditor(effect);
    private void onClickSelectEffectType(long index)
    {
        Type effectType = VaporeonUtil.effectTypes[(int) index];
        if (effectType == this.effect?.GetType()) // ignore if we didn't actually change effect type
            return;

        //public static IEffectDirectDamage Create() 
        var creator = effectType.GetMethod(nameof(EffectBase.Create)); //, BindingFlags.Static);
        IEffect newEffect = (IEffect) creator.Invoke(null, null);
        // re-init with new effect
        init(newEffect, parent);
    }
    private void onClickAddChild()
    {
        var newEffect = EffectBase.Create();
        //this.effect.children.add(newEffect);
    }
    private void onClickRemove()
    {

    }
    #endregion

}
