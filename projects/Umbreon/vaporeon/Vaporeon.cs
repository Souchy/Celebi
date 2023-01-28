using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using System;
using Umbreon.common;
using Umbreon.data.resources;
using Umbreon.src;

public static class VaporeonExtensions
{
    public static Vaporeon GetVaporeon(this Node node)
    {
        var nodePath = "/root/Vaporeon";
        var vaporeon = node.GetNode<Vaporeon>(nodePath);
        return vaporeon;
    }
}

public partial class Vaporeon : Control
{
    #region Vaporeon Properties
    //[Inject]
    //public IUIdGenerator uIdGenerator { get; set; }
    /// <summary>
    /// Object in memory to copy/paste
    /// </summary>
    public object CurrentObjectCopied { get; set; }
    #endregion

    #region Vaporeon Tabs
    /// <summary>
    /// Current Creature Editor Tab
    /// </summary>
    public ICreatureModel CurrentCreatureModel { get; set; }
    public ISpellModel CurrentSpellModel { get; set; }
    public IEffect CurrentEffect { get; set; }

    [NodePath("TabContainer/CreaturesList")]
    public ResourceList CreaturesList { get; set; }
    [NodePath("TabContainer/SpellsList")]
    public ResourceList SpellsList { get; set; }
    [NodePath("TabContainer/EffectsList")]
    public ResourceList EffectsList { get; set; }
    #endregion

    public static readonly List<Type> effectTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(p => typeof(IEffect).IsAssignableFrom(p))
        .ToList();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        this.Inject();
    }

}
