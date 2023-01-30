using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.util
{
    public enum IDClassType
    {
        ICreatureModel,
        ISpellModel,
        IStatusModel,
        IEffectModel,
        IEffect,
        ICreatureSkin,
        ISpellSkin,
        IEffectSkin,
        String, //i18n,

        IFightEntity,
    }
    public class UIdGenerator : IUIdGenerator
    {
        private int counter = 0;
        private HashSet<int> ids = new HashSet<int>();

        public IID next()
        {
            lock(this)
            {
                if(int.MaxValue == ids.Count) // uint.max
                {
                    throw new Exception("Too many IDs");
                }
                while (ids.Contains(counter))
                {
                    counter++;
                    if (counter == int.MaxValue) counter = 0;
                }
                ids.Add(counter);
                return (IID) counter;
            }
        }

        public bool take(IID id)
        {
            lock(this)
            {
                if (ids.Add(id))
                {
                    counter = id + 1;
                    return true;
                }
                else
                    return false;
            }
        }

        public void dispose(IID i)
        {
            lock(this)
            {
                ids.Remove(i);
            }
        }

    }
}
