using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.impl.util;
using System;
using static souchy.celebi.eevee.impl.objects.zones.AreaGenerator;

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
        if (points.Count == 0)
            return;

        int minz = Math.Min(points.minZ() - 1, 0);
        int maxz = Math.Max(points.maxZ(), 0);
        int minx = Math.Min(points.minX() - 1, 0);
        int maxx = Math.Max(points.maxX(), 0);
        GD.Print($"Bounds: min [{minx}, {minz}], max [{maxx}, {maxz}]");
        GD.Print($"Points[{points.Count}] "); //{points.ToString()}

        GridContainer.Columns = maxx - minx + 1; //points.sizeX() + 1;

        for (int j = maxz; j >= minz; j--)
        {
            for (int i = minx; i <= maxx; i++)
            {
                var point = points.FirstOrDefault(p => p.x == i && p.z == j);
                var rect = new ColorRect();
                        rect.Color = new Color(0, 0, 0);
                rect.SizeFlagsHorizontal |= SizeFlags.ExpandFill;
                rect.SizeFlagsVertical |= SizeFlags.ExpandFill;

                if (i == 0 && j == 0)
                {
                    rect.Color = new Color(0, 1, 0);
                    GridContainer.AddChild(rect);
                }
                else if (point == null)
                {
                    if(!addScaleLbl(points, i, j))
                        GridContainer.AddChild(rect);
                }
                else
                {
                    //GD.Print($"ColorRect in [{i}, {j}]");
                    rect.Color = new Color(1, 0, 1);
                    GridContainer.AddChild(rect);
                }
            }
        }
    }

    public bool addScaleLbl(Points points, int i, int j)
    {
        var lbl = new Label();
        lbl.SizeFlagsHorizontal |= SizeFlags.ExpandFill;
        lbl.SizeFlagsVertical |= SizeFlags.ExpandFill;
        lbl.HorizontalAlignment = HorizontalAlignment.Center;
        lbl.VerticalAlignment = VerticalAlignment.Center;
        lbl.Text = null;
        int minz = Math.Min(points.minZ() - 1, 0);
        //int maxz = Math.Max(points.maxZ(), 0);
        int minx = Math.Min(points.minX() - 1, 0);
        //int maxx = Math.Max(points.maxX(), 0);
        if (i == points.minX() && j == minz)
            lbl.Text = points.minX().ToString();
        if (i == points.maxX() && j == minz)
            lbl.Text = points.maxX().ToString();
        if (i == minx && j == points.minZ())
            lbl.Text = points.minZ().ToString();
        if (i == minx && j == points.maxZ())
            lbl.Text = points.maxZ().ToString();
        if(lbl.Text != null)
        {
            GridContainer.AddChild(lbl);
            return true;
        }
        return false;
    }


}
