using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;
using MongoDB.Bson;

namespace souchy.celebi.eevee.impl.objects.effects
{
    /// <summary>
    /// Just nothing. As said in Interface: <br></br>
    /// Effect that doesnt do anything. <br></br>
    /// It just holds other child effects and conditions
    /// </summary>
    public class EffectEmpty : Effect, IEffectBase
    {

        private EffectEmpty() { }
        private EffectEmpty(ObjectId id) : base(id) { }

        public IID textId { get; set; }

        public static IEffectBase Create() => new EffectEmpty(Eevee.RegisterIIDTemporary());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            // just apply children, nothing else
            Mind.applyEffectContainer(action, this);
            return null;
        }
    }
}
