using Godot;
using Godot.Sharp.Extras;
using Newtonsoft.Json;
using souchy.celebi.eevee;
using System;
using System.Security.Cryptography.X509Certificates;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.umbreon.common.persistance;
using souchy.celebi.eevee.face.objects.controllers;

public partial class Sapphire : Node3D
{
	[NodePath]
	public Node Environment;
    [NodePath("%Map")]
    public Node Map;
	[NodePath]
	public Node Controls;
	[NodePath("%sapphireHud")]
	public Node Hud;
	[NodePath]
	public Node Creatures;

	private PackedScene creaScene;

    /// <summary>
    /// TODO
    /// </summary>
    public IFight fight { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		this.OnReady();
		creaScene = GD.Load<PackedScene>("res://sapphire/nodes/CreatureNode.tscn");

        DiamondParser diamonds = this.GetDiamondsParser();
        //DiamondModels diamonds = Eevee.models;
        GD.Print("startPositions: " + diamonds.mapModelsData[0].teamStartPositions[0].Stringify());

        Creatures.GetChildren().Clear();
        var map = diamonds.mapModelsData[0];
        for(int team = 0; team < 2; team++)
        {
            GD.Print("team: " + team);
            int i = 0;
            foreach (var model in Eevee.models.creatureModels.Values) //diamonds.creatureModelsData)
            {
                //createCreature(model, diamonds.creatureSkinsData[model.skins[0]], team, true, map.teamStartPositions[team][i]);
                var skin = Eevee.models.creatureSkins.Get(model.skinIds.Values[0]);
                createCreature(model, skin, team, true, map.teamStartPositions[team][i]);
                i++;
            }
        }
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }


    // CreatureModelData model, CreatureSkinData skin
    public void createCreature(ICreatureModel model, ICreatureSkin skin, int team, bool spawnOnBoard, Vector3 pos)
    {
        GD.Print("create pos: " + pos);
        ICreature crea = Creature.Create(fight.entityUid);
        //TODO crea.modelUid = model.id;

        var node = creaScene.Instantiate<CreatureNode>();
        node.Name = model.GetName().value;
        node.Position = pos; //.toGodot();
        node.init(crea, skin, team);
        Creatures.AddChild(node);
    }

    public static int mapLengthX = 19;
    public static int mapLengthZ = 15;

}
