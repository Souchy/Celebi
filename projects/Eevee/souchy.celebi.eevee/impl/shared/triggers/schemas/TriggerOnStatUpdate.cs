using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{
    public abstract class TriggerOnStatUpdate : ITriggerSchema
    {
        public CharacteristicId statId { get; set; }
    }

    public class TriggerOnStatUpdateSimple : TriggerOnStatUpdate
    {
        public IStat valueMin { get; set; }
        public IStat valueMax { get; set; }
    }
    public class TriggerOnStatUpdateBool : TriggerOnStatUpdate
    {
        public IStat valueMatch { get; set; }
    }

}
