using Godot;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.objects;
using System;
using Umbreon.data.resources;

public partial class Vaporeon : Control
{

    public ICreatureModel CurrentCreatureModel { get; set; }
    public ISpellModel CurrentSpellModel { get; set; }
    public IEffect CurrentEffect { get; set; }



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
