﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.values;
using System.Reflection.Metadata.Ecma335;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatResource : IStatResource
    {
        public StatValueType type => StatValueType.Resource;

        public int current { get; set; }
        public int currentMax { get; set; }
        public int initialMax { get; set; }

        public (int current, int currentMax, int initialMax) Value { 
            get => (current, currentMax, initialMax); 
            set {
                this.current = value.current;
                this.currentMax = value.currentMax;
                this.initialMax = value.initialMax;
            }
        }

        public int getMissing() => currentMax - current;
    }

}