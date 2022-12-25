using Celebi.src;
using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee;
using System;
using System.Security.Cryptography.X509Certificates;

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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.OnReady();
		creaScene = GD.Load<PackedScene>("res://sapphire/nodes/Creature.tscn");

		createCreature("ybot/ybot_pro_magic", true, new Vector3(7, 0, 7));
		createCreature("aurelia/aurelia", true, new Vector3(5, 0, 7));

		createCreature("ybot/ybot_packed", true, new Vector3(14, 0, 9));
		createCreature("ybot/ybot_pro_magic", true, new Vector3(12, 0, 11));
		createCreature("aurelia/aurelia", true, new Vector3(14, 0, 11));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void createCreature(string name, bool spawnOnBoard, Vector3 pos)
	{
		var crea = creaScene.Instantiate<CreatureNode>();
		crea.Name = name;
		crea.Position = pos.toGodot();
		crea.init(name);
		Creatures.AddChild(crea);
	}

    public static int mapLengthX = 19;
    public static int mapLengthZ = 15;

}


public static class Vector3Extensions
{
    public static Vector3 toGodot(this Vector3 fromBoard)
    {
        return new Vector3(fromBoard.x - (Sapphire.mapLengthX - 1) / 2, fromBoard.y, fromBoard.z - (Sapphire.mapLengthZ - 1) / 2);
    }   
    public static Vector3 fromGodot(this Vector3 fromGodot)
    {
        return new Vector3(fromGodot.x + (Sapphire.mapLengthX - 1) / 2, fromGodot.y, fromGodot.z + (Sapphire.mapLengthZ - 1) / 2);
    }



} 
