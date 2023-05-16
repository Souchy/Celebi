using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.neweffects.impl
{
    public class EffectModel : IEffectModel
    {
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public Type SchemaType { get; init; }
        public BoardTargetType BoardTargetType { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
