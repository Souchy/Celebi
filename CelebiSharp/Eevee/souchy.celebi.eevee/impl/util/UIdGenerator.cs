using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.util
{
    public class UIdGenerator : IUIdGenerator
    {
        private uint counter = 0;
        private HashSet<uint> ids = new HashSet<uint>();

        public uint next()
        {
            do
            {
                counter++;
                if (counter == int.MaxValue) counter = 0;
            } while (ids.Contains(counter));
            return counter;
        }

        public void dispose(uint i)
        {
            ids.Remove(i);
        }

    }
}
