using Godot;
using System;


namespace Umbreon.common
{
    public partial class Umbreon : Node
    {
	    // Called when the node enters the scene tree for the first time.
	    public override void _Ready()
	    {
            GD.Print("Umbreon node");
	    }

	    // Called every frame. 'delta' is the elapsed time since the previous frame.
	    public override void _Process(double delta)
	    {
	    }
    }
}
