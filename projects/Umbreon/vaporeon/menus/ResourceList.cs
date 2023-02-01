using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.shared.effects;
using souchy.celebi.eevee.impl.util;
using System;
using System.Collections.Generic;
using System.Reflection;
using Umbreon.common;
using Umbreon.data.resources;
using Umbreon.eevee.impl.objects;
using Umbreon.src;


//public class vaporeon
//{

//    // have a project
//}

public partial class ResourceList : Control
{
    public enum ResourceListType
    {
        ICreatureModel,
        ISpellModel,
        IEffect
    }


    #region Properties
    public IListUtil util { get; set; }
    /// <summary>
    /// List IEffect, ISpellModel, ICreatureModel
    /// </summary>
    [Export(PropertyHint.Enum, "Type")]
    public ResourceListType resourceType { get; set; }
    //public Type selectedType;
    public Control selectedItem { get; set; }
    public PackedScene listItemScene = GD.Load<PackedScene>("res://vaporeon/menus/ResourceListItem.tscn");
    public StyleBoxFlat ResourceListItemNormal = (StyleBoxFlat) GD.Load("res://vaporeon/themes/ResourceListItemNormal.tres");
    public StyleBoxFlat ResourceListItemHover = (StyleBoxFlat) GD.Load("res://vaporeon/themes/ResourceListItemHover.tres");
    public StyleBoxFlat ResourceListItemSelected = (StyleBoxFlat) GD.Load("res://vaporeon/themes/ResourceListItemSelected.tres");
    public Color ColorSelected { get; } = new Color(1, 1, 1, 150f/255f);
    public Color ColorUnselected { get; } = new Color(0, 0, 0, 1);
    #endregion

    #region Nodes
    [NodePath("VBoxContainer/ScrollContainer/Container")]
    public HFlowContainer Container { get; set; }
    #endregion

    #region Toolbar Properties
    [NodePath("VBoxContainer/Toolbar/Search")]
    public LineEdit Search { get; set; }
    [NodePath("VBoxContainer/Toolbar/Search/ClearSearchBtn")]
    public Button ClearSearchBtn { get; set; }
    [NodePath("VBoxContainer/Toolbar/CreateBtn")]
    public Button CreateBtn { get; set; }
    [NodePath("VBoxContainer/Toolbar/DeleteBtn")]
    public Button DeleteBtn { get; set; }
    [NodePath("VBoxContainer/Toolbar/CreatePopupMenu")]
    public PopupMenu CreatePopupMenu { get; set; }
    #endregion



    //public delegate void OnAddItem(object obj);
    //public event OnAddItem AddItem;
    //public delegate void OnRemoveItem(object obj);
    //public event OnRemoveItem RemoveItem;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();

        //this.Container.GetChildren().Clear();
        foreach (var child in this.Container.GetChildren().ToList())
        {
            Container.RemoveChild(child);
            child.QueueFree();
        }

        if (this.resourceType == ResourceListType.ICreatureModel)
        {
            util = new CreatureListUtil(this);
            Eevee.models.creatureModels.GetEntityBus().subscribe(util);
            //Eevee.models.creatureSkins.GetEventBus().subscribe(util);
        }
        if (this.resourceType == ResourceListType.ISpellModel)
        {
            util = new SpellListUtil(this);
            Eevee.models.spellModels.GetEntityBus().subscribe(util);
            //Eevee.models.spellSkins.GetEventBus().subscribe(util);
            // fill list
        }
        if (this.resourceType == ResourceListType.IEffect)
        {
            util = new EffectListUtil(this);
            Eevee.models.effects.GetEntityBus().subscribe(util);
            //Eevee.models.effectSkins.GetEventBus().subscribe(util);
        }

