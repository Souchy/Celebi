using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.zones;
using System;

public partial class ZoneEditorMini : PanelContainer
{

    public IZone zone { get; set; }

    #region Nodes
    [NodePath] public Label Label { get; set; } 
    [NodePath] public OptionButton ZoneType { get; set; }
    [NodePath] public SpinBox SizeVectorI0 { get; set; }
    [NodePath] public SpinBox SizeVectorI1 { get; set; }
    [NodePath] public Button BtnEdit { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        // fill zone types
        foreach (var z in Enum.GetNames<ZoneType>())
            ZoneType.AddItem(z);
        ZoneType.Select(0);
    }

    #region Init
    public void init(IZone z)
    {
        unload();
        this.zone = z;
        load();
    }
    private void unload()
    {

    }
    private void load()
    {

    }
    #endregion


}
