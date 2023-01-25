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
        /// <summary>
        /// If add to a Dictionary: newValue = object and oldValue = dictionary
        /// If remove from a Dictionary: newValue = dictionary and oldValue = object
        /// </summary>
        public delegate void OnChanged(Type propertyType, string propertyPath, object newValue, object oldValue);
        public event OnChanged Changed;
        public void TriggerChanged(Type propertyType, string propertyPath, object newValue, object oldValue); // => Changed(propertyType, propertyPath, newValue, oldValue);

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