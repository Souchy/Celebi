using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon.common;

public partial class ZoneEditor : Control
{
    //[Export]
    private IZone zone { get; set; }

    public IEventBus bus { get; set; } = new EventBus();

    [Export]
    public string Title { get; set; }

    #region Nodes - Main Bar
    [NodePath] public Button SaveBtn { get; set; }
    //[NodePath] public LineEdit NameEdit { get; set; }
    //[NodePath] public Button PreviewBtn { get; set; }
    [NodePath] public Button CopyBtn { get; set; }
    #endregion

    #region Nodes
    [NodePath] public OptionButton ZoneType { get; set; }
    [NodePath] public HBoxContainer SizeVector { get; set; }
    //[NodePath] public SpinBox SizeVectorI0 { get; set; }
    //[NodePath] public SpinBox SizeVectorI1 { get; set; }
    //[NodePath] public SpinBox SizeVectorI2 { get; set; }
    [NodePath] public OptionButton WorldOriginActor { get; set; }
    [NodePath] public SpinBox WorldOffsetVectorX { get; set; }
    [NodePath] public SpinBox WorldOffsetVectorZ { get; set; }
    [NodePath] public OptionButton LocalOriginBtn { get; set; }
    [NodePath] public OptionButton RotationBtn { get; set; }
    [NodePath] public OptionButton ExtendFromSourceBtn { get; set; }
    [NodePath] public CheckBox NegativeBtn { get; set; }
    [NodePath] public Button BtnAddChild { get; set; }
    [NodePath] public Button BtnRemoveChild { get; set; }
    [NodePath] public ItemList ChildrenList { get; set; }
    [NodePath] public ZonePreview ZonePreview { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        //this.NameEdit.Text = Title;

        ZoneType.Clear();
        foreach (var type in Enum.GetNames(typeof(ZoneType)))
        {
            ZoneType.AddItem(type);
        }
        // Direction9Type
        foreach(var type in Enum.GetNames(typeof(Direction9Type)))
        {
            LocalOriginBtn.AddItem(type);
            RotationBtn.AddItem(type);
        }

        // TEST ONLY
        var z = new Zone();
        z.zoneType.value = souchy.celebi.eevee.enums.ZoneType.circle;
        z.size.value.setAt(0, 3);
        init(z);
    }

    public void init(IZone model)
    {
        this.zone = model;
        // sub preview
        this.bus.subscribe(this.ZonePreview);
        // size parameters
        SizeVector.RemoveAndQueueFreeChildren();
        PropertiesComponent.GenerateGrid(model.GetSize(), SizeVector, publishRefresh);
        // type
        ZoneType.Select((int) model.zoneType.value);
        ZoneType.ItemSelected += (i) =>
        {
            model.zoneType.value = (ZoneType) i;
            SizeVector.RemoveAndQueueFreeChildren();
            PropertiesComponent.GenerateGrid(model.GetSize(), SizeVector, publishRefresh);
            publishRefresh();
        };
    }
    public void publishRefresh()
    {
        GD.Print("publishRefresh");
        bus.publish(nameof(ZonePreview.RefreshPreview), zone);
    }

    /*
    public void onZoneTypeSelected(int id)
    {
        switch ((ZoneType) id)
        {
            case souchy.celebi.eevee.enums.ZoneType.line:
                SizeVectorI0.Editable = true;
                SizeVectorI1.Editable = true;
                SizeVectorI2.Editable = false;
                SizeVectorI0.Prefix = "Min";
                SizeVectorI1.Prefix = "Max";
                SizeVectorI2.Prefix = "";
                break;
            case souchy.celebi.eevee.enums.ZoneType.circle:
            case souchy.celebi.eevee.enums.ZoneType.square:
            case souchy.celebi.eevee.enums.ZoneType.rectangle:
                SizeVectorI0.Editable = true;
                SizeVectorI1.Editable = true;
                SizeVectorI2.Editable = true;
                SizeVectorI0.Prefix = "Min";
                SizeVectorI1.Prefix = "Max";
                SizeVectorI2.Prefix = "%";
                break;
            case souchy.celebi.eevee.enums.ZoneType.point:
            default:
                SizeVectorI0.Editable = false;
                SizeVectorI1.Editable = false;
                SizeVectorI2.Editable = false;
                SizeVectorI0.Prefix = "";
                SizeVectorI1.Prefix = "";
                SizeVectorI2.Prefix = "";
                break;
        }
    }
    */

}
