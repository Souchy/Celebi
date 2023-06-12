﻿using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.values
{
    public class ValueMinMax<T> : IValueMinMax<T>
    {
        public ValueMinMax(T min, T max)
        {
            if (min == null) throw new ArgumentNullException(nameof(min));
            if (max == null) throw new ArgumentNullException(nameof(max));
            this.min = min;
            this.max = max;
        }

        public T min { get; set; }
        public T max { get; set; }

        public (T min, T max) value
        {
            get => (min, max);
            set
            {
                min = value.min;
                max = value.max;
            }
        }

    }
}