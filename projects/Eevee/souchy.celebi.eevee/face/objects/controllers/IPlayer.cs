using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.controllers
{
    public interface IPlayer : IFightEntity
    {
        public ITeam team { get; set; }
        public IEntityList<ObjectId> creatures { get; set; }
    }
}