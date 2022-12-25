using Celebi.src;
using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee;
using System;
using System.Xml.Linq;

public partial class CreatureNode : Node3D
{

    //[Inject]
    //public ICreature data;

    //[NodePath]
    //public Node3D Model; // MeshInstance3D
    [NodePath]
    public MeshInstance3D TeamIndicator;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        //this.Inject();

        /*
        var path = "res://assets/Aurelia.glb";
		var node = GD.Load<PackedScene>(path).Instantiate<Node3D>();
        var player = node.GetNode<AnimationPlayer>("AnimationPlayer");
        var anim = player.GetAnimation("Run");
        if(anim != null)
        {
            anim.LoopMode = Animation.LoopModeEnum.Linear;
            player.Play("Run");
        }
        this.AddChild(node);
        */

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public void init(string name)
    {
        if (!name.EndsWith(".tscn")) name += ".tscn";
        PackedScene modelScene = GD.Load<PackedScene>("res://assets/creatures/" + name);
        var model = modelScene.Instantiate<Node3D>();
        model.Name = "model";
        this.GetNode("Model").ReplaceBy(model);
        //this.Model = model;

        //GD.Load("res://data/creatures.json");

        var player = model.GetNode<AnimationPlayer>("AnimationPlayer");
        var idles = new string[] { "Armature025|mixamocom|Layer0|idle", "locomotion/idle", "idle", "Idle" };
        foreach (var idle in idles)
        {
            if(player.HasAnimation(idle))
            {
                var anim = player.GetAnimation(idle);
                anim.LoopMode = Animation.LoopModeEnum.Linear;
                player.Play(idle);
                break;
            }
        }

    }

}
