using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.face.objects.effectResults;

namespace souchy.celebi.eevee.neweffects.face
{
    public interface IEffectScript //<T> where T : IEffectSchema
    {
        //public Type SchemaType { get => typeof(T); }
        public Type SchemaType { get; }
        public IEffectPreview preview(IAction action, IEffect e, IEnumerable<IBoardEntity> targets);
        public IEffectReturnValue apply(IAction action, IEffect e, IEnumerable<IBoardEntity> targets);
    }
}
