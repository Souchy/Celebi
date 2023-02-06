using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.impl.shared
{
    public class SpellModel : ISpellModel
    {
        public IID entityUid { get; set; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public Dictionary<ResourceType, int> costs { get; set; } = new();
        public ISpellProperties properties { get; set; } //= new ISpellProperties();
        public HashSet<IID> effectIds { get; set; } = new();

        private SpellModel() { }
        private SpellModel(IID id) => entityUid = id;
        public static ISpellModel Create()
        {
            var model = new SpellModel(Eevee.RegisterIID<ISpellModel>());
            foreach (var resType in Enum.GetValues<ResourceType>())
                model.costs.Add(resType, 0);
            return model;
        }



        public void Dispose()
        {
            Eevee.DisposeIID<ISpellModel>(entityUid);
        }

    }
}
