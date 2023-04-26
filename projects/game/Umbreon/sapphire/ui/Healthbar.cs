using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.impl.values;
using System;

public partial class Healthbar : Sprite3D
{

    #region Nodes
    [NodePath] public ProgressBar ProgressBar { get; set; }
    [NodePath] public Label Label { get; set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.OnReady();
	}

    public void set(double value, double max)
    {
        var tween = this.GetTree().CreateTween();

        var initialValue = ProgressBar.Value;
        var initialMax = ProgressBar.MaxValue;

        tween.TweenProperty(ProgressBar, nameof(ProgressBar.Value), value, 0.5d)
            .SetTrans(Tween.TransitionType.Expo)
            .SetEase(Tween.EaseType.Out);
            //tp.SetDelay(0);
        tween.TweenProperty(ProgressBar, nameof(ProgressBar.MaxValue), max, 0.5d);

        //tween.SetLoops(10);
        //var tc = tween.TweenCallback(Callable.From(() => Label.Text = $"{(int) value} / {(int) max}"));
        //tween.TweenProperty(Label, nameof(Label.Text), $"{(int) value} / {(int) max}", 0.5d);
        tween.TweenMethod(Callable.From<Vector2I>((vec) => Label.Text = $"{vec.X} / {vec.Y}"), 
            new Vector2I((int) initialValue, (int) initialMax), 
            new Vector2I((int) value, (int) max),
            0.5d
        )
            .SetTrans(Tween.TransitionType.Expo)
            .SetEase(Tween.EaseType.Out);

        tween.LoopFinished += Tween_LoopFinished;
        tween.StepFinished += Tween_StepFinished;
        tween.Finished += Tween_Finished;

        tween.Play();
    }

    private void Tween_Finished()
    {
        GD.Print($"Healthbar tween finished");
    }

    private void Tween_StepFinished(long idx)
    {
        GD.Print($"Healthbar tween step {idx}");
    }

    private void Tween_LoopFinished(long loopCount)
    {
        GD.Print($"Healthbar tween loop {loopCount}");
    }
}
