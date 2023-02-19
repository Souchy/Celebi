using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon.common;

public partial class StatsEditor : PanelContainer
{
    
    private IStats _stats { get; set; }


    private Dictionary<StatCategory, GridContainer> containers;
    #region Nodes 
    [NodePath] public GridContainer ResourceContainer { get; set; }
    [NodePath] public GridContainer AffinityContainer { get; set; }
    [NodePath] public GridContainer ResistanceContainer { get; set; }
    [NodePath] public GridContainer StateContainer { get; set; }
    [NodePath] public GridContainer OtherContainer { get; set; }
    #endregion

    #region Init
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        this.GetVaporeon().bus.subscribe(this);

        containers = new() {
            { StatCategory.Resource, ResourceContainer }, 
            { StatCategory.Affinity, AffinityContainer }, 
            { StatCategory.Resistance, ResistanceContainer }, 
            { StatCategory.State, StateContainer }, 
            { StatCategory.Other, OtherContainer },
        };
    }
    public void init(IStats stats)
    {
        unload();
        _stats = stats;
        load();
    }
    private void unload()
    {
        _stats?.GetEntityBus().unsubscribe(this);
        foreach (var container in containers.Values)
            container.RemoveAndQueueFreeChildren();
    }
    private void load()
    {
        _stats.GetEntityBus().subscribe(this);

        // create stats widgets by category
        foreach (var category in Enum.GetValues<StatCategory>())
        {
            var sts = Enum.GetValues<StatType>().Where(st => st.GetProperties().category == category);
            foreach (var st in sts)
            {
                var stat = _stats.Get(st);
                PropertiesComponent.GenerateStat(containers[category], st, stat);
            }
        }
    }
    #endregion


    #region Diamond Handlers
    [Subscribe]
    public void onStatChange(IStat stat)
    {
        GD.Print($"StatsEditor.onStatChange: {stat.statId} = {stat}");
        _stats.GetEntityBus().publish(IEventBus.save, _stats);
    }
    #endregion


    #region GUI Handlers
    #endregion

}
