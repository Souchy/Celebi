using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.util;

namespace Espeon.souchy.celebi.eeevee.impl.models
{
    public class SpellModel : ISpellModel
    {
        public IID entityUid { get; init; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public List<ICost> costs { get; set; }
        public ISpellProperties properties { get; set; }

        public HashSet<IID> effectIds { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}