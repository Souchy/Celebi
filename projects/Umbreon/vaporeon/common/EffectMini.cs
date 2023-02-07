using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.shared.effects;
using souchy.celebi.eevee.impl.util;
using Umbreon.vaporeon;
using Umbreon.vaporeon.common;
using Umbreon.vaporeon.components;

public partial class EffectMini : PanelContainer, IEffectNodesContainer
{


    public IEffect effect { get; private set; }

    #region Impl EffectContainer
    public IEntityList<IID> parentList { get; private set; }
    public IEntityList<IID> GetEffectIds() => this.effect.effectIds;
    public IEnumerable<IEffect> GetEffectsEnum() => this.effect.GetEffects();
    public Control GetContainer() => this.Children;
    #endregion

    #region Btns
    [NodePath] public Label LblID { get; set; }
    [NodePath] public Label LblChildCount { get; set; }
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
        BtnAddChild.ButtonUp += ((IEffectNodesContainer) this).onClickAddChild; //onClickAddChild;
        BtnRemove.ButtonUp += onClickDeleteThis;
    }

    #region Init
    public void init(IEffect e, IEntityList<IID> parent = null)
    {
        unload();
        this.effect = e;
        this.parentList = parent;
        load();
    }
    private void unload()
    {
        // unsub this
        effect?.GetEntityBus().unsubscribe(this);
        //parentList?.GetEntityBus().unsubscribe(this);
        effect?.effectIds.GetEntityBus().unsubscribe(this);
        //
        this.Values.QueueFreeChildren();
    }
    private void load()
    {
        // sub vaporeon
        //effect.GetEntityBus().subscribe(this.GetVaporeon(), IEventBus.save);
        // sub this
        effect.GetEntityBus().subscribe(this);
        //parentList?.GetEntityBus().subscribe(this);
        effect.effectIds.GetEntityBus().subscribe(this);
        //
        this.LblID.Text = effect.entityUid;
        this.LblChildCount.Text = $"({effect.effectIds.Values.Count})";
        // select type
        var indexType = VaporeonUtil.effectTypes.IndexOf(effect.GetType());
        BtnType.Select(indexType);
        // props
        PropertiesComponent.GenerateGrid(effect, Values);
        // zone
        ZoneEditorMini.init(effect.zone);
        // dis/enable buttons
        var i = parentList?.Values.IndexOf(effect.entityUid);
        BtnMoveUp.Disabled = (parentList == null || i == 0);
        // create children
    }
    #endregion


    #region GUI Handlers
    private void onClickHide()
    {
        Content.Visible = !Content.Visible;
        Children.Visible = !Children.Visible;
    }
    private void onClickEdit() => this.GetVaporeon().openEditor(effect);
    private void onClickSelectEffectType(long index)
    {
        Type effectType = VaporeonUtil.effectTypes[(int) index];
        if (effectType == this.effect?.GetType()) // ignore if we didn't actually change effect type
            return;
        var creator = effectType.GetMethod(nameof(EffectBase.Create)); 
        IEffect newEffect = (IEffect) creator.Invoke(null, null);
        // re-init with new effect
        init(newEffect, parentList);
    }
    private void onClickMoveUp()
    {
        parentList.Move(effect.entityUid, -1);
    }
    private void onClickDeleteThis()
    {
        bool removed = Eevee.models.effects.Remove(effect.entityUid);
        //GD.Print($"Click Delete on {effect.entityUid} = {removed}");
        if(parentList != null)
        {
            bool removedInParent = parentList.Remove(effect.entityUid);
            GD.Print($"Click Delete from parent {effect.entityUid} = {removedInParent}");
        }
    }

    #endregion

    /*
    #region Diamond Handlers
    [Subscribe(EntityList<IID>.EventAdd)]
    public void onAddEffectChild(IID ei)
    {
        this.LblChildCount.Text = $"({effect.effectIds.Values.Count})";
        var e = Eevee.models.effects.Get(ei);
        var mini = Vaporeon.instanceScene<EffectMini>(); // packedScene.Instantiate<EffectMini>(); // new EffectMini();
        GD.Print($"add child scene: {mini} with e: {e}");
        this.Children.AddChild(mini);
        mini.init(e, effect);
    }
    [Subscribe(EntityList<IID>.EventRemove)]
    public void onRemoveEffectChild(IID ei)
    {
        GD.Print($"On Remove {ei}");
        this.LblChildCount.Text = $"({effect.effectIds.Values.Count})";
        var node = this.Children.GetChildren<EffectMini>().FirstOrDefault(m => m.effect.entityUid == ei);
        if(node != null) 
            this.Children.RemoveChild(node);
    }
    [Subscribe(EntityList<IID>.EventMove)]
    public void onMoveEffectChild(IID ei, int indexPrevious, int indexNow)
    {
        var node = this.Children.GetChild(indexPrevious);
        this.Children.MoveChild(node, indexNow);
        // dis/enable buttons
        for (int i = 0; i < effect.effectIds.Values.Count; i++)
        {
           var c = (EffectMini) Children.GetChild(i);
           c.BtnMoveUp.Disabled = i == 0;
        }
        //var i = parent?.children.Values.IndexOf(effect.entityUid);
        //BtnMoveUp.Disabled = (parent == null || i == 0);
    }
    #endregion
    */

}
