﻿using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.util
{
    public class UIdGenerator : IUIdGenerator
    {
        private uint counter = 0;
        private HashSet<uint> ids = new HashSet<uint>();

        public IID next()
        {
            if(int.MaxValue == ids.Count) // uint.max
            {
                throw new Exception("Too many IDs");
            }
            do
            {
                counter++;
                if (counter == int.MaxValue) counter = 0;
            } while (ids.Contains(counter));
            return (IID) counter;
        }

        public void dispose(IID i)
        {
            ids.Remove(i);
        }

    }
}