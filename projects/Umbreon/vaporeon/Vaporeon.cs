
using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using Umbreon.common;
using Umbreon.src;
using Umbreon.vaporeon.common;

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
    CreatureSkins,
    SpellSkins,
    EffectSkins
}

public partial class Vaporeon : Control
{
    #region Const
    public IEventBus bus { get; } = new EventBus();
    #endregion

    #region Properties
    public object CurrentObjectCopied { get; set; }
    #endregion

    #region Packed Scenes
    private PackedScene ZonePreviewScene { get; } = GD.Load<PackedScene>("res://vaporeon/common/zones/ZonePreview.tscn");
    private static Dictionary<Type, PackedScene> SceneTypes { get; } = new();
    #endregion

    #region Vaporeon List Tabs
    [NodePath] public TabContainer TabContainer { get; set; }
    [NodePath] public I18NEditor I18N { get; set; }
    [NodePath] public resource_list_creature CreaturesList { get; set; }
    [NodePath] public resource_list_spell SpellsList { get; set; }
    [NodePath] public resource_list_effect EffectsList { get; set; }
    // todo
    [NodePath] public resource_list_status StatusList { get; set; }
    [NodePath] public resource_list_creature_skin CreatureSkinsList { get; set; }
    [NodePath] public resource_list_spell_skin SpellSkinsList { get; set; }
    [NodePath] public resource_list_effect_skin EffectSkinsList { get; set; }
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
    #endregion

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
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        this.Inject();

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
    [Subscribe("", IEventBus.save)]
    public void onSave(IEntity e) => this.GetDiamondsParser().onSave(e);
    #endregion

    #region Open Editors
    public static Node instanceEditor(object model)
    {
        if (model == null) return null;
        foreach(var t in SceneTypes.Keys)
            if(t.IsAssignableFrom(model.GetType()))
                return SceneTypes[t].Instantiate();
        return null;
    }
    public static T instanceScene<T>() where T : Node
    {
        GD.Print($"instanceScene <{typeof(T)}> in {SceneTypes.Last()}");
        return SceneTypes[typeof(T)].Instantiate<T>();
    }
    public void openEditor<T>(T model)
    {
        if (model == null) return;
        EditorInitiator<T> editor = SceneTypes[typeof(T)].Instantiate<EditorInitiator<T>>();
        newWindow((Control) editor);
        editor.init(model);
    }
    public void openZonePreview(IZone model)
    {
        if (model == null) return;
        var preview = ZonePreviewScene.Instantiate<ZonePreview>();
        newWindow(preview);
        preview.init(model);
    }
    private void newWindow(Control root)
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
