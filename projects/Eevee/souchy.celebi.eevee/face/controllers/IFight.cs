using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.controllers
{
    public interface IFight : IEntity
    {
        /// <summary>
        /// Each player has a team and a list of owned creatures (including ones not on the board)
        /// </summary>
        public List<IPlayer> players { get; init; }
        /// <summary>
        /// The board has every BoardEntity (cell and creatures on the board)
        /// </summary>
        public IBoard board { get; init; }
        /// <summary>
        /// 
        /// </summary>
        public int currentRound { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int currentTurn { get; set; }
    }
}