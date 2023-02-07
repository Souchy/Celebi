using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using Umbreon.vaporeon.common;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.shared.effects;
using souchy.celebi.eevee.face.util;
using Umbreon.vaporeon.components;

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
    [NodePath] public GridContainer CostsGrid { get; set; }
    [NodePath] public GridContainer PropertiesGrid { get; set; }
    [NodePath] public ZoneEditorMini ZoneEditorMiniMin { get; set; }
    [NodePath] public ZoneEditorMini ZoneEditorMiniMax { get; set; }
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
        EffectsChildren.QueueFreeChildren();
        BtnSave.ButtonUp += onClickSave;
        BtnAddEffectChild.ButtonUp += this.onClickAddChild; //onClickAddEffectChild;
        NameEdit.TextChanged += (txt) => spell.GetName().value = txt;
        DescriptionEdit.TextChanged += (txt) => spell.GetDescription().value = txt;
        ZoneEditorMiniMin.Label.Text = "Min";
        ZoneEditorMiniMax.Label.Text = "Max";
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
        spell.GetEntityBus().unsubscribe(this, IEventBus.save);
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
        spell.GetEntityBus().subscribe(this, IEventBus.save);
        // sub this
        spell.GetEntityBus().subscribe(this);
        spell.GetName().GetEntityBus().subscribe(this, nameof(onNameChanged));
        spell.GetDescription().GetEntityBus().subscribe(this, nameof(onDescChanged));
        spell.effectIds.GetEntityBus().subscribe(this);
        // id & name & desc
        this.EntityID.Text = spell.entityUid;
        this.NameEdit.Text = spell.GetName().ToString();
        this.DescriptionEdit.Text = spell.GetDescription().ToString();
        // props
        PropertiesGrid.QueueFreeChildren();
        PropertiesComponent.GenerateGrid(spell.properties, PropertiesGrid);
        // costs
        foreach (ResourceType res in Enum.GetValues(typeof(ResourceType)))
        {
            // lbl
            var lbl = new Label();
            lbl.Text = Enum.GetName(typeof(ResourceType), res);
            // edit
            var edit = new SpinBox();
            edit.Value = spell.costs[res];
            edit.ValueChanged += (value) => spell.costs[res] = (int) value;
            // add nodes
            CostsGrid.AddChild(lbl);
            CostsGrid.AddChild(edit);
        }
        // effects
        this.fillEffects();
        // zones
        ZoneEditorMiniMin.init(spell.RangeZoneMin);
        ZoneEditorMiniMin.init(spell.RangeZoneMax);
    }
    #endregion


    #region GUI Handlers
    private void onClickSave()
    {
        spell.GetEntityBus().publish(IEventBus.save, spell);
    }
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


}
