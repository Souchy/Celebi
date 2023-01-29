using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace Umbreon.data.resources
{
    public class SpellModel : ISpellModel
    {
        public IID entityUid { get; init; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public Dictionary<ResourceType, int> costs { get; set; }
        public ISpellProperties properties { get; set; }
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        public HashSet<IID> effectIds { get; set; }

        public SpellModel()
        {
        }

        public static SpellModel Create()
        {
            return new SpellModel()
            {
                entityUid = Eevee.RegisterIID(), //uIdGenerator.next(),
                nameId = Eevee.RegisterIID(), //uIdGenerator.next(),
                descriptionId = Eevee.RegisterIID(), //uIdGenerator.next(),
            };
        }

        public void Dispose()
        {
        }

    }
}
