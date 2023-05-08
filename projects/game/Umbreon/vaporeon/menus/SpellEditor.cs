using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.umbreon.vaporeon.common;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.objects.effects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.umbreon.vaporeon.components;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using Resource = souchy.celebi.eevee.enums.characteristics.creature.Resource;

public partial class SpellEditor : Control, EditorInitiator<ISpellModel>, IEffectNodesContainer
{
    private ISpellModel spell { get; set; }


    #region Impl EffectContainer
    public IEntityList<IID> parentList { get; private set; } = null;
    public IEntityList<IID> GetEffectIds() => this.spell.effectIds;
    public IEnumerable<IEffect> GetEffectsEnum() => this.spell.GetEffects();
    public Control GetContainer() => this.EffectsChildren;
    #endregion

    #region Nodes - Main bar 
    [NodePath] public Button BtnSave { get; set; }
    [NodePath] public Label EntityID { get; set; }
    [NodePath] public LineEdit NameEdit { get; set; }
    [NodePath] public LineEdit DescriptionEdit { get; set; }
    #endregion

    #region Nodes
    [NodePath] public Button BtnCosts { get; set; }
    [NodePath] public GridContainer CostsGrid { get; set; }
    [NodePath] public Button BtnProperties { get; set; }
    [NodePath] public GridContainer PropertiesGrid { get; set; }
    [NodePath] public Button BtnRange { get; set; }
    [NodePath] public VBoxContainer Range { get; set; }
    [NodePath] public ZoneEditorMini ZoneEditorMiniMin { get; set; }
    [NodePath] public ZoneEditorMini ZoneEditorMiniMax { get; set; }
    [NodePath] public Button BtnConditions { get; set; }
    [NodePath] public VBoxContainer Conditions { get; set; }
    [NodePath] public SmallResourceTree SourceConditionsTree { get; set; }
    [NodePath] public SmallResourceTree TargetConditionsTree { get; set; }
    #endregion

    #region Effects
    [NodePath] public Button BtnAddEffectChild { get; set; }
    [NodePath] public VBoxContainer EffectsChildren { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        // basic
        ZoneEditorMiniMin.Label.Text = "Min";
        ZoneEditorMiniMax.Label.Text = "Max";
        NameEdit.TextChanged += (txt) => spell.GetName().value = txt;
        DescriptionEdit.TextChanged += (txt) => spell.GetDescription().value = txt;
        // effects
        EffectsChildren.RemoveAndQueueFreeChildren();
        BtnAddEffectChild.ButtonUp += this.onClickAddChild; 
        // save
        BtnSave.ButtonUp += publishSave;
        // tabs
        BtnCosts.Toggled += (t) => CostsGrid.Visible = t;
        BtnProperties.Toggled += (t) => PropertiesGrid.Visible = t;
        BtnRange.Toggled += (t) => Range.Visible = t;
        BtnConditions.Toggled += (t) => Conditions.Visible = t;
    }

    #region Init
    public void init(ISpellModel spell)
    {
        unload();
        this.spell = spell;
        load();
    }

    private void unload()
    {
        if (spell == null) return;
        // un vaporeon (save event)
        spell.GetEntityBus().unsubscribe(this.GetVaporeon(), IEventBus.save);
        // unsub this
        spell.GetEntityBus().unsubscribe(this);
        spell.GetName().GetEntityBus().unsubscribe(this, nameof(onNameChanged));
        spell.GetDescription().GetEntityBus().unsubscribe(this, nameof(onDescChanged));
        spell.effectIds.GetEntityBus().unsubscribe(this);
    }
    private void load()
    {
        if (spell == null) return;
        // sub vaporeon (save event)
        spell.GetEntityBus().subscribe(this.GetVaporeon(), IEventBus.save);
        // sub this
        spell.GetEntityBus().subscribe(this);
        spell.GetName().GetEntityBus().subscribe(this, nameof(onNameChanged));
        spell.GetDescription().GetEntityBus().subscribe(this, nameof(onDescChanged));
        spell.effectIds.GetEntityBus().subscribe(this);
        // id & name & desc
        this.EntityID.Text = "#" + spell.entityUid;
        this.NameEdit.Text = spell.GetName().ToString();
        this.DescriptionEdit.Text = spell.GetDescription().ToString();
        // costs
        foreach (ResourceEnum res in Enum.GetValues<ResourceEnum>())
        {
            var characId = Resource.getKey(res, ResourceProperty.Current).ID;
            // lbl
            var lbl = new Label();
            lbl.Text = Enum.GetName(res);
            // edit
            var edit = new SpinBox();
            edit.Value = spell.costs[characId];
            edit.ValueChanged += (value) => spell.costs[characId] = (int) value;
            // add nodes
            CostsGrid.AddChild(lbl);
            CostsGrid.AddChild(edit);
        }
        // props
        PropertiesGrid.QueueFreeChildren();
        PropertiesComponent.GenerateGrid(spell.properties, PropertiesGrid);
        // effects
        this.fillEffects();
        // zones
        ZoneEditorMiniMin.init(spell.RangeZoneMin);
        ZoneEditorMiniMax.init(spell.RangeZoneMax);
        // TODO conditions
        // ...
    }
    #endregion


    #region GUI Handlers
    #endregion

    #region Diamond Handlers
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

    public void publishSave()
    {
        spell.GetEntityBus().publish(IEventBus.save, spell);
    }

}
