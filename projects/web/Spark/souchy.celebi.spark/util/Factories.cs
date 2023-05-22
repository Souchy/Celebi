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
using souchy.celebi.spark.services.models;
using souchy.celebi.eevee.enums.characteristics.other;

namespace souchy.celebi.spark.util
{
    public class Factories
    {

        public static async Task<(ICreatureModel crea, IStringEntity name, IStringEntity desc, IStats stats, (ICreatureSkin skin, IStringEntity name, IStringEntity desc) skin)> newCreatureModel(IDCounterService _ids)
        {
            // Model + Skin
            var creatureModel = CreatureModel.CreatePermanent();
            creatureModel.modelUid = await _ids.GetID<ICreatureModel>();

            // Name
            var name = StringEntity.Create("Creature#" + creatureModel.modelUid);
            name.modelUid = await _ids.GetID<IStringEntity>();
            creatureModel.nameId = name.entityUid;

            // Desc
            var desc = StringEntity.Create("CreatureDesc#" + creatureModel.modelUid);
            desc.modelUid = await _ids.GetID<IStringEntity>();
            creatureModel.descriptionId = desc.entityUid;

            // Base Stats
            var baseStats = CreatureStats.Create();
            creatureModel.statsId = baseStats.entityUid;
            baseStats.Add(Resource.LifeInitialMax.Create(1500));
            baseStats.Add(Resource.ManaInitialMax.Create(10));
            baseStats.Add(Resource.ManaRegen.Create(int.MaxValue));
            baseStats.Add(Resource.MovementInitialMax.Create(5));
            baseStats.Add(Resource.MovementRegen.Create(int.MaxValue));
            baseStats.Add(Resource.SummonInitialMax.Create(1));
            baseStats.Add(Resource.RageRegen.Create(int.MaxValue));
            baseStats.Add(State.Visible.Create(true));

            // Growth Stats
            //var growthStats = Stats.Create();
            //creatureModel.growthStats = growthStats.entityUid;

            // Skin
            var creatureSkin = await newCreatureSkin(_ids);
            creatureModel.skinIds.Add(creatureSkin.Item1.entityUid);

            return (creatureModel, name, desc, baseStats, creatureSkin);
        }
        public static async Task<(ISpellModel spell, IStringEntity name, IStringEntity desc, IStats stats)> newSpellModel(IDCounterService _ids)
        {
            // Model + Skin
            var spellModel = SpellModel.CreatePermanent();
            spellModel.modelUid = await _ids.GetID<ISpellModel>();

            // Name
            var name = StringEntity.Create("Spell#" + spellModel.modelUid);
            name.modelUid = await _ids.GetID<IStringEntity>();
            spellModel.nameId = name.entityUid;

            // Desc
            var desc = StringEntity.Create("SpellDesc#" + spellModel.modelUid);
            desc.modelUid = await _ids.GetID<IStringEntity>();
            spellModel.descriptionId = desc.entityUid;

            // Stats
            var stats = SpellModelStats.Create();
            spellModel.statsId = stats.entityUid;

            return (spellModel, name, desc, stats);
        }

        private static async Task<(ICreatureSkin, IStringEntity, IStringEntity)> newCreatureSkin(IDCounterService _ids)
        {
            var creatureSkin = CreatureSkin.Create();

            // Name
            var name = StringEntity.Create("SkinName #" + creatureSkin.entityUid);
            name.modelUid = await _ids.GetID<IStringEntity>();
            creatureSkin.nameId = name.entityUid;

            // Desc
            var desc = StringEntity.Create("SkinDesc #" + creatureSkin.entityUid);
            desc.modelUid = await _ids.GetID<IStringEntity>();
            creatureSkin.descriptionId = desc.entityUid;

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
            return (creatureSkin, name, desc);
        }

    }
}
