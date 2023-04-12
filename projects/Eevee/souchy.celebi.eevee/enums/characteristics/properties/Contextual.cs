using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record Contextual : CharacteristicType
    {
        public Contextual(int localId, string name) : base(CharacteristicCategory.Contextual, localId, name, SimpleFactory)
        {
            this.StatValueType = StatValueType.Simple;
        }

        public static readonly Contextual LifeGained        = new(1, nameof(LifeGained));
        public static readonly Contextual LifeLost          = new(2, nameof(LifeLost));
        public static readonly Contextual ManaGained        = new(3, nameof(True));
        public static readonly Contextual ManaUsed          = new(4, nameof(True));
        public static readonly Contextual ManaLost          = new(5, nameof(True));
        public static readonly Contextual MovementGained    = new(6, nameof(True));
        public static readonly Contextual MovementUsed      = new(7, nameof(True));
        public static readonly Contextual MovementLost      = new(8, nameof(True));

        public static readonly Contextual CountHitsGiven    = new(9, nameof(True));
        public static readonly Contextual CountHitsReceived = new(10, nameof(True));


        public static readonly Dictionary<CharacteristicId, Contextual> values = new();
        static Contextual()
        {
            var fields = typeof(Contextual).GetFields();
            foreach (var field in fields)
            {
                var value = (Contextual) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
