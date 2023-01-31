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
        public IID entityUid { get; set; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public Dictionary<ResourceType, int> costs { get; set; }
        public ISpellProperties properties { get; set; }
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        public HashSet<IID> effectIds { get; set; }

        private SpellModel() { }
        public static ISpellModel Create() => new SpellModel()
        {              
            entityUid = Eevee.RegisterIID<ISpellModel>(),
        };



        public void Dispose()
        {
            Eevee.DisposeIID<ISpellModel>(entityUid);
        }

    }
}
