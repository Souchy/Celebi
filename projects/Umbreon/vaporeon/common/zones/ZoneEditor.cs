using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using System;

public partial class ZoneEditor : Control
{
    //[Export]
    private IZone zone { get; set; }

    [Export]
    public string Title { get; set; }

    #region Nodes - Main Bar
    [NodePath] public LineEdit NameEdit { get; set; }
    [NodePath] public Button PreviewBtn { get; set; }
    [NodePath] public Button CopyBtn { get; set; }
    #endregion

    #region Nodes
    [NodePath] public OptionButton ZoneType { get; set; }
    [NodePath] public SpinBox SizeVectorI0 { get; set; }
    [NodePath] public SpinBox SizeVectorI1 { get; set; }
    [NodePath] public SpinBox SizeVectorI2 { get; set; }
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
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        //this.NameEdit.Text = Title;

        ZoneType.Clear();
        foreach (var type in Enum.GetNames(typeof(IZoneType)))
        {
            ZoneType.AddItem(type);
        }

        // Direction9Type
        foreach(var type in Enum.GetNames(typeof(Direction9Type)))
        {
            LocalOriginBtn.AddItem(type);
            RotationBtn.AddItem(type);
        }

    }

    public void init(IZone model)
    {

    }

    public void onZoneTypeSelected(int id)
    {
        switch ((IZoneType) id)
        {
            case IZoneType.line:
                SizeVectorI0.Editable = true;
                SizeVectorI1.Editable = true;
                SizeVectorI2.Editable = false;
                SizeVectorI0.Prefix = "Min";
                SizeVectorI1.Prefix = "Max";
                SizeVectorI2.Prefix = "";
                break;
            case IZoneType.circle:
            case IZoneType.square:
            case IZoneType.rectangle:
                SizeVectorI0.Editable = true;
                SizeVectorI1.Editable = true;
                SizeVectorI2.Editable = true;
                SizeVectorI0.Prefix = "Min";
                SizeVectorI1.Prefix = "Max";
                SizeVectorI2.Prefix = "%";
                break;
            case IZoneType.point:
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

}
