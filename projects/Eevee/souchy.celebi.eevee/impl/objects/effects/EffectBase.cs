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

namespace souchy.celebi.eevee.impl.objects.effects
{
    /// <summary>
    /// Just nothing. As said in Interface: <br></br>
    /// Effect that doesnt do anything. <br></br>
    /// It just holds other child effects and conditions
    /// </summary>
    public class EffectBase : Effect, IEffectBase
    {

        private EffectBase() { }
        private EffectBase(IID id) : base(id) { }

        public IID textId { get; set; }

        public static IEffectBase Create() => new EffectBase(Eevee.RegisterIID<IEffect>());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            // just compile children
            throw new NotImplementedException();
        }
    }
}
