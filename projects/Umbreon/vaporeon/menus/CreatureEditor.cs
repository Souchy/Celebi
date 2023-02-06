using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon;
using Umbreon.vaporeon.common;

public partial class CreatureEditor : Control, EditorInitiator<ICreatureModel>
{
    private ICreatureModel creature { get; set; }
    //public ICreatureModel creature { get => this.GetVaporeon().CurrentCreatureModel; }

    
    #region Nodes - Main bar 
    [NodePath] public Button BtnSave { get; set; }
    [NodePath] public Label EntityID { get; set; }
    [NodePath] public LineEdit NameEdit { get; set; }
    [NodePath] public LineEdit DescriptionEdit { get; set; }
    #endregion

    #region Nodes - Content
    [NodePath] public StatsEditor StatsEditor { get; set; }
    [NodePath] public Button BtnAddSpell { get; set; }
    [NodePath] public VFlowContainer SpellsList { get; set; }
    [NodePath] public Button BtnAddPassive { get; set; }
    [NodePath] public VFlowContainer PassivesList { get; set; }
    [NodePath] public Button BtnAddSkin { get; set; }
    [NodePath] public VFlowContainer SkinsList { get; set; }
    #endregion

    #region Init
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        GD.Print("CreatureEditor ready");
        this.GetVaporeon().bus.subscribe(this);


        //creature?.GetEntityBus().subscribe(this);
        //creature.GetBaseStats().GetEntityBus().subscribe(this);
        //Eevee.models.i18n.GetEntityBus().subscribe(this, nameof(onNameChanged));
        NameEdit.TextChanged += (txt) => creature.GetName().value = txt;
        DescriptionEdit.TextChanged += (txt) => creature.GetDescription().value = txt;

