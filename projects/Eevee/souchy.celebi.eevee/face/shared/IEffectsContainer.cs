using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.shared
{
    public interface IEffectsContainer
    {
        public IEntityList<IID> effectIds { get; set; }
        public IEnumerable<IEffect> GetEffects() => effectIds.Values.Select(i => Eevee.models.effects.Get(i));
    }
}
