using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.data.resources;
using Umbreon.vaporeon;

public partial class CreatureEditor : Control
{

    public ICreatureModel creature { get => this.GetVaporeon().CurrentCreatureModel; }


    #region
    [NodePath]
    public Label EntityID { get; set; }
    [NodePath]
    public LineEdit NameEdit { get; set; }
    [NodePath]
    public LineEdit DescriptionEdit { get; set; }
    [NodePath]
    public StatsEditor StatsEditor { get; set; }
    [NodePath]
    public SmallResourceTree Spells { get; set; }
    [NodePath]
    public SmallResourceTree Passives { get; set; }
    [NodePath]
    public SmallResourceTree Skins { get; set; }
    [NodePath]
    public Button BtnSave { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        this.GetVaporeon().bus.subscribe(this);

        //creature?.GetEntityBus().subscribe(this);
        //creature.GetBaseStats().GetEntityBus().subscribe(this);
        Eevee.models.i18n.GetEntityBus().subscribe(this, nameof(onNameChanged));

        BtnSave.ButtonUp += BtnSave_ButtonUp;
        Spells.onAddBtnClick += Spells_onAddBtnClick;
        //this.creature.GetEventBus().subscribe(Spells); //.baseSpells.sub
    }

    private void BtnSave_ButtonUp()
    {
        creature.GetEntityBus().publish(IEventBus.save, creature);
        creature.GetBaseStats().GetEntityBus().publish(IEventBus.save, creature.GetBaseStats());
        // VaporeonSignals.save
        //Vaporeon.bus.publish(IEventBus.save, creature);
        //Vaporeon.bus.publish(IEventBus.save, creature.GetBaseStats());
    }


    [Subscribe(nameof(Vaporeon.CurrentCreatureModel))]
    public void onModelChange(ICreatureModel model)
    {
        // load everything into ui
        creature?.GetEntityBus().subscribe(this);
        this.NameEdit.Text = model.GetName().ToString();
        this.DescriptionEdit.Text = model.GetDescription().ToString();
    }

    private void Spells_onAddBtnClick()
    {
        var spell = SpellModel.Create();
        Eevee.models.spellModels.Add(spell.entityUid, spell);
        this.creature.baseSpells.Add(spell.entityUid);
        //TODO
        var c = new Label();
        c.SetMeta("id", spell.entityUid.ToString());
        c.SetMeta("nameid", spell.nameId.ToString());
        c.Text = spell.GetName().ToString(); //Eevee.models.i18n.Get(spell.nameId);

        Spells.Tree.AddChild(c);
    }

    [Subscribe(nameof(EntityDictionary<IID, string>.Set))]
    public void onNameChanged(EntityDictionary<IID, string> dic, IID key, string val)
    {
        var child = Spells.Tree.GetChildren()
                        .Concat(Passives.Tree.GetChildren())
                        .Concat(Skins.Tree.GetChildren())
                        .FirstOrDefault(n => n.HasMeta("nameid") &&  n.GetMeta("nameid").AsString() == key);
        if (child == null) return;
        var lbl = (Label) child.GetNode("Label");
        lbl.Text = Eevee.models.i18n.Get(key).ToString();
    }

}
