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

public partial class CreatureEditor : Control
{

    public ICreatureModel creature { get => this.GetVaporeon().CurrentCreatureModel; }


    #region
    [NodePath]
    public Label EntityID { get; set; }
    [NodePath]
    public LineEdit NameInput { get; set; }
    [NodePath]
    public LineEdit DescriptionInput { get; set; }
    [NodePath]
    public StatsEditor StatsEditor { get; set; }
    [NodePath]
    public SmallResourceTree Spells { get; set; }
    [NodePath]
    public SmallResourceTree Passives { get; set; }
    [NodePath]
    public SmallResourceTree Skins { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        this.GetVaporeon().bus.subscribe(this);

        Eevee.models.i18n.GetEventBus().subscribe(this, nameof(onNameChanged));

        Spells.onAddBtnClick += Spells_onAddBtnClick;
        //this.creature.GetEventBus().subscribe(Spells); //.baseSpells.sub
    }


    [Subscribe(nameof(Vaporeon.CurrentCreatureModel))]
    public void onModelChange(ICreatureModel model)
    {
        // load everything into ui
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
        c.Text = Eevee.models.i18n.Get(spell.nameId);
        //Eevee.models.i18n.GetEventBus().subscribe("set", ((EntityDictionary<IID, string> i18n, IID key, string val) args) => {
        //    if(args.key.value == spell.nameId)
        //        c.Text = Eevee.models.i18n.Get(spell.nameId);
        //});
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
        lbl.Text = Eevee.models.i18n.Get(key);
    }

}
