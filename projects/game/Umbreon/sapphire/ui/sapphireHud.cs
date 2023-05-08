//using Internal;
using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.umbreon.src;
using System;

namespace souchy.celebi.umbreon.sapphire.ui
{
    public partial class ui_sapphire : Control
    {
        [Export]
        public int anumber = 5;

        [NodePath]
        public Control ui_timeline;
        [NodePath]                                                                  
        public Control MainBar;


        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            this.OnReady();
            //base._Ready();
            this.Inject();
            // foo is not null!
            //var pos = MainBar.Position;
            //pos.y -= 200;
            //MainBar.Visible = false;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            //Console.WriteLine("UI Process from c# !");
            //GD.Print("UI process from C# "); // + sdf.str);
        }
    }

}
