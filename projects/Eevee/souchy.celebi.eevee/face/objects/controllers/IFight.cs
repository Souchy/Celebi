using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.controllers
{
    /// <summary>
    /// ----------------- PREVIOUSLY RedInstances ----------------------
    /// 
    /// so idk, we can do 2 things:
    /// - 1 Red instance per fight
    /// - 1 Red singleton for all fights
    /// 
    /// We could say all of this could go inside of IFight
    /// </summary>
    public interface IFight : IEntity 
    {
        public ITimeline timeline { get; set; }
        public IBoard board { get; set; }
        public Dictionary<IID, IPlayer> players { get; set; }
        public Dictionary<IID, ICreature> creatures { get; init; }
        public Dictionary<IID, ISpell> spells { get; init; }
        public Dictionary<IID, IStatus> statuses { get; init; }
        public Dictionary<IID, ICell> cells { get; init; }
        public Dictionary<IID, IStats> stats { get; init; }
    }
}