        //this.GetDiamonds().Changed += util.onDiamondsChanged;
        this.CreateBtn.Pressed += util.onCreateBtn;
        this.DeleteBtn.Pressed += util.onRemoveBtn;
    }


    public void onSearchChanged()
    {
        // debounce(actualSearch)
    }
    public void onClearSearchClick()
    {

    }

    public void _on_type_list_item_selected(int index)
    {
        GD.Print("?????????????????????? " + index);
    }


    public void addChild(string name, Color col, IID meta)
    {
        // create new square with the icon, name and description
        var item = (Control) listItemScene.Instantiate();
        var image = (ColorRect) item.GetNode("PanelContainer/VBoxContainer/Image");
        image.Color = col;
        var lbl = (Label) item.GetNode("PanelContainer/VBoxContainer/Label");
        lbl.Text = name.ToString();
        //item.Name = spell.entityUid.value;
        item.SetMeta("object", meta.ToString());

        //item.MouseFilter = MouseFilterEnum.;
        item.GuiInput += (e) => onMouseClickItem(e, item); // idk but this doesnt work
        item.MouseEntered += () => onMouseEnterItem(item);
        item.MouseExited += () => onMouseExitItem(item);
        //image.GuiInput += (e) => select(e, item);
        //lbl.GuiInput += (e) => select(e, item);
        Container.AddChild(item);
    }


    public void removeChild(IID id)
    {
        Node node = Container.GetChildren().First(c => c.GetMeta("object").AsString() == id);
        Container.RemoveChild(node);
        node.QueueFree();
    }

    private StyleBoxFlat styleNormal = null;
    private StyleBoxFlat styleHover = null;

    private void onMouseEnterItem(Control item)
    {
        if (item == selectedItem) return;
        var panel = (PanelContainer) item.GetNode("PanelContainer");
        panel.Set("theme_override_styles/panel", ResourceListItemHover);
    }
    private void onMouseExitItem(Control item)
    {
        if (item == selectedItem) return;
        var panel = (PanelContainer) item.GetNode("PanelContainer");
        // copy theme and tween it
        var style = ResourceListItemHover.Duplicate(true);
        panel.Set("theme_override_styles/panel", style);
        Tween t = panel.CreateTween();
        t.Finished += () =>
        {
            if (style == (StyleBoxFlat) panel.Get("theme_override_styles/panel"))
                panel.Set("theme_override_styles/panel", ResourceListItemNormal);
        };
        t.TweenProperty(style, "shadow_color", ResourceListItemNormal.ShadowColor, 0.25f);
    }
    public void onMouseClickItem(InputEvent ev, Control item)
    {
        if (ev is InputEventMouseButton eventMouse && eventMouse.Pressed) // && selectedItem != item)
        {
            if (eventMouse.ButtonIndex == MouseButton.Right)
            {
                var pop = new PopupMenu();
                pop.AddItem("Edit");
                pop.AddItem("Copy");
                pop.IndexPressed += (index) =>
                {
                    if (index == 0) util.edit();
                    if (index == 1) util.copy();
                };
                pop.Show();
                this.AddChild(pop);
            }
            if (eventMouse.ButtonIndex != MouseButton.Left)
                return;

            if(selectedItem == item)
                util.edit();
            // remove previous selected
            if (selectedItem != null)
            {
                var selectedPanel = (PanelContainer) selectedItem.GetNode("PanelContainer");
                selectedPanel.Set("theme_override_styles/panel", ResourceListItemNormal);
            }
            // select new
            selectedItem = item;
            var panel = (PanelContainer) selectedItem.GetNode("PanelContainer");
            panel.Set("theme_override_styles/panel", ResourceListItemSelected);
        }
    }
}


public interface IListUtil
{
    public ResourceList list { get; init; }
    public void fillList();
    public void onCreateBtn();
    public void onRemoveBtn();
    public void edit();
    public void copy();
}

public interface IListUtilGeneric<T> : IListUtil
{
    public T getSelectedItem();
    public void createChildNode(T model);

