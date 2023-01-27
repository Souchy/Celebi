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
    public Color ColorSelected { get; } = new Color(1, 1, 1, 150f/255f);
    public Color ColorUnselected { get; } = new Color(0, 0, 0, 1);
    #endregion

    #region Nodes
    [NodePath]
    public HFlowContainer Container { get; set; }
    #endregion

    #region Toolbar Properties
    [NodePath("Toolbar/Search")]
    public LineEdit Search { get; set; }
    [NodePath("Toolbar/Search/ClearSearchBtn")]
    public Button ClearSearchBtn { get; set; }
    [NodePath("Toolbar/CreateBtn")]
    public Button CreateBtn { get; set; }
    [NodePath("Toolbar/DeleteBtn")]
    public Button DeleteBtn { get; set; }
    [NodePath("Toolbar/CreatePopupMenu")]
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
            Eevee.models.creatureModels.GetEventBus().subscribe(util);
            //Eevee.models.creatureSkins.GetEventBus().subscribe(util);
        }
        if (this.resourceType == ResourceListType.ISpellModel)
        {
            util = new SpellListUtil(this);
            Eevee.models.spellModels.GetEventBus().subscribe(util);
            //Eevee.models.spellSkins.GetEventBus().subscribe(util);
            // fill list
        }
        if (this.resourceType == ResourceListType.IEffect)
        {
            util = new EffectListUtil(this);
            Eevee.models.effects.GetEventBus().subscribe(util);
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

    public void select(InputEvent ev, Control item)
    {
        if (ev is not InputEventMouseButton)
            return;
        InputEventMouseButton eventMouse = (InputEventMouseButton) ev;
        if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
        {
            foreach (var child in Container.GetChildren())
            {
                var childpanel = (PanelContainer) child.GetNode("PanelContainer");
                childpanel.Theme.SetColor("shadow_color", "theme_override_styles/panel", ColorUnselected);
                child.SetMeta("selected", false);
            }
            selectedItem = item;
            var panel = (PanelContainer) selectedItem.GetNode("PanelContainer");
            panel.Theme.SetColor("shadow_color", "theme_override_styles/panel", ColorSelected);
            selectedItem.SetMeta("selected", true);
        }
    }

    public void addChild(string name, Color col, IID meta)
    {
        // create new square with the icon, name and description
        var item = listItemScene.Instantiate();
        var image = (ColorRect) item.GetNode("%Image");
        image.Color = col;
        var lbl = (Label) item.GetNode("%Label");
        lbl.Text = name.ToString();

        //item.Name = spell.entityUid.value;
        item.SetMeta("object", meta.value);
        Container.AddChild(item);
    }
    public void removeChild(IID id)
    {
        Node node = Container.GetChildren().First(c => c.GetMeta("object").AsString() == id.value);
        Container.RemoveChild(node);
        node.QueueFree();
    }

}


public interface IListUtil
{
    public ResourceList list { get; init; }
    public void fillList();
    public void onCreateBtn();
    public void onRemoveBtn();
    //public void onDiamondsChanged(Type propertyType, string propertyPath, object newValue, object oldValue);
}

public interface IListUtilGeneric<T> : IListUtil
{
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
    public void createChildNode(ICreatureModel creatureModel)
    {
        //var skin = Eevee.models.creatureSkins.Get(creatureModel.skins.First());
        //var icon = skin.icon;
        var name = Eevee.models.i18n.Get(creatureModel.nameId);
        var desc = Eevee.models.i18n.Get(creatureModel.descriptionId);
        list.addChild(name, new Color().Random(), creatureModel.entityUid);
    }
    public void onCreateBtn()
    {
        var creatureSkin = new CreatureSkin(Eevee.uIdGenerator); 
        var creatureModel = new CreatureModel(Eevee.uIdGenerator);
        creatureModel.skins.Add(creatureSkin.entityUid);

        Eevee.models.i18n.Add(creatureSkin.nameId, "Name for: " + creatureSkin.entityUid);
        Eevee.models.i18n.Add(creatureSkin.descriptionId, "Desc for: " + creatureSkin.entityUid);
        Eevee.models.creatureSkins.Add(creatureSkin.entityUid, creatureSkin);

        Eevee.models.i18n.Add(creatureModel.nameId, "Name for: " + creatureModel.entityUid);
        Eevee.models.i18n.Add(creatureModel.descriptionId, "Desc for: " + creatureModel.entityUid);
        Eevee.models.creatureModels.Add(creatureModel.entityUid, creatureModel);
    }
    public void onRemoveBtn()
    {
        Node node = list.Container.GetChildren().First(c => c.GetMeta("selected").AsBool() == true);
        IID id = (IID) node.GetMeta("object").AsString();
        var creature = Eevee.models.creatureModels.Get(id);
        Eevee.models.creatureModels.Remove(creature.entityUid);
        // dont delete the skins, keep them for later use and
        // TODO add new tabs to vaporeon for skins
    }

