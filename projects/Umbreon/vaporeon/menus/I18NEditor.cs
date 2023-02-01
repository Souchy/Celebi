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

    [NodePath]
    public GridContainer Grid { get; set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this.OnReady();
        GD.Print("I18N Editor reader");
        Eevee.models.i18n.GetEntityBus().subscribe(this);
        Eevee.models.i18n.ForEach((k, v) => onI18nAdd(Eevee.models.i18n, k, v));
    }

    private (Label lbl, LineEdit edit) find(IID key)
    {
        if (!Grid.HasNode("l" + key.ToString()))
            return (null, null);
        if (!Grid.HasNode("e" + key.ToString()))
            throw new Exception("if e is null, then l should have been null before."); //return (null, null);
        return (
            (Label) Grid.GetNode("l" + key.ToString()),
            (LineEdit) Grid.GetNode("e" + key.ToString())
        );
    }

    [Subscribe("Add")]
    public void onI18nAdd(object dic, IID key, IStringEntity value)
    {
        var lbl = new Label();
        lbl.Text = key.ToString();
        lbl.Name = "l" + key.ToString();
        lbl.CustomMinimumSize = new Vector2(50, 0);
        lbl.HorizontalAlignment = HorizontalAlignment.Fill;
        Grid.AddChild(lbl);
        var edit = new LineEdit();
        edit.Text = value.ToString();
        edit.Name = "e" + key.ToString();
        edit.TextChanged += (txt) => value.value = txt; //Eevee.models.i18n.Set(key, StringEntity.Create(txt));
        edit.CaretBlink = true;
        //edit.ExpandToTextLength = true;
        edit.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        Grid.AddChild(edit);
        value.GetEntityBus().subscribe(this, nameof(onChange));
    }
    [Subscribe("Set")]
    public void onI18nSet(object dic, IID key, IStringEntity value)
    {
        var group = find(key);
        if(group != (null, null))
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
    }

    [Subscribe]
    public void onChange(IStringEntity entity)
    {
        GD.Print($"StringEntity [{entity.entityUid}] set value: {entity.value}");
        var group = find(entity.entityUid);
        if(group != (null, null))
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