    [Subscribe(nameof(IEntityDictionary<IID, T>.Add))]
    public void onDiamondAdd(IEntityDictionary<IID, T> dic, IID id, T model)
    {
        createChildNode(model);
    }
    [Subscribe(nameof(IEntityDictionary<IID, T>.Remove))]
    public void onDiamondRemove(IEntityDictionary<IID, T> dic, IID id, T model)
    {
        list.removeChild(id);
    }
    [Subscribe(nameof(IEntityDictionary<IID, T>.Set))]
    public void onDiamondSet(IEntityDictionary<IID, T> dic, IID id, T model)
    {
        list.removeChild(id);
        createChildNode(model);
    }
}


public static class fuck
{
    public static Color Random(this Color color)
    {
        var rnd = new Random();
        color.R = rnd.NextSingle();
        color.G = rnd.NextSingle();
        color.B = rnd.NextSingle();
        color.A = 1;
        return color;
    }
    public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dict,
        Func<TValue, bool> predicate)
    {
        var keys = dict.Keys.Where(k => predicate(dict[k])).ToList();
        foreach (var key in keys)
        {
            dict.Remove(key);
        }
    }
} 

public class CreatureListUtil : IListUtilGeneric<ICreatureModel>
{
    public ResourceList list { get; init; }
    public CreatureListUtil(ResourceList list) => this.list = list;
    public void fillList()
    {
        foreach (ICreatureModel creature in Eevee.models.creatureModels.Values)
            createChildNode(creature);
    }
    public void createChildNode(ICreatureModel model)
    {
        var name = model.GetName();
        var desc = model.GetDescription();
        list.addChild(name.ToString(), new Color().Random(), model.entityUid);
    }
    public void onCreateBtn()
    {
        var creatureSkin = CreatureSkin.Create(); 
        var creatureModel = CreatureModel.Create();
        creatureModel.skins.Add(creatureSkin.entityUid);

        var name = StringEntity.Create("Name for: " + creatureSkin.entityUid);
        creatureModel.nameId = name.entityUid;
        creatureSkin.nameId = name.entityUid;
        Eevee.models.i18n.Add(creatureSkin.nameId, name);

        var desc = StringEntity.Create("Desc for: " + creatureSkin.entityUid);
        creatureModel.descriptionId = desc.entityUid;
        creatureSkin.descriptionId = desc.entityUid;
        Eevee.models.i18n.Add(creatureSkin.descriptionId, desc);

        Eevee.models.creatureSkins.Add(creatureSkin.entityUid, creatureSkin);
        Eevee.models.creatureModels.Add(creatureModel.entityUid, creatureModel);
    }
    public void onRemoveBtn()
    {
        if (list.selectedItem == null) return;
        IID id = (IID) list.selectedItem.GetMeta("object").AsString();
        var creature = Eevee.models.creatureModels.Get(id);
        Eevee.models.creatureModels.Remove(creature.entityUid);
        // dont delete the skins, keep them for later use and
        // TODO add new tabs to vaporeon for skins
        list.selectedItem = null;
        //list.GetVaporeon().CurrentCreatureModel = null;
    }
    public ICreatureModel getSelectedItem()
    {
        IID id = (IID) list.selectedItem.GetMeta("object").AsString();
        return Eevee.models.creatureModels.Get(id);
    }
    public void edit() => list.GetVaporeon().CurrentCreatureModel = getSelectedItem();
    public void copy() => list.GetVaporeon().CurrentObjectCopied = getSelectedItem();
}


