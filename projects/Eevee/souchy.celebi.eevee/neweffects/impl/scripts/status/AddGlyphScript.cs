using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl.scripts.status
{
    public class AddGlyphScript : IEffectScript
    {
        public Type SchemaType => typeof(AddGlyph);

        public IEffectReturnValue apply(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            return null;
        }

        public IEffectPreview preview(ISubActionEffectTarget action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            return null;
        }
    }
}
