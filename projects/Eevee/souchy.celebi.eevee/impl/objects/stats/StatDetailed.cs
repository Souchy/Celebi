using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatDetailed : IStatDetailed
    {
        public CharacteristicId statId { get; init; }
        public IID entityUid { get; set; }


        private int _baseFlat, _increasedPercent, _increasedFlat, _morePercent;
        public int baseFlat { get => _baseFlat; 
            set
            {
                _baseFlat = value;
                this.GetEntityBus()?.publish(statId, this);
                this.GetEntityBus()?.publish(this);
            }
        }
        public int increasedPercent { get => _increasedPercent; 
            set
            {
                _increasedPercent = value;
                this.GetEntityBus()?.publish(statId, this);
                this.GetEntityBus()?.publish(this);
            }
        }
        public int increasedFlat { get => _increasedFlat; 
            set
            {
                _increasedFlat = value;
                this.GetEntityBus()?.publish(statId, this);
                this.GetEntityBus()?.publish(this);
            }
        }
        public int morePercent { get => _morePercent;
            set
            {
                _morePercent = value;
                this.GetEntityBus()?.publish(statId, this);
                this.GetEntityBus()?.publish(this);
            }
        }

        public int value { 
            get {
                double step1 = 1 + baseFlat * increasedPercent / 100d;
                double step2 = step1 + increasedFlat;
                double step3 = 1 + step2 * morePercent / 100d;
                return (int) step3;
            }
            set {
                throw new Exception("Dont set the final value in IStatDetailed. Rather set the individual components.");
            }
        }


        private StatDetailed() { }
        public static StatDetailed Create(CharacteristicId st, int baseFlat = 0, int increasedPercent = 0, int increasedFlat = 0, int morePercent = 0)
            => new StatDetailed() 
            {
                statId = st,
                baseFlat = baseFlat,
                increasedPercent = increasedPercent,
                increasedFlat = increasedFlat,
                morePercent = morePercent,
                entityUid = Eevee.RegisterIID<IStatDetailed>()
            };

        public IStat copy() => Create(statId, baseFlat, increasedPercent, increasedFlat, morePercent);

        public void Add(IStat s)
        {
            if (s is StatDetailed b)
            {
                this.baseFlat += b.baseFlat;
                this.increasedFlat += b.increasedFlat;
                this.increasedPercent += b.increasedPercent;
                this.morePercent += b.morePercent;
            }
        }

        public void Dispose()
        {
            Eevee.DisposeIID<IStatDetailed>(entityUid);
        }
    }
}
