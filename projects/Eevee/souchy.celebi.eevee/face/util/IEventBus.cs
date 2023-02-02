using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.face.util
{
    public interface IEventBus : IDisposable // : IEntity
    {
        /// <summary>
        /// Event path to save an entity
        /// </summary>
        public const string save = "save";

        //public void subscribe<T>(string path, Action<T> action);
        public void subscribe(object subscriber, params string[] methodNames);
        public void unsubscribe(object subscriber, params string[] methodNames);
        public void publish(string path = "", params object[] param);
        public void publish(params object[] param);

    }
}
