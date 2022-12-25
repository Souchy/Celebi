using System.ComponentModel;

namespace souchy.celebi.eevee.face.filter
{
    public interface ITowerFilter
    {

        public TowerDirection direction { get; set; }
        public int origin { get; set; }
        public int maxLength { get; set; }

    }


    public enum TowerDirection
    {
        up,
        down,
        both
    }

}
