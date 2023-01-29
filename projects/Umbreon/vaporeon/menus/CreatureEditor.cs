using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.shared.models;
using System;

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
	}

}
