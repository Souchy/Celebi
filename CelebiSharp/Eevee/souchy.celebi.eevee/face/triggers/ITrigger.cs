using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.face.triggers
{
    public interface ITrigger
    {
        /// <summary>
        /// Filter what kind of creature can proc this trigger (observation subject)
        /// </summary>
        //public ITargetFilter triggererFilter { get; set; }
        //public IBoardEntity subject { get; set; }
        public ICondition triggererFilter { get; set; }


        public ICondition triggerConditions { get; set; }

    }
}
