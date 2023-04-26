using Godot;
using Godot.Sharp.Extras;
using System;

public partial class fx_projectile_path : Node3D
{
	//[Export]
	//public Vector3 posFrom { get; set; }
	//[Export]
 //   public Vector3 posTo { get; set; }
	[Export]
	public double duration = 1;
	[Export]
	public Vector3[] path { get; set; }

	[NodePath]
	public GpuParticles3D GPUParticles3D { get; set; }
	[NodePath]
	public AnimationPlayer AnimationPlayer { get; set; }
	[NodePath]
	public Camera3D Camera3D { get; set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.OnReady();
		//posFrom = new Vector3(0, 0, 0);
		//posTo = new Vector3(5, 3, 0);

		prepParticleEmitter();
		prepAnimation();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void prepAnimation()
	{
        //var anim = new Animation();
        var anim = AnimationPlayer.GetAnimation("pfxAnim");
		anim.LoopMode = Animation.LoopModeEnum.Linear;
		int posTrackId = anim.AddTrack(Animation.TrackType.Position3D);
		anim.TrackSetPath(posTrackId, nameof(GPUParticles3D));
        int speedTrackId = anim.AddTrack(Animation.TrackType.Value);
        anim.TrackSetPath(speedTrackId, nameof(GPUParticles3D));

        double time = 0;
		foreach(var v in this.path)
		{
			GD.Print("add point " + v);
			anim.PositionTrackInsertKey(posTrackId, time, v);
            anim.TrackInsertKey(speedTrackId, time, v);
            time += duration / path.Length;
		}

		//var lib = AnimationPlayer.GetAnimationLibrary("[Global]"); //new AnimationLibrary();
		//lib.AddAnimation("pfxAnimFromTo", anim);
		//AnimationPlayer.AddAnimationLibrary("Global", lib);

		AnimationPlayer.Play("pfxAnim");

		AnimationPlayer.Play("Path3D/PathFollow3D:progress_ratio");
	}

	private void prepParticleEmitter()
	{

	}


}
