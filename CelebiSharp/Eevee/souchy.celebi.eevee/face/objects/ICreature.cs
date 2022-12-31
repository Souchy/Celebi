using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;

namespace souchy.celebi.eevee
{
    public interface ICreature : IBoardEntity
    {
        public IID player { get; set; }
        public IStats stats { get; set; }
        public List<IID> spellModelIds { get; set; }
    }
}