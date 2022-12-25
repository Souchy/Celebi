using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.controllers
{
    public interface IFight : IEntity
    {
        public IBoard board { get; init; }

        public List<IPlayer> players { get; init; }
    }
}