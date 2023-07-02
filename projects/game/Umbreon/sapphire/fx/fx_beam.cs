using Godot;
using Godot.Sharp.Extras;
using System;

public partial class fx_beam : Node3D
{
    //[Export]
    public List<Vector3> points = new();

    [Export]
    public CurveXyzTexture texture { get; set; } = new CurveXyzTexture();


    [NodePath]
    public GpuParticles3D GPUParticles3D { get; set; }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
