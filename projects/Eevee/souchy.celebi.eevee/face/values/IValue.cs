﻿using Newtonsoft.Json.Linq;

namespace souchy.celebi.eevee.face.values
{
    public interface IValue<T>
    {
        public T value { get; set; }

    }
}