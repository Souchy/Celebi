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
        public Contextual(int localId, string name, StatValueType valueType = StatValueType.Simple) : base(CharacteristicCategory.Contextual, localId, name)
        {
            this.StatValueType = valueType;
            this.Factory = SimpleFactory;
        }

        public static readonly Contextual LifeGained         = new(0, nameof(LifeGained));
        public static readonly Contextual ListUsed           = new(1, nameof(LifeLost));
        public static readonly Contextual LifeLost           = new(2, nameof(LifeLost));
        public static readonly Contextual ManaGained         = new(3, nameof(ManaGained));
        public static readonly Contextual ManaUsed           = new(4, nameof(ManaUsed));
        public static readonly Contextual ManaLost           = new(5, nameof(ManaLost));
        public static readonly Contextual MovementGained     = new(6, nameof(MovementGained));
        public static readonly Contextual MovementUsed       = new(7, nameof(MovementUsed));
        public static readonly Contextual MovementLost       = new(8, nameof(MovementLost));
                                                             
        public static readonly Contextual CountHitsGiven     = new(9, nameof(CountHitsGiven));
        public static readonly Contextual CountHitsTaken     = new(10, nameof(CountHitsTaken));
                                                             
        /// <summary>                                        
        /// Cell ID (int = x + y * width)                    
        /// </summary>                                       
        public static readonly Contextual StartTurnPosition  = new(11, nameof(StartTurnPosition));
        public static readonly Contextual StartFightPosition = new(12, nameof(StartFightPosition));
        /// <summary>                                        
        /// Cell IDs (int = x + y * width)                   
        /// </summary>                                       
        public static readonly Contextual PreviousPositions  = new(13, nameof(PreviousPositions), StatValueType.List);
        public static readonly Contextual NextPositions      = new(14, nameof(NextPositions), StatValueType.List);



        public static readonly Dictionary<CharacteristicId, Contextual> values = new();
        static Contextual()
        {
            var fields = typeof(Contextual).GetFields().Where(f => f.FieldType == typeof(Contextual));
            foreach (var field in fields)
            {
                var value = (Contextual) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
