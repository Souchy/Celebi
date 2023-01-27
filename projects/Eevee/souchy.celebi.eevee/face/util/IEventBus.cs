using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.face.util
{
    public interface IEventBus : IDisposable // : IEntity
    {

        public void subscribe(object subscriber, params string[] methodNames);
        public void unsubscribe(object subscriber, params string[] methodNames);
        public void publish(string path = "", params object[] param);

    }
}
