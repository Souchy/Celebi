using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{
    public abstract class TriggerOnStatUpdate : TriggerSchema
    {
        public CharacteristicId statId { get; set; }
    }

    public class TriggerOnStatUpdateSimple : TriggerOnStatUpdate
    {
        public IStat valueMin { get; set; }
        public IStat valueMax { get; set; }

        public override bool checkTrigger(IAction action, TriggerEvent triggerEvent)
        {
            return true;
        }

        public override ITriggerSchema copy()
        {
            var copy = new TriggerOnStatUpdateSimple();
            copy.statId = this.statId;
            copy.valueMin = this.valueMin;
            copy.valueMax = this.valueMax;
            return copy;
        }
    }
    public class TriggerOnStatUpdateBool : TriggerOnStatUpdate
    {
        public IStat valueMatch { get; set; }

        public override bool checkTrigger(IAction action, TriggerEvent triggerEvent)
        {
            return true;
        }

        public override ITriggerSchema copy()
        {
            var copy = new TriggerOnStatUpdateBool();
            copy.statId = this.statId;
            copy.valueMatch = this.valueMatch.copy();
            return copy;
        }
    }

}
