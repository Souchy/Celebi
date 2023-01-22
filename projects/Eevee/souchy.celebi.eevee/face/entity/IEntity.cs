using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.entity
{
    public interface IEntity : IDisposable
    {
        public IID entityUid { get; init; }


        //      (ObserverPattern)
        // UIs listen to this signal to update display
        // signal Changed(string propertyPath, object entity, object oldValue)
        //      ex path in spellmodel: "properties.cooldown"

        //[Signal]
        public delegate void OnChanged(object entity, string propertyPath, object oldValue);

        public event OnChanged Changed;

        //
        // Summary:
        //     Represents the method that handles the Godot.Range.Changed event of a Godot.Range
        //     class.
        //public event Action Changed
        //{
        //    add
        //    {
        //        Connect(SignalName.Changed, Callable.From(value));
        //    }
        //    remove
        //    {
        //        Disconnect(SignalName.Changed, Callable.From(value));
        //    }
        //}

    }
}