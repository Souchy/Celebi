
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.face.objects
{
    public interface ITeam : IEntity
    {
        public string name { get; set; }
        //public List<ObjectId> alliedTeams { get; set; }
    }
}