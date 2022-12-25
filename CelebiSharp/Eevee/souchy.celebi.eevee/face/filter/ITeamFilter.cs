namespace souchy.celebi.eevee.face.filter
{
    public interface ITeamFilter
    {

        public bool allowAlly { get; set; }
        public bool allowEnemy { get; set; }
        public bool allowSelf { get; set; }

    }
}
