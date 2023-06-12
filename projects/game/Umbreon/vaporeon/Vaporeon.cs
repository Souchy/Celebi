
using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.umbreon.common;
using souchy.celebi.umbreon.common.persistance;
using souchy.celebi.umbreon.src;
using souchy.celebi.umbreon.vaporeon.common;

public static class VaporeonExtensions
{
    public static Vaporeon GetVaporeon(this Node node)
    {
        var nodePath = "/root/Vaporeon";
        var vaporeon = node.GetNode<Vaporeon>(nodePath);
        return vaporeon;
    }
}

public enum VaporeonTab
{
    I18n,
    Creatures,
    Spells,
    Effects,
    Status,
    //
    CreatureSkins,
    SpellSkins,
    EffectSkins,
    //
    Current1,
    Current2,
    Current3,
}

public partial class Vaporeon : Control
{
    #region Const
    public IEventBus bus { get; } = new EventBus();
    #endregion

    #region Properties
    public IID fightId { get; set; } = Eevee.RegisterIID<IFight>();
    public object CurrentObjectCopied { get; set; }
    #endregion

    #region Packed Scenes
    private static Dictionary<Type, PackedScene> SceneTypes { get; } = new();
    #endregion

    #region Vaporeon List Tabs
    [NodePath] public TabContainer TabContainer { get; set; }
    [NodePath] public I18NEditor I18N { get; set; }
    [NodePath] public resource_list_creature CreaturesList { get; set; }
    [NodePath] public resource_list_spell SpellsList { get; set; }
    [NodePath] public resource_list_effect EffectsList { get; set; }
    [NodePath] public resource_list_status StatusList { get; set; }
    // todo
    [NodePath] public resource_list_creature_skin CreatureSkinsList { get; set; }
    [NodePath] public resource_list_spell_skin SpellSkinsList { get; set; }
    [NodePath] public resource_list_effect_skin EffectSkinsList { get; set; }
    // simuroom
    //
    
    #endregion

    #region Tab Buttons
    [NodePath] public Button BtnI18N { get; set; }
    [NodePath] public Button BtnCreatures { get; set; }
    [NodePath] public Button BtnSpells { get; set; }
    [NodePath] public Button BtnEffects { get; set; }
    [NodePath] public Button BtnStatus { get; set; }
    [NodePath] public Button BtnCreatureSkins { get; set; }
    [NodePath] public Button BtnSpellSkins { get; set; }
    [NodePath] public Button BtnEffectSkins { get; set; }
    //
    [NodePath] public Button BtnSimuroom { get; set; }
    //
    [NodePath] public Button BtnCurrent1 { get; set; }
    [NodePath] public Button BtnCurrent2 { get; set; }
    [NodePath] public Button BtnCurrent3 { get; set; }
    #endregion

    private readonly Simuroom simuroom = instanceScene<Simuroom>();

