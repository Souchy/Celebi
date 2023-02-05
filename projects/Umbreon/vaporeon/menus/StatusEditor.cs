using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.shared.models;
using System;

public partial class StatusEditor : MarginContainer
{
    private IStatusModel status { get; set; }

     // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
	}

    public void init(IStatusModel model)
    {

    }

}
