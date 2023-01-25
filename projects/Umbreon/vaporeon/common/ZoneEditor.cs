using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.zones;
using System;

public partial class ZoneEditor : PanelContainer
{

    //[Export]
    public IZone zone { get; set; }


    [Export]
    public string Title { get; set; }

    [NodePath("MarginContainer/VBoxContainer/Titlebar/ZoneNameLbl")]
    public Label ZoneNameLbl { get; set; }

    [NodePath("MarginContainer/VBoxContainer/ParametersGrid/ZoneType")]
    public OptionButton ZoneTypeBtn { get; set; }

    [NodePath("MarginContainer/VBoxContainer/ParametersGrid/SizeVector/vectA")]
    public SpinBox vectA { get; set; }
    [NodePath("MarginContainer/VBoxContainer/ParametersGrid/SizeVector/vectB")]
    public SpinBox vectB { get; set; }
    [NodePath("MarginContainer/VBoxContainer/ParametersGrid/SizeVector/vectC")]
    public SpinBox vectC { get; set; }

    [NodePath("MarginContainer/VBoxContainer/ParametersGrid/LocalOriginBtn")]
    public OptionButton LocalOriginBtn { get; set; }
    [NodePath("MarginContainer/VBoxContainer/ParametersGrid/RotationBtn")]
    public OptionButton RotationBtn { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
        this.ZoneNameLbl.Text = Title;

        ZoneTypeBtn.Clear();
        foreach (var type in Enum.GetNames(typeof(IZoneType)))
        {
            ZoneTypeBtn.AddItem(type);
        }

        // Direction9Type
        foreach(var type in Enum.GetNames(typeof(Direction9Type)))
        {
            LocalOriginBtn.AddItem(type);
            RotationBtn.AddItem(type);
        }

    }

    public void onZoneTypeSelected(int id)
    {
        switch ((IZoneType) id)
        {
            case IZoneType.line:
                vectA.Editable = true;
                vectB.Editable = true;
                vectC.Editable = false;
                vectA.Prefix = "Min";
                vectB.Prefix = "Max";
                vectC.Prefix = "";
                break;
            case IZoneType.circle:
            case IZoneType.square:
            case IZoneType.rectangle:
                vectA.Editable = true;
                vectB.Editable = true;
                vectC.Editable = true;
                vectA.Prefix = "Min";
                vectB.Prefix = "Max";
                vectC.Prefix = "%";
                break;
            case IZoneType.point:
            default:
                vectA.Editable = false;
                vectB.Editable = false;
                vectC.Editable = false;
                vectA.Prefix = "";
                vectB.Prefix = "";
                vectC.Prefix = "";
                break;
        }
    }

}
