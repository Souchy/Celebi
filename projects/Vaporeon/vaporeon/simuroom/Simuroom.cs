using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.util;
using System;

public static class SimuroomExtensions
{
    public static Simuroom GetSimuroom(this Node node)
    {
        var nodePath = "/root/Simuroom";
        var simuroom = node.GetNode<Simuroom>(nodePath);
        return simuroom;
    }
}
public partial class Simuroom : Node
{

    public ITeam team1 { get; set; } = new Team() { name = "Team 1" };
    public ITeam team2 { get; set; } = new Team() { name = "Team 2" };
    public IEntityDictionary<ITeam, IEntityList<ICreature>> teams { get; set; } = EntityDictionary<ITeam, IEntityList<ICreature>>.Create();

    #region Nodes
    [NodePath] public TeamEditor TeamEditor1 { get; set; }
    [NodePath] public TeamEditor TeamEditor2 { get; set; }
    [NodePath] public Node Creatures { get; set; }
    [NodePath] public Node Hud { get; set; }
    [NodePath] public Controls Controls { get; set; }

    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        Creatures.RemoveAndQueueFreeChildren();

        teams.Add(team1, new EntityList<ICreature>());
        teams.Add(team2, new EntityList<ICreature>());
        teams.Get(team1).GetEntityBus().subscribe(this);
        
        TeamEditor1.BtnTeam.Text = team1.name;
        TeamEditor2.BtnTeam.Text = team2.name;
        TeamEditor1.init(team1);
    }


}
