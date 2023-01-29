using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.stats;
using System;
using Umbreon.vaporeon.common;

public partial class StatsEditor : MarginContainer
{

    #region Nodes

    [NodePath("VBoxContainer/HBoxContainer/Add")]
    public Button BtnAdd { get; set; }
    [NodePath("VBoxContainer/HBoxContainer/Remove")]
    public Button BtnRemove { get; set; }

    [NodePath("VBoxContainer/StatsContainer")]
    public GridContainer StatsContainer { get; set; }
    #endregion

    public IStats stats = Stats.Create();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        BtnAdd.ButtonUp += BtnAdd_ButtonUp;
	}

    private void BtnAdd_ButtonUp()
    {
        var pop = new PopupMenu();
        // list stats that we dont already have
        foreach (var statType in Enum.GetValues<StatType>().Where(st => !stats.has(st)))
            pop.AddItem(Enum.GetName(statType));
        // on choose
        pop.IndexPressed += (index) =>
        {
            var st = Enum.GetValues<StatType>()[(int) index];
            PropertiesComponent.GenerateStat(StatsContainer, st);
        };
        pop.Show();
        this.AddChild(pop);
    }
}
