using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record Contextual(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Contextual, localId)
    {
        public static readonly Contextual LifeGained = new(1);
        public static readonly Contextual LifeLost = new(2);
        public static readonly Contextual ManaGained = new(3);
        public static readonly Contextual ManaUsed = new(4);
        public static readonly Contextual ManaLost = new(5);
        public static readonly Contextual MovementGained = new(6);
        public static readonly Contextual MovementUsed = new(7);
        public static readonly Contextual MovementLost = new(8);

        public static readonly Contextual CountHitsGiven = new(9);
        public static readonly Contextual CountHitsReceived = new(10);


        public static readonly Dictionary<int, Contextual> values = new();
        static Contextual()
        {
            var fields = typeof(Contextual).GetFields();
            foreach (var field in fields)
            {
                var value = (Contextual) field.GetValue(null);
                values[value.id] = value;
            }
        }
    }
}
