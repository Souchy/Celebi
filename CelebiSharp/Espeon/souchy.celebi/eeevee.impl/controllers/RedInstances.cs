using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;

namespace Espeon.souchy.celebi.eeevee.impl.controllers
{
    public class RedInstances : IRedInstances
    {
        public Dictionary<IID, IFight> fights { get; init; }
        public Dictionary<IID, ICreature> creatures { get; init; }
        public Dictionary<IID, ISpell> spells { get; init; }
    }
}
