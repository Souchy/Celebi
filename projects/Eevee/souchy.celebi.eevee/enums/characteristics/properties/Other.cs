﻿
namespace souchy.celebi.eevee.enums.characteristics.properties
{
    public sealed record OtherProperty: CharacteristicType
    {
        public OtherProperty(int localId, string name) : base(CharacteristicCategory.Other, localId, name)
        {
            this.StatValueType = StatValueType.Simple;
        }

        public static readonly OtherProperty Range      = new(0, nameof(Range));
        public static readonly OtherProperty Speed      = new(1, nameof(Speed)); // initiative
        public static readonly OtherProperty Erosion    = new(2, nameof(Erosion));
        public static readonly OtherProperty Echo       = new(3, nameof(Echo)); // number of times it echoes
        /// <summary>
        /// 
        /// </summary>
        public static readonly OtherProperty Lock       = new(4, nameof(Lock)); 
        public static readonly OtherProperty Evasion    = new(5, nameof(Evasion)); 
        /// <summary>
        /// Ap/Mp reduction and parry (retrait & esquive pa/pm)
        /// </summary>
        public static readonly OtherProperty MindPower  = new(6, nameof(MindPower)); 

        /// <summary>
        /// When swapped out, can't be swapped IN right away. <br></br>
        /// Need to wait x turns before swap in
        /// </summary>
        public static readonly OtherProperty SwapInBaseCooldown = new(7, nameof(SwapInBaseCooldown));
        /// <summary>
        /// When swapped in, can't be swapped OUT right away.
        /// Need to wait x turns before swap out
        /// </summary>
        public static readonly OtherProperty SwapOutBaseCooldown = new(8, nameof(SwapOutBaseCooldown));


        public static readonly Dictionary<CharacteristicId, OtherProperty> values = new();
        static OtherProperty()
        {
            var fields = typeof(OtherProperty).GetFields()
                .Where(f => f.FieldType == typeof(OtherProperty));
            foreach (var field in fields)
            {
                var value = (OtherProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
