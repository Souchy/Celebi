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

        public static ICreatureModel newCreatureModel()
        {
            // Model + Skin
            var creatureModel = CreatureModel.CreatePermanent();
            // Name
            var name = StringEntity.Create("CreatureName #" + creatureModel.entityUid);
                creatureModel.nameId = name.entityUid;
                Eevee.models.i18n.Add(creatureModel.nameId, name);
            // Desc
            var desc = StringEntity.Create("CreatureDesc #" + creatureModel.entityUid);
                creatureModel.descriptionId = desc.entityUid;
                Eevee.models.i18n.Add(creatureModel.descriptionId, desc);
            // Stats
            var stats = Stats.Create();
                creatureModel.baseStats = stats.entityUid;
                Eevee.models.stats.Add(stats.entityUid, stats);
            //fillStats(stats, true);
            // Growth Stats
            var growthStats = Stats.Create();
                creatureModel.growthStats = growthStats.entityUid;
                Eevee.models.stats.Add(growthStats.entityUid, growthStats);
            //fillStats(growthStats, true);
            // Skin
            var creatureSkin = newCreatureSkin();
                creatureModel.skins.Add(creatureSkin.entityUid);
            // Eevee
                Eevee.models.creatureModels.Add(creatureModel.entityUid, creatureModel);
            return creatureModel;
        }
        public static ISpellModel newSpellModel()
        {
            // Model + Skin
            var spellModel = SpellModel.CreatePermanent();
            // Name
            var name = StringEntity.Create("SpellName #" + spellModel.entityUid);
            spellModel.nameId = name.entityUid;
            Eevee.models.i18n.Add(spellModel.nameId, name);
            // Desc
            var desc = StringEntity.Create("SpellDesc #" + spellModel.entityUid);
            spellModel.descriptionId = desc.entityUid;
            Eevee.models.i18n.Add(spellModel.descriptionId, desc);
            // Stats
            var stats = Stats.Create();
            spellModel.stats = stats.entityUid;
            Eevee.models.stats.Add(stats.entityUid, stats);
            // Eevee
            Eevee.models.spellModels.Add(spellModel.entityUid, spellModel);
            return spellModel;
        }

        private static ICreatureSkin newCreatureSkin()
        {
            var creatureSkin = CreatureSkin.Create();
            // Name
            var name = StringEntity.Create("SkinName #" + creatureSkin.entityUid);
            creatureSkin.nameId = name.entityUid;
            Eevee.models.i18n.Add(creatureSkin.nameId, name);
            // Desc
            var desc = StringEntity.Create("SkinDesc #" + creatureSkin.entityUid);
            creatureSkin.descriptionId = desc.entityUid;
            Eevee.models.i18n.Add(creatureSkin.descriptionId, desc);
            // model
            creatureSkin.meshModel = "ybot/ybot_pro_magic";
            creatureSkin.meshName = "Alpha_Surface";
            creatureSkin.animations = new()
            {
                idle = "locomotion/idle",
                run = "Armature050|mixamocom|Layer0|run forward",
                walk = "Armature054|mixamocom|Layer0|walk forward hold",
                receiveHit = "Armature043|mixamocom|Layer0|hit from front",
                victory = "Victory",
                defeat = "Defeat"
            };
            // Eevee
            Eevee.models.creatureSkins.Add(creatureSkin.entityUid, creatureSkin);
            return creatureSkin;
        }

        private static void fillStats(IStats stats, bool isModel)
        {
            // fill stats
            foreach (var statType in CharacteristicType.Characteristics) //Enum.GetValues<StatType>())
            {
                IStat s = statType.Create();
                stats.Set(s);
                //var valueType = statType.StatValueType; // statType.GetProperties().valueType;
                //// dont need current/max when making a creature model
                //if (isModel && valueType == StatValueType.Resource)
                //    stats.Add(StatValueType.Simple.Create(statType));
                //else 
                //    stats.Add(statType.Create());
            }
        }

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

                var duration = StatusModelProperty.Duration.Create(statusModel.duration.value);
                var delay = StatusModelProperty.Delay.Create(statusModel.delay.value);
                status.GetStats().Set(duration);
                status.GetStats().Set(delay);
            }
            Eevee.fights.Get(fightId).creatures.Add(crea.entityUid, crea);
            return crea;
        }

    }
}
