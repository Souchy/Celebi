using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.effects.compiledeffects
{
    public interface ICompiledEffect
    {
        public IID sourceID { get; set; }
        public IID targetID { get; set; }

        public IID spellID { get; set; }
        public IID effectModelID { get; set; }
        public IID effectInstanceID { get; set; }
    }
}
