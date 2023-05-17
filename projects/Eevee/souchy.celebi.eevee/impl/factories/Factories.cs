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

namespace souchy.celebi.eevee.impl.factories
{
    public class Factories
    {
        public static ICreature newCreatureFromModel(ICreatureModel model, ObjectId fightId)
        {
            IFight fight = Eevee.fights.Get(fightId);
            var crea = Creature.Create(fightId);
            crea.modelUid = model.modelUid;
            fight.creatures.Add(crea.entityUid, crea);

            IStats stats = model.GetBaseStats().copy();
            crea.stats = stats.entityUid;
            fight.stats.Add(stats.entityUid, stats);

            foreach (ISpellModel spellModel in model.GetSpells())
            {
                ISpell spell = Spell.Create(fightId);
                spell.modelUid = spellModel.modelUid; //entityUid;
                fight.spells.Add(spell.entityUid, spell);
            }
            foreach (IStatusModel statusModel in model.GetStatusPassives())
            {
                IStatusContainer container = StatusContainer.Create(fightId);
                container.sourceCreature = crea.entityUid;
                container.holderEntity = crea.entityUid;
                fight.statuses.Add(container.entityUid, container);

                IStatusInstance status = StatusInstance.Create(fightId);
                status.modelUid = statusModel.modelUid; 
                status.EffectIds = statusModel.EffectIds;
                status.GetStats().Set(StatusProperty.Duration.Create(statusModel.duration.value));
                status.GetStats().Set(StatusProperty.Delay.Create(statusModel.delay.value));
                container.instances.Add(status);
            }
            return crea;
        }

    }
}
