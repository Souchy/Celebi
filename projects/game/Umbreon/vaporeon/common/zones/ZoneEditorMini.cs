using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.zones;
using System;
using souchy.celebi.umbreon.vaporeon.common;

public partial class ZoneEditorMini : PanelContainer
{

    public IZone zone { get; set; }

    #region Nodes
    [NodePath] public Label Label { get; set; } 
    [NodePath] public OptionButton ZoneType { get; set; }
    [NodePath] public Button BtnEdit { get; set; }
    [NodePath] public HBoxContainer SizeVector { get; set; }
    //[NodePath] public SpinBox SizeVectorI0 { get; set; }
    //[NodePath] public SpinBox SizeVectorI1 { get; set; }
    //[NodePath] public SpinBox SizeVectorI2 { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        // Type
        ZoneType.Clear();
        ZoneType.ExpandIcon = false;
        foreach (var type in Enum.GetNames(typeof(ZoneType)))
        {
            //var tex = GD.Load<Texture2D>($"res://assets/icons/zones/{type}.png");
            //tex.GetImage().Resize(30, 30);
            //var img = Image.LoadFromFile($"res://assets/icons/zones/{type}.png");
            var img = new Image();
            img.Load($"res://assets/icons/zones/{type}.png");
            img.Resize(24, 24);
            //var tex = new Texture2D();
            var tex2 = ImageTexture.CreateFromImage(img);
            ZoneType.AddIconItem(tex2, type);
        }
        // on selected type
        ZoneType.ItemSelected += (i) =>
        {
            zone.zoneType.value = (ZoneType) i;
            SizeVector.RemoveAndQueueFreeChildren();
            PropertiesComponent.GenerateGrid(zone.GetSize(), SizeVector, publishRefresh);
            publishRefresh();
        };
        // btn open full editor
        BtnEdit.ButtonUp += () => this.GetVaporeon().openEditor(zone);
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
        zone = null;
    }
    private void load()
    {
        // select
        ZoneType.Select((int) zone.zoneType.value);
        // size parameters
        SizeVector.RemoveAndQueueFreeChildren();
        PropertiesComponent.GenerateGrid(zone.GetSize(), SizeVector, publishRefresh);
        //publishRefresh();
    }
    #endregion

    /// <summary>
    /// refresh preview
    /// </summary>
    public void publishRefresh()
    {
        //bus.publish(nameof(ZonePreview.RefreshPreview), zone);
    }

}
