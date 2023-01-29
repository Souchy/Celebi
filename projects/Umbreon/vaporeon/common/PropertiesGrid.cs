using Godot;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.values;
using System.Reflection;
using Timer = Godot.Timer;

namespace Umbreon.vaporeon.common
{
    public static class PropertiesComponent
    {
        private static Dictionary<Type, Func<PropertyInfo, object, Control>> dic = new();
        private static Dictionary<string, Timer> timers = new();


        static PropertiesComponent()
        {
            dic[typeof(string)] = GeneratePropString;
            dic[typeof(int)] = GeneratePropInt;
            dic[typeof(IValue<int>)] = GeneratePropValueInt;
            dic[typeof(IValue<bool>)] = GeneratePropValueBool;
        }

        public static void GenerateGrid(GridContainer container, Type type, object obj)
        {
            foreach (var prop in type.GetProperties())
            {
                var lbl = new Label();
                lbl.Text = prop.Name;

                var edit = dic[prop.PropertyType](prop, obj);

                container.AddChild(lbl);
                container.AddChild(edit);
            }
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

    }
}
