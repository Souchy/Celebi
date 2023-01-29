using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.entity
{
    public interface IFightEntity : IEntity
    {
        public IID fightUid { get; set; }

        //public IFight GetFight() => Eevee.fights.Get(fightUid); 

    }
}
