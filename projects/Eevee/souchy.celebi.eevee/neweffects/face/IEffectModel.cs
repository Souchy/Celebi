using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models;


namespace souchy.celebi.eevee.neweffects.face
{
    /// <summary>
    /// Just name, description and wheter the effect targets creatures or cells + the EffectSchema Type
    /// </summary>
    public interface IEffectModel : IEntityModel
    {
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public BoardTargetType BoardTargetType { get; set; }
        public Type SchemaType { get; init; }

        
        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId);
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId);
    }
}
