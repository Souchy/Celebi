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

namespace souchy.celebi.eevee.impl.factories
{
    public class Factories
    {

        public static void newCreatureModel()
        {
            // Model + Skin
            var creatureModel = CreatureModel.Create();
            var creatureSkin = CreatureSkin.Create();
            creatureModel.skins.Add(creatureSkin.entityUid);
            // Name
            var name = StringEntity.Create("CreatureName #" + creatureSkin.entityUid);
            creatureSkin.nameId = name.entityUid;
            Eevee.models.i18n.Add(creatureSkin.nameId, name);
            // Desc
            var desc = StringEntity.Create("CreatureDesc #" + creatureSkin.entityUid);
            creatureSkin.descriptionId = desc.entityUid;
            Eevee.models.i18n.Add(creatureSkin.descriptionId, desc);
            // Stats
            var stats = Stats.Create();
            creatureModel.baseStats = stats.entityUid;
            Eevee.models.stats.Add(stats.entityUid, stats);
            fillStats(stats, true);
            // Growth Stats
            var growthStats = Stats.Create();
            creatureModel.growthStats = growthStats.entityUid;
            Eevee.models.stats.Add(growthStats.entityUid, growthStats);
            fillStats(growthStats, true);
            // add Model + Skin
            Eevee.models.creatureSkins.Add(creatureSkin.entityUid, creatureSkin);
            Eevee.models.creatureModels.Add(creatureModel.entityUid, creatureModel);
        }

        private static void fillStats(IStats stats, bool isModel)
        {
            // fill stats
            foreach (var statType in Enum.GetValues<StatType>())
            {
                var valueType = statType.GetProperties().valueType;
                // dont need current/max when making a creature model
                if (isModel && valueType == StatValueType.Resource)
                    stats.Add(StatValueType.Simple.Create(statType));
                else 
                    stats.Add(statType.Create());
            }
        }

        public static ICreature newCreatureFromModel(ICreatureModel model, IID fightId)
        {
            var crea = Creature.Create();
            foreach (var st in Enum.GetValues<StatType>())
            {
                var stat = st.Create();
                var modelStat = model.GetBaseStats().Get(st);
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
                crea.GetStats().Set(st, stat);
            }
            foreach (var spellModel in model.GetSpells())
            {
                var spell = Spell.Create(fightId);
                spell.modelUid = spellModel.entityUid;
            }
            foreach (var statusModel in model.GetStatusPassives())
            {
                var status = Status.Create(fightId);
                status.modelUid = statusModel.entityUid;
                status.sourceCreature = crea.entityUid;
                status.holderEntity = crea.entityUid;
                status.effectIds = statusModel.effectIds;
                status.duration.value = statusModel.duration.value;
                status.delay.value = statusModel.delay.value;
            }
            Eevee.fights.Get(fightId).creatures.Add(crea.entityUid, crea);
            return crea;
        }

    }
}