    //this.GetEventBus().publish(nameof(Set), this, key, value);
    //public void onDiamondsChanged(Type propertyType, string propertyPath, object newValue, object oldValue)
    //{
    //    if (newValue != null && newValue is ICreatureModel creatureAdd)
    //        createChildNode(creatureAdd);
    //    else
    //    if (oldValue != null && oldValue is ICreatureModel creatureRemove)
    //        list.removeChild(creatureRemove.entityUid);
    //}
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
    public void createChildNode(ISpellModel spell)
    {
        //new Dictionary<IID, int>().Values
        // get the first skin for that spell
        var skin = Eevee.models.spellSkins.Values.Where(skin => skin.spellModelUid.value == spell.entityUid.value).First();
        var icon = skin.icon;
        var name = Eevee.models.i18n.Get(spell.nameId);
        var desc = Eevee.models.i18n.Get(spell.descriptionId);
        list.addChild(name, new Color().Random(), spell.entityUid);
    }
    //public void onDiamondsChanged(Type propertyType, string propertyPath, object newValue, object oldValue)
    //{
    //    if (propertyPath != nameof(DiamondModels.spellModels)) return;
    //    if(newValue != null && newValue is ISpellModel spellAdd)
    //    {
    //        createChildNode(spellAdd);
    //    } 
    //    else
    //    if (oldValue != null && oldValue is ISpellModel spellRemove)
    //    {
    //        Node node = list.Container.GetChildren()
    //            .First(c => spellRemove == (ISpellModel) (object) c.GetMeta("object"));
    //        list.Container.RemoveChild(node);
    //        node.QueueFree();
    //    }
    //}
    public void onCreateBtn()
    {
        var spellmodel = new SpellModel(Eevee.uIdGenerator); //);
        var spellskin = new SpellSkin(Eevee.uIdGenerator);
        spellskin.spellModelUid = spellmodel.entityUid;
        Eevee.models.i18n.Add(spellmodel.nameId, "Name for: " + spellmodel.entityUid);
        Eevee.models.i18n.Add(spellmodel.descriptionId, "Desc for: " + spellmodel.entityUid);
        Eevee.models.spellSkins.Add(spellskin.entityUid, spellskin);
        Eevee.models.spellModels.Add(spellmodel.entityUid, spellmodel);
    }
    public void onRemoveBtn()
    {
        Node node = list.Container.GetChildren().First(c => c.GetMeta("selected").AsBool() == true);
        //ISpellModel spell = (ISpellModel) (object) node.GetMeta("object");
        IID id = (IID) node.GetMeta("object").AsString();
        var spell = Eevee.models.spellModels.Get(id);
        Eevee.models.spellModels.Remove(spell.entityUid);
        Eevee.models.spellSkins.Remove(s => s.spellModelUid.value == spell.entityUid.value);
    }
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
        var name = Eevee.models.i18n.Get(model.nameId);
        var desc = Eevee.models.i18n.Get(model.descriptionId);
        list.addChild(name, new Color().Random(), effect.entityUid);
    }
    //public void onDiamondsChanged(Type propertyType, string propertyPath, object newValue, object oldValue)
    //{
    //    if (newValue != null && newValue is IEffect effectAdd)
    //        createChildNode(effectAdd);
    //    else
    //    if (oldValue != null && oldValue is IEffect effectRemove)
    //        list.removeChild(effectRemove.entityUid);
    //}
    public void onCreateBtn()
    {
        list.CreatePopupMenu.Show();
    }
    private void EffectPopup_IndexPressed(long index)
    {
        Type effectType = Vaporeon.effectTypes[(int) index];
        IEffect effect = (IEffect) Activator.CreateInstance(effectType, Eevee.uIdGenerator);
        Eevee.models.effects.Add(effect.entityUid, effect);
    }
    public void onRemoveBtn()
    {
        Node node = list.Container.GetChildren().First(c => c.GetMeta("selected").AsBool() == true);
        IID id = (IID) node.GetMeta("object").AsString();
        var effect = Eevee.models.effects.Get(id);
        Eevee.models.effects.Remove(effect.entityUid);
    }
}