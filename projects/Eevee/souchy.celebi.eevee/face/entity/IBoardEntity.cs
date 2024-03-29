﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl;
using System;

namespace souchy.celebi.eevee.face.entity
{
    /// <summary>
    /// BoardEntities are Cells and Creatures
    /// </summary>
    public interface IBoardEntity : IEntityModeled, IFightEntity
    {
        public IPosition position { get; init; }
        /// <summary>
        /// StatusContainers
        /// </summary>
        public IEntitySet<ObjectId> statuses { get; init; } 

        public Dictionary<ContextType, IContext> contexts { get; set; }


        public IEnumerable<IStatusContainer> GetStatuses() => this.GetFight().statuses.Values.Where(s => statuses.Contains(s.entityUid));
        public ICell GetCell() => this.GetFight().cells.Values/*.board.GetCells()*/.First(c => c.position.equals(this.position));

    }
}