        BtnSave.ButtonUp += onClickBtnSave;
        BtnAddSpell.ButtonUp += onClickBtnAddSpell;
        BtnAddSkin.ButtonUp += onClickBtnAddSkin;
        BtnAddPassive.ButtonUp += onClickBtnAddPassive;
        //Spells.onAddBtnClick += Spells_onAddBtnClick;
        //this.creature.GetEventBus().subscribe(Spells); //.baseSpells.sub
    }


    private void onClickBtnSave()
    {
        creature.GetEntityBus().publish(IEventBus.save, creature);
        creature.GetBaseStats().GetEntityBus().publish(IEventBus.save, creature.GetBaseStats());
        // VaporeonSignals.save
        //Vaporeon.bus.publish(IEventBus.save, creature);
        //Vaporeon.bus.publish(IEventBus.save, creature.GetBaseStats());
    }
    public void init(ICreatureModel model)
    {
        unload();
        creature = model;
        StatsEditor.init(model.GetBaseStats());
        load();
    }
    private void unload()
    {
        if (creature == null) return;
        // unsub vaporeon
        creature.GetEntityBus().unsubscribe(this.GetVaporeon(), IEventBus.save);
        creature.GetBaseStats().GetEntityBus().unsubscribe(this.GetVaporeon(), IEventBus.save);
        // unsub this
        creature.GetEntityBus().unsubscribe(this);
        creature.GetName().GetEntityBus().unsubscribe(this);
        creature.GetDescription().GetEntityBus().unsubscribe(this);
    }
    private void load()
    {
        // sub vaporeon for save event
        creature.GetEntityBus().subscribe(this.GetVaporeon(), IEventBus.save);
        creature.GetBaseStats().GetEntityBus().subscribe(this.GetVaporeon(), IEventBus.save);
        // sub this
        creature.GetEntityBus().subscribe(this);
        creature.GetName().GetEntityBus().subscribe(this, nameof(onNameChanged));
        creature.GetDescription().GetEntityBus().subscribe(this, nameof(onDescChanged));
        // load everything into ui
        this.EntityID.Text = $"#{creature.entityUid}";
        this.NameEdit.Text = creature.GetName().ToString();
        this.DescriptionEdit.Text = creature.GetDescription().ToString();

        // Spells
        foreach(var s in creature.baseSpells.Select(s => Eevee.models.spellModels.Get(s)))
            addSpellToList(s);
        // Passives
        foreach (var s in creature.baseStatusPassives.Select(s => Eevee.models.statusModels.Get(s)))
            addPassiveToList(s);
        // Skins
        foreach (var s in creature.skins.Select(s => Eevee.models.creatureSkins.Get(s)))
            addSkinToList(s);
    }

    #endregion


    #region Main bar
    [Subscribe]
    public void onNameChanged(IStringEntity str)
    {
        int col = NameEdit.CaretColumn;
        NameEdit.Text = str.ToString();
        NameEdit.CaretColumn = col;
    }
    [Subscribe]
    public void onDescChanged(IStringEntity str)
    {
        int col = DescriptionEdit.CaretColumn;
        DescriptionEdit.Text = str.ToString();
        DescriptionEdit.CaretColumn = col;
    }
    #endregion


    #region Spells
    private void onClickBtnAddSpell()
    {
        this.GetVaporeon().SpellsList.selectorForControl = this;
        this.GetVaporeon().TabContainer.CurrentTab = (int) VaporeonTab.Spells;
        this.GetVaporeon().GetWindow().GrabFocus();
        //this.GetVaporeon().GetWindow().AlwaysOnTop = true;
        //this.GetVaporeon().GetWindow().AlwaysOnTop = false;
    }
    [Subscribe(VaporeonSignals.select)]
    public void onAddSelectSpell(Control selector, ISpellModel model)
    {
        if (selector != this) return;
        bool added = this.creature.baseSpells.Add(model.entityUid);
        if(added) addSpellToList(model); // tood: remove this, make an eventful hashset/list
    }
    public void addSpellToList(ISpellModel model)
    {
        if (model == null) return;
        var box = createRow(model.entityUid, model.GetName().ToString(),
            () => this.GetVaporeon().openEditor(model),
            () => this.creature.baseSpells.Remove(model.entityUid)
        );
        this.SpellsList.AddChild(box);
    }
    #endregion

    #region Passives
    private void onClickBtnAddPassive()
    {
        this.GetVaporeon().StatusList.selectorForControl = this;
        this.GetVaporeon().TabContainer.CurrentTab = (int) VaporeonTab.Status;
        this.GetVaporeon().GetWindow().GrabFocus();
    }
    [Subscribe(VaporeonSignals.select)]
    public void onAddSelectStatus(Control selector, IStatusModel model)
    {
        if (selector != this) return;
        bool added = this.creature.baseStatusPassives.Add(model.entityUid);
        if (added) addPassiveToList(model); // todo: remove this, make an eventful hashset/list
    }
    public void addPassiveToList(IStatusModel model)
    {
        if (model == null) return;
        var box = createRow(model.entityUid, $"{model.entityUid}",
            () => this.GetVaporeon().openEditor(model),
            () => this.creature.baseStatusPassives.Remove(model.entityUid)
        );
        this.SpellsList.AddChild(box);
    }
    #endregion

    #region Skins
    private void onClickBtnAddSkin()
    {
        this.GetVaporeon().CreatureSkinsList.selectorForControl = this;
        this.GetVaporeon().TabContainer.CurrentTab = (int) VaporeonTab.CreatureSkins;
        this.GetVaporeon().GetWindow().GrabFocus();
    }
    [Subscribe(VaporeonSignals.select)]
    public void onAddSelectSkin(Control selector, ICreatureSkin model)
    {
        if (selector != this) return;
        bool added = this.creature.skins.Add(model.entityUid);
        if (added) addSkinToList(model); // todo: remove this, make an eventful hashset/list
    }
    public void addSkinToList(ICreatureSkin model)
    {
        if (model == null) return;
        //this.SkinsList.AddItem($"{model.entityUid} - {model.GetName()} - {model.GetDescription()}");
    }
    #endregion

    private HBoxContainer createRow(IID id, string label, Action onEdit, Action onDelete)
    {
        var box = new HBoxContainer();
        box.SetMeta(VaporeonUtil.metaIID, id.ToString());

        var btnEdit = new Button();
        btnEdit.Text = "Edit";
        btnEdit.ButtonUp += onEdit;

        var btnRemove = new Button();
        btnRemove.Text = "Remove";
        btnRemove.ButtonUp += //onDelete;
        () =>
        {
            onDelete();
            box.QueueFree(); // need to do this here until we have an EntityHashSet / EntityList
        };

        var lbl = new Label();
        lbl.Text = label;

        box.AddChild(btnEdit);
        box.AddChild(lbl);
        box.AddChild(btnRemove);
        return box;
    }


}
