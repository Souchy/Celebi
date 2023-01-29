﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.effects.spell;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.eevee.impl.objects
{
    public class Cell : ICell
    {
        public IID entityUid { get; set; }
        public IID modelUid { get; set; }
        public IID fightUid { get; set; }
        public bool walkable { get; set; }
        public bool blocksLos { get; set; }
        public IPosition position { get; init; }
        public List<IID> statuses { get; init; }
        public Dictionary<ContextType, IContext> contexts { get; set; }

        public Cell() { }
        public Cell(IID id) => this.entityUid = id;
        public static ICell Create() => new Cell(Eevee.RegisterIID());

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
