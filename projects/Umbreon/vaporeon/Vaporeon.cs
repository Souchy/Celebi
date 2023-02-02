using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.common;
using Umbreon.data.resources;
using Umbreon.eevee.impl.objects;
using Umbreon.src;
using Umbreon.vaporeon;

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
    public readonly IEventBus bus = new EventBus();
    /// <summary>
    /// Object in memory to copy/paste
    /// </summary>
    public object CurrentObjectCopied { get; set; }
    public static readonly List<Type> effectTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(p => typeof(IEffect).IsAssignableFrom(p))
        .ToList();

    #endregion

    #region Vaporeon Editor Tabs
    private ICreatureModel _creatureModel;
    private ISpellModel _spellModel;
    private IEffect _effect;
    public ICreatureModel CurrentCreatureModel { 
        get => _creatureModel; 
        set {
            _creatureModel = value;
            bus.publish(nameof(CurrentCreatureModel), CurrentCreatureModel);
        } 
    }
    public ISpellModel CurrentSpellModel
    {
        get => _spellModel;
        set
        {
            _spellModel = value;
            bus.publish(nameof(CurrentSpellModel), CurrentSpellModel);
        }
    }
    public IEffect CurrentEffect
    {
        get => _effect;
        set
        {
            _effect = value;
            bus.publish(nameof(CurrentEffect), CurrentEffect);
        }
    }
    #endregion

    #region Vaporeon List Tabs
    [NodePath("TabContainer/CreaturesList")]
    public ResourceList CreaturesList { get; set; }
    [NodePath("TabContainer/SpellsList")]
    public ResourceList SpellsList { get; set; }
    [NodePath("TabContainer/EffectsList")]
    public ResourceList EffectsList { get; set; }
    #endregion


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        this.Inject();

        bus.subscribe(this.GetDiamondsParser());
    }

}
