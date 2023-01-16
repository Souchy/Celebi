using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.objects;
using System;
using Umbreon.common;


//public class vaporeon
//{

//    // have a project
//}

public partial class ResourceList : Control
{
    [Export]
    public string resourceType { get; set; }

    public List<Type> types;
    public Type selectedType;

    [NodePath]
    public ItemList ItemList { get; set; }
    [NodePath]
    public ItemList TypeList { get; set; }
    [NodePath]
    public HFlowContainer Container { get; set; }

    private PackedScene listItemScene = GD.Load<PackedScene>("res://vaporeon/menus/ResourceListItem.tscn");


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();

        this.Container.GetChildren().Clear();

        this.GetDiamonds().spellModels.Add(new souchy.celebi.eevee.face.util.IID("123"), default);


        if (this.resourceType == nameof(ISpell))
        {
            foreach(ISpellModel spell in this.GetDiamonds().spellModels.Values)
            {
                //GridContainer.AddChild()
                var skin = this.GetDiamonds().spellSkins.Values.FirstOrDefault(s => s.entityUid.value == spell.entityUid.value);
                var icon = skin.icon;
                var name = spell.nameId;
                var desc = spell.descriptionId;
                // new square with the icon, name and description

                var item = listItemScene.Instantiate();
                var image = (ColorRect) item.GetNode("%Image");
                var lbl = (Label) item.GetNode("%Label");

                lbl.Text = name.ToString();

            }
        }

        types = new List<Type> { typeof(ICreature), typeof(ISpell), typeof(IEffect) };
        selectedType = types[0];

        foreach (var s in types.Select(t => t.Name))
            TypeList.AddItem(s);

        //TypeList.Connect
        //this.EmitSignal(nameof(MySignalEventHandler));
        //this.Connect(
        //MySignalEventHandler.
        //this.Connect(nameof(MySignal), onHandle);

        selectedType = types[2];
        loadItems(selectedType);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	
    }


    [Signal]
    public delegate void MySignalEventHandler();
    public void onHandle()
    {

    }

    public void _on_type_list_item_selected(int index)
    {
        GD.Print("?????????????????????? " + index);
    }

    public void loadItems(Type type)
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p));

        ItemList.Clear();
        foreach (var t in types)
        {
            ItemList.AddItem(t.FullName);
        }
    }
}
