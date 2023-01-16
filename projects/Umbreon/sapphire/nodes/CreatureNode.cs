using Umbreon.data;
using Umbreon.src;
using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee;
using System;
using System.Xml.Linq;
using Umbreon.eevee.impl.objects;

public partial class CreatureNode : Node3D
{

    //[Inject]
    //public ICreature data;

    //[NodePath]
    //public Node3D Model; // MeshInstance3D
    [NodePath]
    public MeshInstance3D TeamIndicator;
    [Export]
    public Color color = new Color(0, 0, 0);

    //[Export]
    public Creature Creature { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        //this.Inject();

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public void init(CreatureSkinData skin, int team) //string name)
    {
        // Set Model
        GD.Print("CreatureNode_init: " + skin.model);
        if (!skin.model.EndsWith(".tscn")) skin.model += ".tscn";
        PackedScene modelScene = GD.Load<PackedScene>("res://assets/creatures/" + skin.model);
        var characterNode = modelScene.Instantiate<Node3D>();
        characterNode.Name = "model";
        this.GetNode("Model").ReplaceBy(characterNode);
        
        // Set Color
        var color = getTeamColor(team);

        // Set Model Material Color
        var meshNode = characterNode.FindChild(skin.meshName, true);
        if (meshNode != null)
        {
            MeshInstance3D mesh = (MeshInstance3D)meshNode;
            foreach (int matId in skin.colorMaterials)
            {
                var mat = (StandardMaterial3D) mesh.GetActiveMaterial(matId);
                mat.AlbedoColor = color;
            }
        }
        // Set TeamIndicator Material Color
        //TeamIndicator = (MeshInstance3D) this.GetNode("TeamIndicator");
        //var teamMat = (StandardMaterial3D) TeamIndicator.MaterialOverride;
        //teamMat.AlbedoColor = color;

        // Set Idle Animation
        var player = characterNode.GetNode<AnimationPlayer>("AnimationPlayer");
        string idle = skin.animations.idle;
        if (player.HasAnimation(idle))
        {
            var anim = player.GetAnimation(idle);
            anim.LoopMode = Animation.LoopModeEnum.Linear;
            player.Play(idle);
        }
    }

    public Color getTeamColor(int team)
    {
        var rng = new Color(new Random().NextSingle(), new Random().NextSingle(), new Random().NextSingle());
        switch (team)
        {
            case 0: return rng;
            case 1: return rng;
            default: return this.color;
        }
    }


}