    static Vaporeon()
    {
        SceneTypes.Add(typeof(ICreatureModel), GD.Load<PackedScene>("res://vaporeon/menus/CreatureEditor.tscn"));
        SceneTypes.Add(typeof(ISpellModel), GD.Load<PackedScene>("res://vaporeon/menus/SpellEditor.tscn"));
        SceneTypes.Add(typeof(IEffect), GD.Load<PackedScene>("res://vaporeon/menus/EffectEditor.tscn"));
        SceneTypes.Add(typeof(IStatusModel), GD.Load<PackedScene>("res://vaporeon/menus/StatusEditor.tscn"));
        SceneTypes.Add(typeof(IZone), GD.Load<PackedScene>("res://vaporeon/common/zones/ZoneEditor.tscn"));
        SceneTypes.Add(typeof(IStatBool), GD.Load<PackedScene>("res://vaporeon/common/stats/StatBoolEditor.tscn"));
        SceneTypes.Add(typeof(IStatDetailed), GD.Load<PackedScene>("res://vaporeon/common/stats/StatDetailedEditor.tscn"));
        SceneTypes.Add(typeof(IStatResource), GD.Load<PackedScene>("res://vaporeon/common/stats/StatResourceEditor.tscn"));
        SceneTypes.Add(typeof(IStatSimple), GD.Load<PackedScene>("res://vaporeon/common/stats/StatSimpleEditor.tscn"));
        SceneTypes.Add(typeof(EffectMini), GD.Load<PackedScene>("res://vaporeon/common/EffectMini.tscn"));
        SceneTypes.Add(typeof(Simuroom), GD.Load<PackedScene>("res://vaporeon/simuroom/Simuroom.tscn"));
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        //this.Inject();

        bus.subscribe(this.GetDiamondsParser());

        TabContainer.TabChanged += TabContainer_TabChanged;
        TabContainer.CurrentTab = (int) VaporeonTab.Creatures;

        GD.Print($"CurrentTab: {TabContainer.CurrentTab}, supposed to be 1 = {(int) VaporeonTab.Creatures}");
        BtnI18N.ButtonUp +=         () => TabContainer.CurrentTab = (int) VaporeonTab.I18n;
        BtnCreatures.ButtonUp +=    () => TabContainer.CurrentTab = (int) VaporeonTab.Creatures;
        BtnSpells.ButtonUp +=       () => TabContainer.CurrentTab = (int) VaporeonTab.Spells;
        BtnEffects.ButtonUp +=      () => TabContainer.CurrentTab = (int) VaporeonTab.Effects;
        BtnStatus.ButtonUp +=       () => TabContainer.CurrentTab = (int) VaporeonTab.Status;
        BtnCreatureSkins.ButtonUp +=() => TabContainer.CurrentTab = (int) VaporeonTab.CreatureSkins;
        BtnSpellSkins.ButtonUp +=   () => TabContainer.CurrentTab = (int) VaporeonTab.SpellSkins;
        BtnEffectSkins.ButtonUp +=  () => TabContainer.CurrentTab = (int) VaporeonTab.EffectSkins;
        BtnCurrent1.ButtonUp +=     () => TabContainer.CurrentTab = (int) VaporeonTab.Current1;
        BtnCurrent2.ButtonUp +=     () => TabContainer.CurrentTab = (int) VaporeonTab.Current2;
        BtnCurrent3.ButtonUp +=     () => TabContainer.CurrentTab = (int) VaporeonTab.Current3;

        BtnSimuroom.ButtonUp += () =>
        {
            newWindow(simuroom);
        };
    }

    private void TabContainer_TabChanged(long tab)
    {
        if (tab != (int) VaporeonTab.Creatures) 
            CreaturesList.selectorForControl = null;
        if (tab != (int) VaporeonTab.Spells) 
            SpellsList.selectorForControl = null;
        if (tab != (int) VaporeonTab.Effects) 
            EffectsList.selectorForControl = null;
    }

    #region Save handler
    // FIXME i dont think we ever go here? and if we do, we might be proccing it twice because of "bus.subscribe(this.GetDiamondsParser());"
    [Subscribe("", IEventBus.save)]
    public void onSave(IEntity e) => this.GetDiamondsParser().persistance.onSave(e);
    #endregion

    #region Open Editors
    public static Control instanceEditor(object model)
    {
        if (model == null) return null;
        foreach(var t in SceneTypes.Keys)
            if(t.IsAssignableFrom(model.GetType()))
                return (Control) SceneTypes[t].Instantiate();
        return null;
    }
    public static T instanceScene<T>() where T : Node
    {
        GD.Print($"instanceScene <{typeof(T)}> in {SceneTypes}");
        return SceneTypes[typeof(T)].Instantiate<T>();
    }
    public void openEditor<T>(T model)
    {
        if (model == null) return;
        EditorInitiator<T> editor = SceneTypes[typeof(T)].Instantiate<EditorInitiator<T>>();
        newWindow((Control) editor);
        editor.init(model);
    }
    public void newWindow(Node root)
    {
        var wd = new Window();
        wd.GuiEmbedSubwindows = false;
        wd.InitialPosition = Window.WindowInitialPosition.CenterMainWindowScreen;
        wd.Title = root.Name;
        wd.Size = this.GetWindow().Size;
        wd.AddChild(root);
        wd.CloseRequested += () => wd.QueueFree();
        this.AddChild(wd);
        wd.Show();
    }
    #endregion

}