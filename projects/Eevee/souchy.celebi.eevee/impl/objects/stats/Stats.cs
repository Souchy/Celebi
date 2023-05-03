using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class Stats : EntityDictionary<CharacteristicId, IStat>, IStats
    {
        private Stats() { }
        private Stats(IID id) => entityUid = id;
        public static new IStats Create() => new Stats(Eevee.RegisterIID<IStats>());

        public T Get<T>(CharacteristicId statId) where T : IStat
        {
            return (T) Get(statId);
        }

        public IStats anonymousCopy()
        {
            var c = new Stats();
            foreach(var s in this) 
                c.Set(s.Key, s.Value);
            return c;
        }

    }
}