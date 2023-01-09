using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.stats
{
    public interface IStatDetailed : IStat, IValue<int>
    {

        public int baseFlat { get; set; }
        public int increasedPercent { get; set; }
        public int increasedFlat { get; set; }
        public int morePercent { get; set; }

    }
}