public class SpellListUtil : IListUtilGeneric<ISpellModel>
{
    public ResourceList list { get; init; }
    public SpellListUtil(ResourceList list) => this.list = list;
    public void fillList()
    {
        foreach (ISpellModel spell in Eevee.models.spellModels.Values)
            createChildNode(spell);
    }
    public void createChildNode(ISpellModel model)
    {
        // get the first skin for that spell
        var skin = Eevee.models.spellSkins.Values.Where(skin => skin.spellModelUid == model.entityUid).First();
        var icon = skin.icon;
        var name = model.GetName(); //Eevee.models.i18n.Get(spell.nameId);
        var desc = model.GetDescription(); //Eevee.models.i18n.Get(spell.descriptionId);
        list.addChild(name.ToString(), new Color().Random(), model.entityUid);
    }
    public void onCreateBtn()
    {
        var spellmodel = SpellModel.Create();
        var spellskin = SpellSkin.Create(); // base skin
        spellskin.spellModelUid = spellmodel.entityUid;

        var name = StringEntity.Create("Name for: " + spellmodel.entityUid);
        spellmodel.nameId = name.entityUid;
        Eevee.models.i18n.Add(name.entityUid, name);

        var desc = StringEntity.Create("Desc for: " + spellmodel.entityUid);
        spellmodel.descriptionId = desc.entityUid;
        Eevee.models.i18n.Add(desc.entityUid, desc);

        Eevee.models.spellSkins.Add(spellskin.entityUid, spellskin);
        Eevee.models.spellModels.Add(spellmodel.entityUid, spellmodel);
    }
    public void onRemoveBtn()
    {
        if (list.selectedItem == null) return;
        IID id = (IID) list.selectedItem.GetMeta("object").AsString();
        list.selectedItem = null;
        var spell = Eevee.models.spellModels.Get(id);
        Eevee.models.spellModels.Remove(spell.entityUid);
        Eevee.models.spellSkins.Remove(s => s.spellModelUid == spell.entityUid);
    }
    public ISpellModel getSelectedItem()
    {
        IID id = (IID) list.selectedItem.GetMeta("object").AsString();
        return Eevee.models.spellModels.Get(id);
    }
    public void edit() => list.GetVaporeon().CurrentSpellModel = getSelectedItem();
    public void copy() => list.GetVaporeon().CurrentObjectCopied = getSelectedItem();
}


public class EffectListUtil : IListUtilGeneric<IEffect>
{
    public ResourceList list { get; init; }
    public EffectListUtil(ResourceList list)
    {
        this.list = list;
        foreach (Type t in Vaporeon.effectTypes)
            list.CreatePopupMenu.AddItem(t.FullName);
        list.CreatePopupMenu.IndexPressed += EffectPopup_IndexPressed;
    }
    public void fillList()
    {
        foreach (IEffect effect in Eevee.models.effects.Values)
            createChildNode(effect);
    }
    public void createChildNode(IEffect effect)
    {
        IEffectModel model = Eevee.models.effectModels.Get(effect.modelUid);
        var name = model.GetName(); 
        var desc = model.GetDescription(); 
        list.addChild(name.ToString(), new Color().Random(), effect.entityUid);
    }
    public void onCreateBtn()
    {
        list.CreatePopupMenu.Show();
    }
    private void EffectPopup_IndexPressed(long index)
    {
        Type effectType = Vaporeon.effectTypes[(int) index];
        var createMethod = effectType.GetMethod(nameof(EffectBase.Create), BindingFlags.Static);
        IEffect effect = (IEffect) createMethod.Invoke(null, new object[0]);

        //TODO IEffectSkin skin;

        Eevee.models.effects.Add(effect.entityUid, effect);
    }
    public void onRemoveBtn()
    {
        if (list.selectedItem == null) return;
        IID id = (IID) list.selectedItem.GetMeta("object").AsString();
        list.selectedItem = null;
        var effect = Eevee.models.effects.Get(id);
        Eevee.models.effects.Remove(effect.entityUid);
    }
    public IEffect getSelectedItem()
    {
        IID id = (IID) list.selectedItem.GetMeta("object").AsString();
        return Eevee.models.effects.Get(id);
    }
    public void edit() => list.GetVaporeon().CurrentEffect = getSelectedItem();
    public void copy() => list.GetVaporeon().CurrentObjectCopied = getSelectedItem();
}