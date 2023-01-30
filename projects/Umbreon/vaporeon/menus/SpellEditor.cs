using Godot;
using Godot.Sharp.Extras;
using System;
using souchy.celebi.eevee.enums;
using Umbreon.vaporeon.common;
using Umbreon.common;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.util;

public partial class SpellEditor : Control
{

    public ISpellModel spell { get => this.GetVaporeon().CurrentSpellModel; }

    #region Nodes
    [NodePath]
    public LineEdit ModelID { get; set; }
    [NodePath]
    public LineEdit Name { get; set; }
    [NodePath("CostsBox/ScrollContainer/CostsGrid")]
    public GridContainer CostsGrid { get; set; }
    [NodePath("PropertiesBox/ScrollContainer/PropertiesGrid")]
    public GridContainer PropertiesGrid { get; set; }
    [NodePath]
    public SmallResourceTree EffectsTree { get; set; }
    [NodePath]
    public SmallResourceTree SourceConditionsTree { get; set; }
    [NodePath]
    public SmallResourceTree TargetConditionsTree { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();

        // StatType
        // CostsGrid.AddChild();
        update();
    }

    [Subscribe(nameof(Vaporeon.CurrentSpellModel))]
    public void onModelChange(ISpellModel model)
    {
        update();
    }

    public void update()
    {
        if (spell == null) return;
        // props
        PropertiesGrid.QueueFreeChildren();
        PropertiesComponent.GenerateGrid(spell.properties, PropertiesGrid);
        // costs
        foreach (ResourceType res in Enum.GetValues(typeof(ResourceType)))
        {
            var lbl = new Label();
            lbl.Text = Enum.GetName(typeof(ResourceType), res);
            var edit = new SpinBox();
            edit.Value = spell.costs[res];
            edit.ValueChanged += (value) => spell.costs[res] = (int) value;
            CostsGrid.AddChild(lbl);
            CostsGrid.AddChild(edit);
        }
        // effects
        foreach(var effectId in spell.effectIds)
        {
            IEffect effect = Eevee.models.effects.Get(effectId);
            IEffectModel effectModel = Eevee.models.effectModels.Get(effect.modelUid);
            Label lbl = new Label();
            lbl.Text = effect.entityUid + ": " + Eevee.models.i18n.Get(effectModel.nameId);
            lbl.SetMeta("id", (string) effectId);
            lbl.SetMeta("type", nameof(IEffect));
            EffectsTree.AddChild(lbl);
        }
    }
    
    public void onZoneTypeSelected()
    {
                                    
    }

}
