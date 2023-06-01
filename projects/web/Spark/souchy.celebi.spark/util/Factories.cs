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
using souchy.celebi.eevee.impl.shared.skins;
using souchy.celebi.eevee.enums.characteristics.properties;

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
            var baseStats = (IStats) CreatureStats.Create();
            creatureModel.statsId = baseStats.entityUid;
            baseStats.Add(Resource.LifeInitialMax.Create(2000));
            baseStats.Add(Resource.ManaInitialMax.Create(11));
            baseStats.Add(Resource.ManaRegen.Create(int.MaxValue));
            baseStats.Add(Resource.MovementInitialMax.Create(6));
            baseStats.Add(Resource.MovementRegen.Create(int.MaxValue));
            baseStats.Add(Resource.SummonInitialMax.Create(1));
            baseStats.Add(Resource.RageInitialMax.Create(20));
            baseStats.Add(Resource.RageRegen.Create(0));
            baseStats.Add(OtherProperty.Speed.Create(100));
            baseStats.Add(State.Visible.Create(true));

            // Growth Stats
            //var growthStats = Stats.Create();
            //creatureModel.growthStats = growthStats.entityUid;

            // Skin
            var creatureSkin = await newCreatureSkin(_ids);
            creatureModel.skinIds.Add(creatureSkin.Item1.entityUid);

            return (creatureModel, name, desc, baseStats, creatureSkin);
        }
        public static async Task<(ISpellModel spell, IStringEntity name, IStringEntity desc, IStats stats, ISpellSkin skin)> newSpellModel(IDCounterService _ids)
        {
            // Model
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
            stats.Add(SpellModelProperty.MaxCastPerTarget.Create(int.MaxValue));
            stats.Add(SpellModelProperty.MaxCastPerTurn.Create(int.MaxValue));
            stats.Add(SpellModelProperty.Cooldown.Create(0));
            stats.Add(SpellModelProperty.MaxCharges.Create(0));
            stats.Add(SpellModelProperty.LineOfSightRequired.Create(true));
            stats.Add(SpellModelProperty.BroadcastTargetedCell.Create(true));
            stats.Add(SpellModelProperty.IsRangeModifiable.Create(true));
            spellModel.statsId = stats.entityUid;

            // Skin
            var skin = newSpellSkin();
            spellModel.skinIds.Add(skin.entityUid);

            return (spellModel, name, desc, stats, skin);
        }

        public static async Task<(IStatusModel status, IStringEntity name, IStringEntity desc, IStats stats, IStatusSkin skin)> newStatusModel(IDCounterService _ids)
        {
            // Model
            var model = StatusModel.CreatePermanent();
            model.modelUid = await _ids.GetID<IStatusModel>();

            // Name
            var name = StringEntity.Create("Status#" + model.modelUid);
            name.modelUid = await _ids.GetID<IStringEntity>();
            model.nameId = name.entityUid;

            // Desc
            var desc = StringEntity.Create("StatusDesc#" + model.modelUid);
            desc.modelUid = await _ids.GetID<IStringEntity>();
            model.descriptionId = desc.entityUid;

            // Stats
            var stats = StatusModelStats.Create();
            model.statsId = stats.entityUid;

            // Skin
            var skin = newStatusSkin();
            model.skinIds.Add(skin.entityUid);

            return (model, name, desc, stats, skin);
        }
        public static IStatusSkin newStatusSkin()
        {
            var skin = StatusSkin.Create();
            return skin;
        }

        public static async Task<(ICreatureSkin, IStringEntity, IStringEntity)> newCreatureSkin(IDCounterService _ids)
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


        public static ISpellSkin newSpellSkin()
        {
            var skin = SpellSkin.Create();
            return skin;
        }

    }
}
