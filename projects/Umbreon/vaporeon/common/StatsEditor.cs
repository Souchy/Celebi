using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon.common;

public partial class StatsEditor : MarginContainer
{
    
    public IStats stats { get => this.GetVaporeon().CurrentCreatureModel.GetBaseStats(); } 
    //public IStats stats = Stats.Create();

    #region Nodes
    [NodePath("VBoxContainer/HBoxContainer/Add")]
    public Button BtnAdd { get; set; }
    [NodePath("VBoxContainer/HBoxContainer/Remove")]
    public Button BtnRemove { get; set; }
    [NodePath("VBoxContainer/StatsContainer")]
    public GridContainer StatsContainer { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        this.GetVaporeon().bus.subscribe(this);

        BtnAdd.ButtonUp += BtnAdd_ButtonUp;
	}

    [Subscribe(nameof(Vaporeon.CurrentCreatureModel))]
    public void onModelChange(ICreatureModel model)
    {
        StatsContainer.QueueFreeChildren();
        foreach (var st in model.GetBaseStats().stats.Keys)
            PropertiesComponent.GenerateStat(StatsContainer, st);
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
