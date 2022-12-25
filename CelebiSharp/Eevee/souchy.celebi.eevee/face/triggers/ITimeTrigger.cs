using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.triggers
{

    public interface ITimeTrigger : ITrigger
    {
        public MomentType Type { get; set; }


    }
}
