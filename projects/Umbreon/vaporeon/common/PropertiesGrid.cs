using Godot;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.values;
using System.Reflection;
using Timer = Godot.Timer;

namespace Umbreon.vaporeon.common
{
    public static class PropertiesComponent
    {
        private static Dictionary<Type, Func<PropertyInfo, object, Control>> editorFactory = new();
        private static Dictionary<string, Timer> timers = new();


        static PropertiesComponent()
        {
            editorFactory[typeof(string)] = GeneratePropString;
            editorFactory[typeof(int)] = GeneratePropInt;
            editorFactory[typeof(IValue<int>)] = GeneratePropValueInt;
            editorFactory[typeof(IValue<bool>)] = GeneratePropValueBool;
        }

        public static GridContainer GenerateGrid(object obj, GridContainer container = null)
        {
            if (container == null)
                container = new GridContainer();
            foreach (var prop in obj.GetType().GetProperties())
            {
                var lbl = new Label();
                lbl.Text = prop.Name;

                var edit = editorFactory[prop.PropertyType](prop, obj);

                container.AddChild(lbl);
                container.AddChild(edit);
            }
            return container;
        }

        public static void debounce(string id, Action action)
        {
            if(!timers.ContainsKey(id))
            {
                timers[id] = new Timer();
            }
            timers[id].Timeout += action;
            timers[id].Start(1);
            timers[id].Paused = false;
        }

        public static Control GeneratePropInt(PropertyInfo prop, object obj)
        {
            var edit = new SpinBox();
            edit.Value = (int) prop.GetValue(obj);
            edit.ValueChanged += (val) => prop.SetValue(obj, val);
            edit.ValueChanged += (val) => debounce(VaporeonSignals.save, () => edit.EmitSignal(VaporeonSignals.save));
            return edit;
        }
        public static Control GeneratePropString(PropertyInfo prop, object obj)
        {
            var edit = new LineEdit();
            edit.Text = (string) prop.GetValue(obj);
            edit.TextChanged += (val) => prop.SetValue(obj, val);
            edit.TextChanged += (val) => debounce(VaporeonSignals.save, () => edit.EmitSignal(VaporeonSignals.save));
            return edit;
        }
        public static Control GeneratePropValueInt(PropertyInfo prop, object obj)
        {
            var edit = new SpinBox();
            edit.Value = ((IValue<int>) prop.GetValue(obj)).value;
            edit.ValueChanged += (val) => prop.SetValue(obj, new Value<int>((int) val)); //((IValue<int>) prop.GetValue(obj)).Value = (int) val;
            edit.ValueChanged += (val) => debounce(VaporeonSignals.save, () => edit.EmitSignal(VaporeonSignals.save));
            return edit;
        }
        public static Control GeneratePropValueBool(PropertyInfo prop, object obj)
        {
            var edit = new CheckBox();
            edit.ButtonPressed = ((IValue<bool>) prop.GetValue(obj)).value;
            edit.Toggled += (val) => prop.SetValue(obj, new Value<bool>(val)); //((IValue<bool>) prop.GetValue(obj)).Value = val;
            edit.Toggled += (val) => debounce(VaporeonSignals.save, () => edit.EmitSignal(VaporeonSignals.save));
            return edit;
        }



        public static void GenerateStat(GridContainer container, StatType statType, IStat stat = null)
        {
            Label lb = new Label();
            lb.Text = Enum.GetName(statType);
            //IStat stat = statType.Create();
            if (stat == null)
                stat = statType.Create();

            Button btn = new Button();
            btn.ButtonUp += () =>
            {
                var pop = new PopupPanel();
                var grid = GenerateGrid(stat);
                //grid.anchor
                pop.AddChild(grid);
                pop.Show();
                container.AddChild(pop);
            };
        }

    }
}
