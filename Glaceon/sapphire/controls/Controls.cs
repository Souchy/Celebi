using Godot;
using Godot.Sharp.Extras;
using System;

public partial class Controls : Node // Node3D
{
    // const
    public const int floorHeight = 0;

    // click position
    //public readonly int RAY_LENGTH = 1000;
    //public readonly Plane GROUND_PLANE = new Plane(Vector3.Up, 0);
    public readonly float translationSpeed = 10;
    public readonly float rotationSpeed = 10;

    // pan
    public bool isPanning = false;
    public Vector3 lastCursorPositionWorld = Vector3.Zero;
    public Vector3 translation = Vector3.Zero;
    public Vector3 rotation = Vector3.Zero;
    public Vector3 rotationUnit = Vector3.Zero;

    // 
    public Node draggedEntity;
    public Node targetEntity;

    // Nodes
    [NodePath]
    public Camera3D camTop;
    [NodePath]
    public Camera3D camIso;
    [NodePath]
    public Node3D cursorBoard;
    [NodePath]
    public Node3D cursorWorld;

    public Camera3D cam { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.OnReady();
        cam = camIso;
        //camTop.Near = -100;
        //camIso.Near = -100;
        cursorWorld.Visible = false;
        // doesnt work :(
        //var mat2 = cursorWorld.GetNode<MeshInstance3D>().MaterialOverride;
        //foreach(var prop in mat2.GetPropertyList())
        //{
        //	if (((string)prop["name"]) == "Albedo")
        //	{
        //		prop["Color"] = new Color(1, 0, 0);
        //	}
        //}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (isPanning)
        {
            var currentMousePos = getCursorPosToWorld(); 
            var mouseWorldDelta = currentMousePos - this.lastCursorPositionWorld;

            //this.cam.LookAtFromPosition(from, ToolAttribute, up);
            var thing = (cam.GlobalTransform.basis.z * mouseWorldDelta.z + cam.GlobalTransform.basis.x * mouseWorldDelta.x);
            this.cam.Position += -thing;

            lastCursorPositionWorld = getCursorPosToWorld();
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        // Cursor
        var posBoard = getCursorPosToBoard();
        this.cursorBoard.Position = posBoard;

        // Panning
        if (@event.IsActionPressed("camera_pan"))
        {
            isPanning = true;
            lastCursorPositionWorld = getCursorPosToWorld();
        }
        if (@event.IsActionReleased("camera_pan")) isPanning = false;
        // Zoom
        if (@event.IsAction("camera_zoom_in")) this.cam.Size--;
        if (@event.IsAction("camera_zoom_out")) this.cam.Size++;
        // Change cam
        if (@event.IsActionPressed("changeCam"))
        {
            this.cam.Current = false;
            if (this.cam == camTop) this.cam = this.camIso;
            else this.cam = camTop;
            this.cam.Current = true;
        }
        if (@event.IsActionPressed("resetCam"))
        {
            this.resetIso();
            this.resetTop();
        }
        // Rotate 45
        if (@event.IsActionPressed("rotate45Right")) this.cam.Rotate(Vector3.Up, 45);
        if (@event.IsActionPressed("rotate45Left")) this.cam.Rotate(Vector3.Up, -45);
        if (@event.IsActionPressed("rotate45Up")) this.cam.Rotate(cam.GlobalTransform.basis.x, 45);
        if (@event.IsActionPressed("rotate45Down")) this.cam.Rotate(cam.GlobalTransform.basis.x, -45);


        //var dir = this.cam.GlobalTransform.basis;
        //var pos = this.cam.Position;
        //var rot = this.cam.Rotation;
        //translation = Vector3.Zero;
        //rotation = Vector3.Zero;
        //rotationUnit = Vector3.Zero;

        //translate(@event);
        //if (isPanning) move();
    }

    public void translate(InputEvent @event)
    {
        // Translation XY
        if (@event.IsActionPressed("camera_forward")) translation += new Vector3(1, 1, 0); //translation += (1, 1, 0); // (up.x, up.y, 0);
        if (@event.IsActionPressed("camera_backward")) translation += new Vector3(1, 1, 0); //translation.add(-up.x, -up.y, 0);
        if (@event.IsActionPressed("camera_left")) translation += new Vector3(1, 1, 0); //translation.add(-up.y, up.x, 0);
        if (@event.IsActionPressed("camera_right")) translation += new Vector3(1, 1, 0); //translation.add(up.y, -up.x, 0);
                                                                                         // Rotation XZ only if 
        if (translation == Vector3.Zero)
        {
            //if (@event.IsActionPressed("camera_forward")) translation += new Vector3(1, 1, 0); //translation += (1, 1, 0); // (up.x, up.y, 0);
            //if (@event.IsActionPressed("camera_backward")) translation += new Vector3(1, 1, 0); //translation.add(-up.x, -up.y, 0);
            //if (@event.IsActionPressed("camera_left")) translation += new Vector3(1, 1, 0); //translation.add(-up.y, up.x, 0);
            //if (@event.IsActionPressed("camera_right")) translation += new Vector3(1, 1, 0); //translation.add(up.y, -up.x, 0);
        }
    }

    public void move()
    {
        var pos = getCursorPosToWorld();
        if (lastCursorPositionWorld != Vector3.Zero)
        {
            GD.Print("move " + pos);
            var trans = pos - lastCursorPositionWorld;
            //Camera.Projection.
            this.cam.Translate(trans);
        }
    }


    public Vector3 getCursorPosToWorld()
    {
        var mouse_pos = GetViewport().GetMousePosition();
        var origin = this.cam.ProjectRayOrigin(mouse_pos); // from_ray
        var normal = this.cam.ProjectRayNormal(mouse_pos);
        //var ray = origin + normal * RAY_LENGTH; // to_ray
        //var pos = GROUND_PLANE.IntersectRay(origin, ray);
        //return pos;
        var distance = (floorHeight - origin.y) / normal.y;
        var v = normal * distance + origin;
        return v;
    }

    public Vector3 getCursorPosToBoard()
    {
        var pos = getCursorPosToWorld();
        //if (!pos.HasValue) return null;
        var posBoard = new Vector3(pos.x, 0, pos.z);
        posBoard = posBoard.Round();
        //GD.Print("word " + pos?.x + " -> " + posBoard);
        //GD.Print(string.Format("world: [{0:F2}, {1:F2}], board: [{2:F2}, {3:F2}]", pos?.x, pos?.z, posBoard.x, posBoard.z));
        return posBoard;
    }

    public void resetIso()
    {
        this.camIso.Position = new Vector3(-5, 7, 5);
        this.camIso.Rotation = new Vector3(-45, -45, 0);
    }
    public void resetTop()
    {
        this.camTop.Position = new Vector3(0, 5, 0);
        this.camTop.Rotation = new Vector3(-90, 0, 0);
    }

}
