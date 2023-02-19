using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.util;
using System;

public partial class TeamEditor : PanelContainer
{

    public ITeam team { get; set; }

    #region Nodes
    [NodePath] public Button BtnTeam { get; set; }
    [NodePath] public Button BtnAddCreature { get; set; }
    [NodePath] public VBoxContainer Creatures { get; set; }
    [NodePath] public VBoxContainer Content { get; set; }
    #endregion


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        Creatures.RemoveAndQueueFreeChildren();
        BtnTeam.ButtonUp += () => Content.Visible = !Content.Visible;
        BtnAddCreature.ButtonUp += () =>
        {
            var crea = Creature.Create();

        };
	}

    public void init(ITeam team)
    {
        this.team = team;
    }

    public void load()
    {
        var creatures = new List<ICreature>();
        foreach(var c in creatures)
        {
            var hbox = new HBoxContainer();

            var btnEdit = new Button();
            btnEdit.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            btnEdit.ButtonUp += () =>
            {
                var editor = GD.Load<CreatureInstanceEditor>("res://vaporeon/simuroom/editors/CreatureInstanceEditor.tscn");
                //this.GetVaporeon().
                this.GetNode<Vaporeon>();
            };
            hbox.AddChild(btnEdit);

            var btnDelete = new Button();
            btnDelete.Text = "x";
            hbox.AddChild(btnDelete);

            this.Creatures.AddChild(hbox);
        }
    }

    [Subscribe(EntityList<ICreature>.EventAdd)]
    public void onCreatureListAdd(ICreature c)
    {

    }
    [Subscribe(EntityList<ICreature>.EventRemove)]
    public void onCreatureListRemove(ICreature c)
    {

    }

}
