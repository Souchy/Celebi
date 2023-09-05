using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.conditions.creature;
using souchy.celebi.eevee.impl.shared.conditions.other;
using souchy.celebi.eevee.impl.shared.conditions.spell;
using souchy.celebi.eevee.impl.shared.conditions.status;
using souchy.celebi.eevee.impl.shared.conditions;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.shared.triggers.schemas;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.enums
{
    /*
    public enum TriggerType
    {
        // simple MomentType, no parameter
        OnFightStart,
        OnFightEnd,
        OnRoundStart,
        OnRoundEnd,
        OnTurnStart,
        OnTurnEnd,
        OnTurnPass,

        // complex, based on an effect, 
        //OnCreatureSwapIn,
        //OnCreatureSwapOut,
        // complex, could filter on the creature id, the spell id, the 
        OnCreatureSpellCast,
        OnEffect,

        OnCreatureWalkEnterCell,
        OnCreatureWalkExitCell,
        OnCreatureWalkStopCell,

        //CompileStats,
    }
    */

    public enum TriggerOrderType {
        Before,
        Apply,
        After
    }

    public sealed record TriggerType
    {
        public IID id { get; init; }
        public Type schemaType { get; init; }
        public TriggerType(int id, Type schemaType)
        {
            this.id = new IID(id.ToString());
            this.schemaType = schemaType;
        }
        public ITriggerSchema createInstance()
        {
            return (ITriggerSchema) Activator.CreateInstance(schemaType);
        }

        //
        public static readonly TriggerType TriggerOnTimeline = new TriggerType(001, typeof(TriggerOnTimeline));

        public static readonly TriggerType TriggerOnSpell = new TriggerType(101, typeof(TriggerOnSpell));
        public static readonly TriggerType TriggerOnMove = new TriggerType(102, typeof(TriggerOnMove));
        public static readonly TriggerType TriggerOnCellMovement = new TriggerType(103, typeof(TriggerOnCellMovement));
        public static readonly TriggerType TriggerOnEffectCast = new TriggerType(104, typeof(TriggerOnEffectCast));
        public static readonly TriggerType TriggerOnEffectReceive = new TriggerType(105, typeof(TriggerOnEffectReceive));

        //
        private static List<TriggerType> _values = new();
        static TriggerType() => _values.AddRange(StaticEnumUtils.findValues<TriggerType>());
        public static IEnumerable<TriggerType> values() => _values.ToArray();
        public static TriggerType get(IID id) => _values.Find(v => v.id == id);
        public static TriggerType getByType(Type triggerSchemaType) => _values.Find(v => v.schemaType == triggerSchemaType);
    }


}
