using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl
{
    public abstract class Effect : IEffect
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public IEffectSchema Schema { get; set; }
        public ICondition SourceCondition { get; set; }
        public ICondition TargetFilter { get; set; }
        public IZone TargetAcquisitionZone { get; set; } = new Zone();
        public IEntityList<ITriggerModel> Triggers { get; set; } = new EntityList<ITriggerModel>();
        public IEntityList<ObjectId> EffectIds { get; set; } = new EntityList<ObjectId>();


        public abstract IEnumerable<IEffect> GetEffects();

        public void CopyBasicTo(IEffectInstance e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO use effect model to get the boardTargetType etc
        /// </summary>
        public IEnumerable<IBoardEntity> GetPossibleBoardTargets(IAction action, IPosition targetCell)
        {
            //var model = ((IEffect) this).GetModel();
            //var boardTargetType = model.BoardTargetType;

            // use board cells + effect acquisitionZone

            var points = TargetAcquisitionZone.GeneratePoints();
            points.ForEach(p => p.add(targetCell));
            var cells = points.Select(p => action.fight.cells.Values.FirstOrDefault(c => c.position.equals(p)))
                .Where(c => c != null)
                .ToList();

            List<IBoardEntity> result;
            if(true) // model.BoardTargetType == creatures
            {
                result = cells.Select(cell => action.fight.creatures.Values.FirstOrDefault(crea => crea.position.equals(cell.position)))
                    .Where(c => c != null)
                    .ToList<IBoardEntity>();
                // TODO conditions here or somewhere else?
                //      i think here but there was a different plan before, hence the "possible" targets name
                // result = result.Where(crea => TargetFilter.check(action, null, action.getCaster(), crea));
            } else
            {
                result = cells.ToList<IBoardEntity>();
            }

            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
