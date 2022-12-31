using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.controllers
{
    public interface IPlayer : IEntity
    {
        public ITeam team { get; set; }
        public List<ICreature> creatures { get; set; }
    }
}