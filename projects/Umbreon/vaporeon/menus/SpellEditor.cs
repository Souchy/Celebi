using Godot;
using Godot.Sharp.Extras;
using System;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.models;
using Umbreon.vaporeon.common;

public partial class SpellEditor : Control
{

    public readonly Type spellModelType = typeof(ISpellModel);
    public readonly Type spellPropertiesType = typeof(ISpellProperties);
    // 
    public ISpellModel spell;


    [NodePath]
    public LineEdit ModelID { get; set; }
    [NodePath]
    public LineEdit Name { get; set; }
    [NodePath("./CostsBox/CostsGrid")]
    public GridContainer CostsGrid { get; set; }
    [NodePath("./PropertiesBox/PropertiesGrid")]
    public GridContainer PropertiesGrid {get; set;}



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();

		// StatType
		// CostsGrid.AddChild();
        foreach(ResourceType res in Enum.GetValues(typeof(ResourceType)))
        {
            var lbl = new Label();
            lbl.Text = Enum.GetName(typeof(ResourceType), res);
            var edit = new SpinBox();
            edit.Value = spell.costs[res];
            edit.ValueChanged += (value) => spell.costs[res] = (int) value;
            CostsGrid.AddChild(lbl);
            CostsGrid.AddChild(edit);
        }

        PropertiesGrid.QueueFreeChildren();
        PropertiesComponent.GenerateGrid(PropertiesGrid, spellPropertiesType, spell.properties);

    }
    

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
