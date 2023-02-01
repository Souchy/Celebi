using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.util;
using System;
public partial class I18NEditor : Control
{

    [NodePath("ScrollContainer/GridContainer")]
    public GridContainer Grid { get; set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this.OnReady();
        GD.Print("I18N Editor reader");
        Eevee.models.i18n.GetEntityBus().subscribe(this);
        Eevee.models.i18n.ForEach((k, v) => onI18nAdd(Eevee.models.i18n, k, v));
    }

    private (Label lbl, LineEdit edit, Button btn) find(IID key)
    {
        if (!Grid.HasNode("l" + key.ToString()))
            return (null, null, null);
        if (!Grid.HasNode("e" + key.ToString()))
            throw new Exception("if e is null, then l should have been null before."); //return (null, null);
        return (
            (Label) Grid.GetNode("l" + key.ToString()),
            (LineEdit) Grid.GetNode("e" + key.ToString()),
            (Button) Grid.GetNode("b" + key.ToString())
        );
    }

    [Subscribe("Add")]
    public void onI18nAdd(object dic, IID key, IStringEntity value)
    {
        var btnRemove = new Button();
        btnRemove.Name = "b" + key.ToString();
        btnRemove.Text = "-";
        btnRemove.Pressed += () => Eevee.models.i18n.Remove(key); //onI18nRemove(dic, key, value);
        Grid.AddChild(btnRemove);

        var lbl = new Label();
        lbl.Name = "l" + key.ToString();
        lbl.Text = key.ToString();
        lbl.CustomMinimumSize = new Vector2(50, 0);
        //lbl.HorizontalAlignment = HorizontalAlignment.Fill;
        Grid.AddChild(lbl);

        var edit = new LineEdit();
        edit.Name = "e" + key.ToString();
        edit.Text = value.ToString();
        edit.TextChanged += (txt) => value.value = txt; 
        edit.CaretBlink = true;
        edit.ExpandToTextLength = true;
        edit.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        Grid.AddChild(edit);

        value.GetEntityBus().subscribe(this, nameof(onChange));
    }
    [Subscribe("Set")]
    public void onI18nSet(object dic, IID key, IStringEntity value)
    {
        var group = find(key);
        if(group != (null, null, null))
        {
            group.edit.Text = value.ToString();
            group.edit.TextChanged += (txt) => value.value = txt;
            value.GetEntityBus().subscribe(this, nameof(onChange));
        } 
        else
        {
            onI18nAdd(dic, key, value);
        }
    }
    [Subscribe("Remove")]
    public void onI18nRemove(object dic, IID key, IStringEntity value)
    {
        value.GetEntityBus().unsubscribe(this);
        var group = find(key);
        Grid.RemoveChild(group.lbl);
        Grid.RemoveChild(group.edit);
        Grid.RemoveChild(group.btn);
    }

    [Subscribe]
    public void onChange(IStringEntity entity)
    {
        //GD.Print($"StringEntity [{entity.entityUid}] set value: {entity.value}");
        var group = find(entity.entityUid);
        if(group != (null, null, null))
        {
            int col = group.edit.CaretColumn;
            group.edit.Text = entity.ToString();
            group.edit.CaretColumn = col;
        } 
        else
        {
            throw new Exception("What is he cooking");
        }
    }


}
