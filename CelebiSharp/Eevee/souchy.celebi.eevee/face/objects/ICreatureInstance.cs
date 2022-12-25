using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.stats;

namespace souchy.celebi.eevee
{
    public interface ICreatureInstance : IBoardEntity
    {
        public IPlayer player { get; set; }
        public IStats stats { get; set; }
        public List<int> spellModelIds { get; set; }
    }
}