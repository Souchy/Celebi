using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.conditions.creature;
using souchy.celebi.eevee.impl.shared.conditions.other;
using souchy.celebi.eevee.impl.shared.conditions.spell;
using souchy.celebi.eevee.impl.shared.conditions.status;
using souchy.celebi.eevee.impl.shared.conditions;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.shared.triggers.schemas;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.face.shared.conditions;

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
        public string name { get; init; }
        public Type schemaType { get; init; }
        public TriggerType(int id, Type schemaType)
        {
            this.id = new IID(id.ToString());
            this.name = schemaType.Name;
            this.schemaType = schemaType;
        }
        public TriggerSchema createInstance()
        {
            return (TriggerSchema) Activator.CreateInstance(schemaType);
        }

        //
        public static readonly TriggerType TriggerOnTimeline = new TriggerType(001, typeof(TriggerOnTimeline));

        public static readonly TriggerType TriggerOnSpellCast = new TriggerType(011, typeof(TriggerOnSpell));
        public static readonly TriggerType TriggerOnSpellReceive = new TriggerType(012, typeof(TriggerOnSpell));

        public static readonly TriggerType TriggerOnMove = new TriggerType(021, typeof(TriggerOnMove));
        public static readonly TriggerType TriggerOnCellMovement = new TriggerType(022, typeof(TriggerOnCellMovement));

        public static readonly TriggerType TriggerOnEffectCast = new TriggerType(031, typeof(TriggerOnEffectCast));
        public static readonly TriggerType TriggerOnEffectReceive = new TriggerType(032, typeof(TriggerOnEffectReceive));

        //
        static TriggerType() => _values.AddRange(StaticEnumUtils.findValues<TriggerType>());
        private static List<TriggerType> _values = new();
        public static TriggerType[] values() => _values.ToArray();
        public static TriggerType get(IID id) => _values.Find(v => v.id == id);
        public static TriggerType getByType(Type triggerSchemaType) => _values.Find(v => v.schemaType == triggerSchemaType);
        public static TriggerType getByName(string schemaName) => _values.Find(v => v.schemaType.Name.Equals(schemaName));

    }


}
