using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatBool : IStatBool
    {
        //public StatValueType valueType => StatValueType.Bool;

        public IID entityUid { get; set; }
        public StatType StatType { get; init; }
        public bool value { get; set; }

        public StatBool(StatType st) => this.StatType = st;
        public StatBool(StatType st, bool value) : this(st) =>  this.value = value; 

        public IStat copy() => new StatBool(StatType, value);

        public void Dispose()
        {
            Eevee.DisposeIID(this);
            throw new NotImplementedException();
        }
    }
}
