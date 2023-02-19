using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.factories;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.statuses;
using System;

public partial class CreatureInstanceEditor : PanelContainer
{

    private ICreature creature { get; set; }

    #region
    [NodePath] public Label LblName { get; set; }
    [NodePath] public OptionButton BtnModel { get; set; }
    [NodePath] public SpinBox PosX { get; set; }
    [NodePath] public SpinBox PosZ { get; set; }
    [NodePath] public SpinBox PosY { get; set; }
    [NodePath] public StatsEditor StatsEditor { get; set; }
    [NodePath] public Button BtnSpells { get; set; }
    [NodePath] public VBoxContainer Spells { get; set; }
    [NodePath] public Button BtnStatuses { get; set; }
    [NodePath] public VBoxContainer Statuses { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        BtnModel.ItemSelected += BtnModel_ItemSelected;
        foreach(var model in Eevee.models.creatureModels.Values)
            BtnModel.AddItem(model.GetName().value);
	}

    public void init(ICreature inst)
    {
        unload();
        creature = inst;
        load();
    }
    private void unload()
    {
        if (creature == null) return;
        creature = null;
    }
    private void load()
    {
        if (creature == null) return;
        // model
        var list = Eevee.models.creatureModels.Values.ToList();
        var creamodel = Eevee.models.creatureModels.Get(creature.modelUid);
        var idx = list.IndexOf(creamodel);
        BtnModel.Select(idx);
        // name
        LblName.Text = creamodel.GetName().value;
        // pos
        PosX.Value = creature.position.x;
        PosZ.Value = creature.position.z;
        PosY.Value = creature.position.y;
        // spells
        foreach (var s in creature.GetSpells())
        {
            var lbl = new Label();
            lbl.Text = "no model for spell #" + s.entityUid;
            if (s.modelUid != default)
                lbl.Text = Eevee.models.spellModels.Get(s.modelUid).GetName().value;
            Statuses.AddChild(lbl);
        }
        // statuses
        foreach (var s in creature.GetStatuses())
        {
            var lbl = new Label();
            lbl.Text = "no model or source for status #" + s.entityUid;
            if (s.modelUid != default)
                lbl.Text = Eevee.models.statusModels.Get(s.modelUid).GetName().value;
            if (s.sourceSpellModel != default)
                lbl.Text = Eevee.models.spellModels.Get(s.sourceSpellModel).GetName().value;
            Statuses.AddChild(lbl);
        }
    }

    private void BtnModel_ItemSelected(long index)
    {
        var list = Eevee.models.creatureModels.Values.ToList();
        var model = list[(int) index];
        var crea = Factories.newCreatureFromModel(model, this.GetVaporeon().fightId);
        crea.position.set(creature.position);
    }


}
