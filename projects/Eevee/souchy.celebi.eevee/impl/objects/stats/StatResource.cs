using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.stats;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatResource : IStatResource
    {
        public IID entityUid { get; set; }
        public CharacteristicId statId { get; init; }


        private int _current, _currentMax, _initialMax;
        public int current { get => _current;
            set
            {
                _current = value;
                this.GetEntityBus()?.publish(statId, this);
                this.GetEntityBus()?.publish(this);
            }
        }
        public int currentMax { get => _currentMax;
            set
            {
                _currentMax = value;
                this.GetEntityBus()?.publish(statId, this);
                this.GetEntityBus()?.publish(this);
            }
        }
        public int initialMax { get => _initialMax;
            set
            {
                _initialMax = value;
                this.GetEntityBus()?.publish(statId, this);
                this.GetEntityBus()?.publish(this);
            }
        }

        public (int current, int currentMax, int initialMax) value { 
            get => (current, currentMax, initialMax);
            set 
            {
                this._current = value.current;
                this._currentMax = value.currentMax;
                this._initialMax = value.initialMax;
                this.GetEntityBus()?.publish(statId, this);
                this.GetEntityBus()?.publish(this);
            }
        }

        private StatResource() { }
        public static StatResource Create(CharacteristicId st, int current = 0, int currentMax = 0, int initialMax = 0) => new StatResource()
        {
            statId = st,
            current = current,
            currentMax = currentMax,
            initialMax = initialMax,
            entityUid = Eevee.RegisterIID<IStatResource>()
        };

        public void Add(IStat s)
        {
            if (s is StatResource b)
            {
                this.current += b.current;
                this.initialMax += b.initialMax;
                this.currentMax += b.currentMax;
            }
        }

        public IStat copy() => Create(statId, current, currentMax, initialMax);

        public void Dispose()
        {
            Eevee.DisposeIID<IStatResource>(entityUid);
        }
    }

}
