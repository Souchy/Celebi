﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStats : IEntity
    {
        public IEntityDictionary<StatType, IStat> stats { get; init; }


        public T get<T>(StatType statId) where T : IStat;

        public void set(StatType statId, IStat value);

    }
}
