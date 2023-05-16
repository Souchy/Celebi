using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics;
using System.Xml.Linq;

namespace souchy.celebi.eevee.impl.factories
{
    public class Factories
    {
        public static ICreature newCreatureFromModel(ICreatureModel model, ObjectId fightId)
        {
            var crea = Creature.Create(fightId);
            crea.modelUid = model.modelUid;
            foreach (CharacteristicType st in CharacteristicType.Characteristics) //Enum.GetValues<StatType>())
            {
                var stat = st.Create();
                var modelStat = model.GetBaseStats().Get(st.ID);
                if (stat is IStatResource sr)
                {
                    var mss = ((IStatSimple) modelStat);
                    sr.value = (mss.value, mss.value, mss.value);
                }
                else
                if (stat is IStatSimple ss)
                {
                    ss.value = ((IStatSimple) modelStat).value;
                }
                else
                if (stat is IStatBool sb && modelStat is IStatBool msb)
                {
                    sb.value = msb.value;
                }
                crea.GetBaseStats().Set(st.ID, stat);
            }
            foreach (ISpellModel spellModel in model.GetSpells())
            {
                var spell = Spell.Create(fightId);
                spell.modelUid = spellModel.modelUid; //entityUid;
            }
            foreach (IStatusModel statusModel in model.GetStatusPassives())
            {
                var container = StatusContainer.Create(fightId);
                container.sourceCreature = crea.entityUid;
                container.holderEntity = crea.entityUid;
                var status = StatusInstance.Create(fightId);
                status.modelUid = statusModel.modelUid; //entityUid;
                status.effectIds = statusModel.effectIds;

                var duration = StatusProperty.Duration.Create(statusModel.duration.value);
                var delay = StatusProperty.Delay.Create(statusModel.delay.value);
                status.GetStats().Set(duration);
                status.GetStats().Set(delay);
            }
            Eevee.fights.Get(fightId).creatures.Add(crea.entityUid, crea);
            return crea;
        }

    }
}
