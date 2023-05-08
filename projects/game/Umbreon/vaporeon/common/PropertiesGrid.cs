using Godot;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;
using System.Reflection;
using Timer = Godot.Timer;

namespace souchy.celebi.umbreon.vaporeon.common
{
    public static class PropertiesComponent
    {
        private static Dictionary<Type, Func<PropertyInfo, object, Action, Control>> editorFactory = new();
        //private static Dictionary<string, Timer> timers = new();


        static PropertiesComponent()
        {
            editorFactory[typeof(string)] = GeneratePropString;
            editorFactory[typeof(int)] = GeneratePropInt;
            editorFactory[typeof(IValue<int>)] = GeneratePropValueInt;
            editorFactory[typeof(IValue<bool>)] = GeneratePropValueBool;
            editorFactory[typeof(IValue<ElementType>)] = GeneratePropValueElement;
            editorFactory[typeof(IValue<StatusFusingStrategy>)] = GeneratePropValueStatusFusingStrategy;
        }

        public static Control GenerateGrid(object obj, Control container = null, Action onEdit = null)
        {
            if (container == null)
                container = new GridContainer();
            if (obj == null)
                throw new NullReferenceException(nameof(obj));
            var props = obj.GetType().GetProperties()
                    .Where(prop => editorFactory.ContainsKey(prop.PropertyType))
                    .ToList();
            for (int i = 0; i < props.Count; i++)
            {
                var prop = props[i];

                var lbl = new Label();
                lbl.Text = prop.Name;

                var edit = editorFactory[prop.PropertyType](prop, obj, onEdit);
                
                container.AddChild(lbl);
                container.AddChild(edit);
                if (container is HBoxContainer && i != props.Count - 1)
                    container.AddChild(new VSeparator());
            }
            return container;
        }

        public static Control GeneratePropInt(PropertyInfo prop, object obj, Action onEdit)
        {
            var edit = new SpinBox();
            edit.SelectAllOnFocus = true;
            edit.Rounded = true;
            edit.Value = (int) prop.GetValue(obj);
            edit.ValueChanged += (val) => prop.SetValue(obj, (int) val);
            if(onEdit != null)
                edit.ValueChanged += (val) => onEdit();
            return edit;
        }
        public static Control GeneratePropString(PropertyInfo prop, object obj, Action onEdit)
        {
            var edit = new LineEdit();
            edit.Text = (string) prop.GetValue(obj);
            edit.TextChanged += (val) => prop.SetValue(obj, val);
            if (onEdit != null)
                edit.TextChanged += (val) => onEdit();
            return edit;
        }
        public static Control GeneratePropValueInt(PropertyInfo prop, object obj, Action onEdit)
        {
            var edit = new SpinBox();
            edit.SelectAllOnFocus = true;
            edit.Rounded = true;
            var getValue = () => (IValue<int>) prop.GetValue(obj);
            edit.Value = getValue().value; // ((IValue<int>) prop.GetValue(obj)).value;
            edit.ValueChanged += (val) => getValue().value = (int) val; // prop.SetValue(obj, new Value<int>((int) val)); //((IValue<int>) prop.GetValue(obj)).Value = (int) val;
            if (onEdit != null)
                edit.ValueChanged += (val) => onEdit();
            return edit;
        }
        public static Control GeneratePropValueBool(PropertyInfo prop, object obj, Action onEdit)
        {
            var edit = new CheckBox();
            var getValue = () => (IValue<bool>) prop.GetValue(obj);
            edit.ButtonPressed = getValue().value; // ((IValue<bool>) prop.GetValue(obj)).value;
            edit.Toggled += (val) => getValue().value = val; //prop.SetValue(obj, new Value<bool>(val)); //((IValue<bool>) prop.GetValue(obj)).Value = val;
            if (onEdit != null)
                edit.Toggled += (val) => onEdit();
            edit.MouseDefaultCursorShape = Control.CursorShape.PointingHand;
            return edit;
        }
        public static Control GeneratePropValueElement(PropertyInfo prop, object obj, Action onEdit)
        {
            var edit = new OptionButton();
            foreach(var ele in Enum.GetNames<ElementType>())
                edit.AddItem(ele);
            var getValue = () => (IValue<ElementType>) prop.GetValue(obj);
            edit.Selected = (int) getValue().value; // Enum.GetValues<ElementType>().ToList().IndexOf(getValue().value);
            edit.ItemSelected += (index) => getValue().value = (ElementType) index; // Enum.GetValues<ElementType>().ToList()[(int) index];
            if (onEdit != null)
                edit.ItemSelected += (i) => onEdit();
            edit.MouseDefaultCursorShape = Control.CursorShape.PointingHand;
            return edit;
        }
        public static Control GeneratePropValueStatusFusingStrategy(PropertyInfo prop, object obj, Action onEdit)
        {
            var edit = new OptionButton();
            foreach (var ele in Enum.GetNames<StatusFusingStrategy>())
                edit.AddItem(ele);
            var getValue = () => (IValue<StatusFusingStrategy>) prop.GetValue(obj);
            edit.Selected = (int) getValue().value; // Enum.GetValues<ElementType>().ToList().IndexOf(getValue().value);
            edit.ItemSelected += (index) => getValue().value = (StatusFusingStrategy) index; // Enum.GetValues<ElementType>().ToList()[(int) index];
            if (onEdit != null)
                edit.ItemSelected += (i) => onEdit();
            edit.MouseDefaultCursorShape = Control.CursorShape.PointingHand;
            return edit;
        }

        

        public static void GenerateStat(GridContainer container, CharacteristicType statType, IStat stat) 
        {
            Label lbl = new Label();
            //Eevee.models.i18n.Get(statType.ID);
            //Eevee.RegisterIID<IStringEntity>();
            lbl.Name = "lbl:" + statType.GetName(); //Enum.GetName(statType);
            lbl.Text = statType.GetName().value; //Enum.GetName(statType);
            if (stat == null)
                stat = statType.Create();
            container.AddChild(lbl);

            var editor = Vaporeon.instanceEditor(stat);
            editor.Name = statType.GetName().value; //Enum.GetName(statType);
            container.AddChild(editor);

            if (stat is IStatSimple)
                ((EditorInitiator<IStatSimple>) editor).init((IStatSimple) stat);
            if (stat is IStatBool)
                ((EditorInitiator<IStatBool>) editor).init((IStatBool) stat);
            if (stat is IStatDetailed)
                ((EditorInitiator<IStatDetailed>) editor).init((IStatDetailed) stat);
            if (stat is IStatResource)
                ((EditorInitiator<IStatResource>) editor).init((IStatResource) stat);
        }


    }
}
