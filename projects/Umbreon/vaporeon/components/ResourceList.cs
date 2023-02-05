using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using Umbreon.vaporeon;

public partial class ResourceList : Control
{

    #region Constants
    public PackedScene listItemScene { get; } = GD.Load<PackedScene>("res://vaporeon/components/ResourceListItem.tscn");
    public StyleBoxFlat ResourceListItemNormal { get; } = (StyleBoxFlat) GD.Load("res://vaporeon/themes/ResourceListItemNormal.tres");
    public StyleBoxFlat ResourceListItemHover { get; } = (StyleBoxFlat) GD.Load("res://vaporeon/themes/ResourceListItemHover.tres");
    public StyleBoxFlat ResourceListItemSelected { get; } = (StyleBoxFlat) GD.Load("res://vaporeon/themes/ResourceListItemSelected.tres");
    public Color ColorSelected { get; } = new Color(1, 1, 1, 150f / 255f);
    public Color ColorUnselected { get; } = new Color(0, 0, 0, 1);
    #endregion

    #region Properties
    public Control selectorForControl { get; set; }
    public Control selectedNode { get; set; }
    private StyleBoxFlat styleNormal = null;
    private StyleBoxFlat styleHover = null;
    #endregion

    #region Nodes - Main bar
    [NodePath]
    public LineEdit Search { get; set; }
    [NodePath]
    public Button ClearSearchBtn { get; set; }
    [NodePath]
    public Button CreateBtn { get; set; }
    [NodePath]
    public Button DeleteBtn { get; set; }
    #endregion

    #region Nodes
    [NodePath]
    public HFlowContainer Container { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();

        this.Container.QueueFreeChildren();
        fillList();

        this.Search.TextChanged += onSearchChanged;
        this.ClearSearchBtn.Pressed += onClickClearSearchBtn;
        this.CreateBtn.Pressed += onClickCreateBtn;
        this.DeleteBtn.Pressed += onClickRemoveBtn;
    }

    #region Diamond Handlers
    [Subscribe(nameof(IEntityDictionary<IID, IID>.Add))]
    public void onDiamondAdd(object dic, IID id, object model)
    {
        GD.Print($"ResList.onDiamondAdd: {model}");
        createChildNode(id);
    }
    [Subscribe(nameof(IEntityDictionary<IID, ISpellModel>.Remove))]
    public void onDiamondRemove(object dic, IID id, object model)
    {
        GD.Print($"ResList.onDiamondRemove: {model}");
        removeChild(id);
    }
    [Subscribe(nameof(IEntityDictionary<IID, ISpellModel>.Set))]
    public void onDiamondSet(object dic, IID id, object model)
    {
        GD.Print($"ResList.onDiamondSet: {model}");
        removeChild(id);
        createChildNode(id);
    }
    #endregion

    #region Functions
    public IID getSelectedItemID() => (IID) selectedNode.GetMeta(VaporeonUtil.metaIID).AsString();
    public virtual void createChildNode(IID modelID) => throw new NotImplementedException();
    public virtual void fillList() => throw new NotImplementedException();
    public virtual void publishSelect(IID id) => throw new NotImplementedException();
    public virtual void onClickCreateBtn() => throw new NotImplementedException();
    public virtual void onClickRemoveBtn() => throw new NotImplementedException();
    public virtual void onClickEdit() => throw new NotImplementedException();
    public virtual void onClickCopy() => throw new NotImplementedException();

    public void addChild(string name, Color col, IID meta)
    {
        // create new square with the icon, name and description
        var item = (Control) listItemScene.Instantiate();
        var image = (ColorRect) item.GetNode("PanelContainer/VBoxContainer/Image");
        image.Color = col;
        var lbl = (Label) item.GetNode("PanelContainer/VBoxContainer/Label");
        lbl.Text = name; //.ToString();
        //item.Name = spell.entityUid.value;
        item.SetMeta(VaporeonUtil.metaIID, meta.ToString());

        //item.MouseFilter = MouseFilterEnum.;
        item.GuiInput += (e) => onMouseClickItem(e, item);
        item.MouseEntered += () => onMouseEnterItem(item);
        item.MouseExited += () => onMouseExitItem(item);
        //image.GuiInput += (e) => select(e, item);
        //lbl.GuiInput += (e) => select(e, item);
        Container.AddChild(item);
    }

    public void removeChild(IID id)
    {
        Node node = Container.GetChildren().First(c => c.GetMeta(VaporeonUtil.metaIID).AsString() == id);
        Container.RemoveChild(node);
        node.QueueFree();
    }
    public void search()
    {

    }
    #endregion

    #region GUI Handlers
    public void onSearchChanged(string text) => Debouncer.debounce($"{this.GetType().Name}:search", 500, search);
    public void onClickClearSearchBtn() => this.Search.Text = "";
    private void onMouseEnterItem(Control item)
    {
        if (item == selectedNode) return;
        var panel = (PanelContainer) item.GetNode("PanelContainer");
        panel.Set("theme_override_styles/panel", ResourceListItemHover);
    }
    private void onMouseExitItem(Control item)
    {
        if (item == selectedNode) return;
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
                    if (index == 0) onClickEdit();
                    if (index == 1) onClickCopy();
                };
                pop.Show();
                this.AddChild(pop);
            }
            if (eventMouse.ButtonIndex != MouseButton.Left)
                return;

            if (selectorForControl != null)
            {
                publishSelect(item.GetMetaIID());
                return;
            }

            if (selectedNode == item)
            {
                onClickEdit();
                PackedScene creatureEditorScene = GD.Load<PackedScene>("res://vaporeon/menus/CreatureEditor.tscn");
                creatureEditorScene.Instantiate<CreatureEditor>();
            }
            // remove previous selected
            if (selectedNode != null)
            {
                var selectedPanel = (PanelContainer) selectedNode.GetNode("PanelContainer");
                selectedPanel.Set("theme_override_styles/panel", ResourceListItemNormal);
            }
            // select new
            selectedNode = item;
            var panel = (PanelContainer) selectedNode.GetNode("PanelContainer");
            panel.Set("theme_override_styles/panel", ResourceListItemSelected);
        }
    }
    #endregion

}
