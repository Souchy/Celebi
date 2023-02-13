using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.impl.util;
using System;

public partial class ZonePreview : PanelContainer
{
    #region Nodes
    [NodePath] public GridContainer GridContainer { get; set; }
    #endregion


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
	}

    public void init(IZone model)
    {
        RefreshPreview(model);
    }

    [Subscribe(nameof(RefreshPreview))]
    public void RefreshPreview(IZone zone)
    {
        var points = zone.GeneratePoints();
        GridContainer.RemoveAndQueueFreeChildren();
        GD.Print($"Bounds: min [{points.minX()}, {points.minZ()}], max [{points.maxX()}, {points.maxZ()}]");
        GD.Print($"Points[{points.Count}]: {points}");
        if (points.Count == 0)
            return;
        GridContainer.Columns = points.sizeX();
        for (int j = points.minZ(); j <= points.maxZ(); j++)
        {
            for (int i = points.minX(); i <= points.maxX(); i++)
            {
                var point = points.FirstOrDefault(p => p.x == i && p.z == j);
                var rect = new ColorRect();
                rect.SizeFlagsHorizontal |= SizeFlags.ExpandFill;
                rect.SizeFlagsVertical |= SizeFlags.ExpandFill;
                if (point == null)
                {
                    //GD.Print($"Empty in [{i}, {j}]");
                    rect.Color = new Color(0, 0, 0);
                    GridContainer.AddChild(rect);
                } else
                {
                    GD.Print($"ColorRect in [{i}, {j}]");
                    rect.Color = new Color(1, 0, 1);
                    GridContainer.AddChild(rect);
                }
            }
        }

    }


}
