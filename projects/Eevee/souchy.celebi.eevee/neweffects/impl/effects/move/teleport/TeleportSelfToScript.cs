﻿using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl.effects.move.teleport
{
    public class TeleportToScript : IEffectScript
    {
        public Type SchemaType => typeof(TeleportSelfTo);

        public IEffectReturnValue apply(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }

        public IEffectPreview preview(ISubActionEffect action, IBoardEntity currentTarget, IEnumerable<IBoardEntity> allTargetsInZone)
        {
            throw new NotImplementedException();
        }
    }
}
