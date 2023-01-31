using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl;
using Umbreon.data.resources;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Umbreon.eevee.impl.objects
{
    public class Creature : ICreature
    {
        public IID entityUid { get; set; }
        public IID modelUid { get; set; }
        public IID fightUid { get; set; }

        public IID originalOwnerUid { get; set; }
        public IID currentOwnerUid { get; set; }
        public IID stats { get; set; }
        public List<IID> spells { get; set; }
        public IPosition position { get; init; }
        public List<IID> statuses { get; init; }
        public Dictionary<ContextType, IContext> contexts { get; set; }


        private Creature() { }
        private Creature(IID id) => entityUid = id;
        public static ICreature Create() => new Creature(Eevee.RegisterIID<ICreature>());

        public void Dispose()
        {
            Eevee.DisposeIID<ICreature>(entityUid);
            throw new NotImplementedException();
        }
    }
}
