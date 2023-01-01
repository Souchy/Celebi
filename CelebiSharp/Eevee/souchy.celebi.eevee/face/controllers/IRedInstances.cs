using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.controllers
{
    public interface IRedInstances
    {

        public Dictionary<IID, ICreature> creatures { get; init; }
        public Dictionary<IID, ISpell> spells { get; init; }
        public Dictionary<IID, IEffect> effects { get; init; }



    }
}
