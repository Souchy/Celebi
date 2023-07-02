using Godot;
using Godot.Sharp.Extras;
using System;

public partial class fx_beam_path_advanced : Node3D
{

    [Export]
    public Vector3[] points { get; set; }
    [Export]
    public float duration { get; set; }

    [NodePath]
    public GpuParticles3D BeamParticles { get; set; }
    [NodePath]
    public GpuParticles3D MovingParticles { get; set; }
    [NodePath]
    public Path3D Path3D { get; set; }
    [NodePath]
    public AnimationPlayer AnimationPlayer { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        StandardMaterial3D asdf;
        var mat = (ParticleProcessMaterial) BeamParticles.ProcessMaterial;
        var tex = (CurveXyzTexture) mat.EmissionPointTexture;

        float time = 0;
        float increment = duration / points.Length;
        tex.CurveX.ClearPoints();
        tex.CurveY.ClearPoints();
        tex.CurveZ.ClearPoints();
        Path3D.Curve.ClearPoints();
        foreach (var point in points)
        {
            tex.CurveX.AddPoint(new(point.X, time));
            tex.CurveY.AddPoint(new(point.Y, time));
            tex.CurveZ.AddPoint(new(point.Z, time));
            Path3D.Curve.AddPoint(point);
            time += increment;
        }

        GD.Print("points: " + string.Join(", ", points.Select(p => p.ToString())));
        Console.WriteLine("");

        //AnimationPlayer.GetAnimation("pfx");
        AnimationPlayer.Play("pfx");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
