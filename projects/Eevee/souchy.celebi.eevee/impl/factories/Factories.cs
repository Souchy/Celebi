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
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.enums.characteristics.other;

namespace souchy.celebi.eevee.impl.factories
{
    public class Factories
    {
        public static ICreature newCreatureFromModel(ICreatureModel model, ObjectId fightId)
        {
            IFight fight = Eevee.fights.Get(fightId);
            var crea = Creature.Create(fightId);
            crea.modelUid = model.modelUid;

            IStats stats = model.GetBaseStats().copy();
            crea.statsId = stats.entityUid;

            fight.stats.Add(stats.entityUid, stats);
            fight.creatures.Add(crea.entityUid, crea);

            foreach (ISpellModel spellModel in model.GetSpells())
            {
                ISpell spell = Spell.Create(fightId);
                spell.modelUid = spellModel.modelUid; //entityUid;
                var newStats = SpellStats.Create();
                spell.statsId = newStats.entityUid;

                fight.stats.Add(newStats.entityUid, newStats);
                fight.spells.Add(spell.entityUid, spell);
            }
            foreach (IStatusModel statusModel in model.GetStatusPassives())
            {
                IStatusContainer container = StatusContainer.Create(fightId);
                container.sourceCreature = crea.entityUid;
                container.holderEntity = crea.entityUid;

                IStatusInstance status = StatusInstance.Create(fightId);
                status.modelUid = statusModel.modelUid; 
                status.EffectIds = statusModel.EffectIds;

                //status.GetStats().Set(StatusProperty.Duration.Create(statusModel.duration.value));
                //status.GetStats().Set(StatusProperty.Delay.Create(statusModel.delay.value));
                var newStats = statusModel.GetStats().copy();
                status.statsId = newStats.entityUid;

                fight.stats.Add(newStats.entityUid, newStats);
                fight.statuses.Add(container.entityUid, container);


                container.instances.Add(status);
            }
            return crea;
        }

    }
}
