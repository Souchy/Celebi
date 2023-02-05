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

public partial class StatsEditor : Panel
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

        //BtnAdd.ButtonUp += BtnAdd_ButtonUp;
    }
    public void init(IStats stats)
    {
        _stats?.GetEntityBus().unsubscribe(this);

        _stats = stats;

        _stats.GetEntityBus().subscribe(this);
        //StatsContainer.QueueFreeChildren();
        foreach (var container in containers.Values)
            container.QueueFreeChildren();

        var asdf = Enum.GetValues<StatType>().Where(st => !_stats.Has(st));
        //GD.Print($"asdf: { string.Join(", ", asdf.Select(e => Enum.GetName(e))) }");

        // add missing stats just in case
        foreach (var statType in Enum.GetValues<StatType>().Where(st => !_stats.Has(st)))
        {
            var stat = statType.Create();
            //GD.Print($"Want to add {Enum.GetName(statType)} = {stat.statId}, has: {_stats.Has(statType)}");
            _stats.Add(stat);
        }

        foreach(var category in Enum.GetValues<StatCategory>())
        {
            var sts = Enum.GetValues<StatType>().Where(st => st.GetProperties().category == category);
            foreach(var st in sts)
            {
                var stat = _stats.Get(st);
                //GD.Print($"foreach st: {st} = {stat} at {stat.entityUid} with bus {stat.GetEntityBus()}");
                PropertiesComponent.GenerateStat(containers[category], st, stat);
            }
        }
        //stats.stats.ForEach((k, v) => 
        //    PropertiesComponent.GenerateStat(StatsContainer, k, v)
        //);
    }
    #endregion


    #region Diamond Handlers
    //[Subscribe("Add", "Set")]
    //public void onStatAddSet(IEntityDictionary<StatType, IStat> dic, StatType key, IStat value)
    //{
    //    var cat = key.GetProperties().category;
    //    var container = containers[cat];
    //    PropertiesComponent.GenerateStat(container, key, value);
    //}
    //[Subscribe("Remove")]
    //public void onStatRemove(IEntityDictionary<StatType, IStat> dic, StatType key, IStat value)
    //{
    //    var cat = key.GetProperties().category;
    //    var container = containers[cat];
    //    container.GetNode("lbl:" + Enum.GetName(key)).QueueFree();
    //    container.GetNode(Enum.GetName(key)).QueueFree();
    //}
    [Subscribe]
    public void onStatChange(IStat stat)
    {
        GD.Print($"StatsEditor.onStatChange: {stat.statId} = {stat}");
        _stats.GetEntityBus().publish(IEventBus.save, _stats);
    }
    #endregion


    #region GUI Handlers
    private void BtnAdd_ButtonUp()
    {
        if (true) return;
        var pop = new PopupMenu();
        //pop.Position
        pop.InitialPosition = Window.WindowInitialPosition.Absolute;
        // list stats that we dont already have
        foreach (var statType in Enum.GetValues<StatType>().Where(st => !_stats.Has(st)))
            pop.AddItem(Enum.GetName(statType));
        // on choose
        pop.IndexPressed += (index) =>
        {
            var st = Enum.GetValues<StatType>()[(int) index];
            var stat = st.Create();
            stat.GetEntityBus().subscribe(this);
            _stats.Add(stat);
            //PropertiesComponent.GenerateStat(StatsContainer, st);
        };
        pop.Show();
        this.AddChild(pop);
    }
    #endregion

}
