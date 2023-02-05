using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.common;
using Umbreon.data.resources;
using Umbreon.eevee.impl.objects;
using Umbreon.src;
using Umbreon.vaporeon;
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

public partial class Vaporeon : Control
{
    #region Const
    public IEventBus bus { get; } = new EventBus();
    #endregion

    #region Properties
    public object CurrentObjectCopied { get; set; }
    #endregion

    #region Packed Scenes
    private PackedScene ZonePreviewScene { get; } = GD.Load<PackedScene>("res://vaporeon/common/ZonePreview.tscn");
    private static Dictionary<Type, PackedScene> SceneTypes { get; } = new();
    #endregion

    #region Vaporeon List Tabs
    [NodePath] public TabContainer TabContainer { get; set; }
    [NodePath] public I18NEditor I18N { get; set; }
    [NodePath] public resource_list_creature CreaturesList { get; set; }
    [NodePath] public resource_list_spell SpellsList { get; set; }
    [NodePath] public resource_list_effect EffectsList { get; set; }
    //[NodePath] public resource_list_creature CreatureSkinsList { get; set; }
    //[NodePath] public resource_list_spell SpellSkinsList { get; set; }
    //[NodePath] public resource_list_effect EffectSkinsList { get; set; }
    #endregion

    #region Tab Buttons
    [NodePath] public Button BtnI18N { get; set; }
    [NodePath] public Button BtnCreatures { get; set; }
    [NodePath] public Button BtnSpells { get; set; }
    [NodePath] public Button BtnEffects { get; set; }
    [NodePath] public Button BtnCreatureSkins { get; set; }
    [NodePath] public Button BtnSpellSkins { get; set; }
    [NodePath] public Button BtnEffectSkins { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        this.Inject();

        SceneTypes.Add(typeof(ICreatureModel), GD.Load<PackedScene>("res://vaporeon/menus/CreatureEditor.tscn"));
        SceneTypes.Add(typeof(ISpellModel), GD.Load<PackedScene>("res://vaporeon/menus/SpellEditor.tscn"));
        SceneTypes.Add(typeof(IEffect), GD.Load<PackedScene>("res://vaporeon/menus/EffectEditor.tscn"));
        SceneTypes.Add(typeof(IZone), GD.Load<PackedScene>("res://vaporeon/common/ZoneEditor.tscn"));
        SceneTypes.Add(typeof(IStatBool), GD.Load<PackedScene>("res://vaporeon/common/stats/StatBoolEditor.tscn"));
        SceneTypes.Add(typeof(IStatDetailed), GD.Load<PackedScene>("res://vaporeon/common/stats/StatDetailedEditor.tscn"));
        SceneTypes.Add(typeof(IStatResource), GD.Load<PackedScene>("res://vaporeon/common/stats/StatResourceEditor.tscn"));
        SceneTypes.Add(typeof(IStatSimple), GD.Load<PackedScene>("res://vaporeon/common/stats/StatSimpleEditor.tscn"));

        bus.subscribe(this.GetDiamondsParser());

        TabContainer.TabChanged += TabContainer_TabChanged;
        TabContainer.CurrentTab = 1;


        BtnI18N.ButtonUp +=         () => TabContainer.CurrentTab = 0;
        BtnCreatures.ButtonUp +=    () => TabContainer.CurrentTab = 1;
        BtnSpells.ButtonUp +=       () => TabContainer.CurrentTab = 2;
        BtnEffects.ButtonUp +=      () => TabContainer.CurrentTab = 3;
        BtnCreatureSkins.ButtonUp +=() => TabContainer.CurrentTab = 4;
        BtnSpellSkins.ButtonUp +=   () => TabContainer.CurrentTab = 5;
        BtnEffectSkins.ButtonUp +=  () => TabContainer.CurrentTab = 6;

    }

    private void TabContainer_TabChanged(long tab)
    {
        if (tab != 0) CreaturesList.selectorForControl = null;
        if (tab != 1) SpellsList.selectorForControl = null;
        if (tab != 2) EffectsList.selectorForControl = null;
    }

    #region Open Editors
    public static Node instanceEditor(object model)
    {
        if (model == null) return null;
        foreach(var t in SceneTypes.Keys)
        {
            if(t.IsAssignableFrom(model.GetType()))
            {
                //var types = model.GetType().GetInterfaces();
                //var type = types.First();
                return SceneTypes[t].Instantiate();
            }
        }
        return null;
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
        //wd.GuiEmbedSubwindows = false;
        wd.InitialPosition = Window.WindowInitialPosition.CenterMainWindowScreen;
        wd.Title = root.Name;
        wd.Size = this.GetWindow().Size;
        wd.AddChild(root);
        this.AddChild(wd);
        wd.Show();
    }
    #endregion

}
