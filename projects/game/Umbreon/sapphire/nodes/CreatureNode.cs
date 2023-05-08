using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.impl.util;
using Resource = souchy.celebi.eevee.enums.characteristics.creature.Resource;

public partial class CreatureNode : Node3D
{
    public Creature Creature { get; set; }

    #region Nodes
    [NodePath] public Node3D Model { get; set; } 
    [NodePath] public MeshInstance3D TeamIndicator { get; set; }
    [NodePath] public AnimationPlayer AnimationPlayer { get; set; }
    [NodePath] public Healthbar Healthbar { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
    }

    [Subscribe(nameof(Resource.Life))]
    public void onLifeChanged(IStatResource life)
    {
        Healthbar.set(life.current, life.currentMax);
    }

    public void init(ICreature c, ICreatureSkin skin, int team) //string name)
    {
        // Sub
        c.GetStats(null).Get<IStatResource>(Resource.Life).GetEntityBus().subscribe(this);

        // Set Model
        GD.Print("CreatureNode_init: " + skin.meshModel);
        if (!skin.meshModel.EndsWith(".tscn")) skin.meshModel += ".tscn";
        PackedScene modelScene = GD.Load<PackedScene>("res://assets/creatures/" + skin.meshModel);
        var characterNode = modelScene.Instantiate<Node3D>();
        characterNode.Name = "model";
        var previousModel = this.GetNode("Model");
        previousModel.ReplaceBy(characterNode);
        
        // Set Color
        var color = getTeamColor(team);

        // Set Model Material Color
        /*
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
        */

        // Set TeamIndicator Material Color
        //TeamIndicator = (MeshInstance3D) this.GetNode("TeamIndicator");
        //var teamMat = (StandardMaterial3D) TeamIndicator.MaterialOverride;
        //teamMat.AlbedoColor = color;

        // Set Idle Animation
        //var player = characterNode.GetNode<AnimationPlayer>("AnimationPlayer");
        string idle = skin.animations.idle;
        if (AnimationPlayer.HasAnimation(idle))
        {
            var anim = AnimationPlayer.GetAnimation(idle);
            anim.LoopMode = Animation.LoopModeEnum.Linear;
            AnimationPlayer.Play(idle);
        }
    }

    public Color getTeamColor(int team)
    {
        var rng = new Color(new Random().NextSingle(), new Random().NextSingle(), new Random().NextSingle());
        switch (team)
        {
            case 0: return rng;
            case 1: return rng;
            default: return rng; // this.color;
        }
    }


}
