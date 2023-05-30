using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record SpellModelProperty: CharacteristicType
    {
        public SpellModelProperty(int localId, string name, StatValueType statValueType = StatValueType.Simple, object defaultValue = null) : base(CharacteristicCategory.SpellModel, localId, name, defaultValue)
        {
            this.StatValueType = statValueType;
        }

        public static readonly SpellModelProperty MaxCharges        = new(1, nameof(MaxCharges), defaultValue: 0);
        public static readonly SpellModelProperty MaxCastPerTarget  = new(2, nameof(MaxCastPerTarget), defaultValue: int.MaxValue);
        public static readonly SpellModelProperty MaxCastPerTurn    = new(3, nameof(MaxCastPerTurn), defaultValue: int.MaxValue);
        public static readonly SpellModelProperty Cooldown          = new(4, nameof(Cooldown), defaultValue: 0);
        public static readonly SpellModelProperty CooldownInitial   = new(5, nameof(CooldownInitial), defaultValue: 0);
        public static readonly SpellModelProperty CooldownGlobal    = new(6, nameof(CooldownGlobal), defaultValue: 0);
        /// <summary>
        /// Number of times the spell can pierce creatures
        /// </summary>
        public static readonly SpellModelProperty MaxPierces        = new(7, nameof(MaxPierces), defaultValue: 0);
        /// <summary>
        /// Adds to the SpellChainSchema.maxChains prop
        /// </summary>
        public static readonly SpellModelProperty MaxChains         = new(8, nameof(MaxChains), defaultValue: 0);
        /// <summary>
        /// Adds to the SpellChainSchema.chainZone.size.x prop
        /// </summary>
        public static readonly SpellModelProperty ChainRange        = new(9, nameof(ChainRange), defaultValue: 0);
        /// <summary>
        /// Forks operate inside a Chain.  <br></br>
        /// It adds to the number of samples in the chain zone (Zone.maxSampleCount).
        /// </summary>
        public static readonly SpellModelProperty MaxForks          = new(10, nameof(MaxForks), defaultValue: 0);


        // bools
        public static readonly SpellModelProperty LineOfSightRequired   = new(20, nameof(LineOfSightRequired), StatValueType.Bool, defaultValue: true);
        /// <summary>
        /// Wether or not we tell every other player what was the spell's target cell <br></br>
        /// ex: éclipse, pièges, roublardise ou lorsqu'on est invisible <br></br>
        /// donc tous des cellStatus invisible ou des perso invisible
        ///     roublardise peut être codé pour mettre invi avant de cast le spell
        ///     
        /// </summary>
        public static readonly SpellModelProperty BroadcastTargetedCell = new(21, nameof(BroadcastTargetedCell), StatValueType.Bool, defaultValue: true);
        public static readonly SpellModelProperty IsRangeModifiable     = new(22, nameof(IsRangeModifiable), StatValueType.Bool, defaultValue: true);




        public static readonly Dictionary<CharacteristicId, SpellModelProperty> values = new();
        static SpellModelProperty()
        {
            var fields = typeof(SpellModelProperty).GetFields().Where(f => f.FieldType == typeof(SpellModelProperty));
            foreach (var field in fields)
            {
                var value = (SpellModelProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
    //public sealed record SpellModelState : CharacteristicType
    //{
    //    public SpellModelState(int localId, string name) : base(CharacteristicCategory.SpellModel, localId, name)
    //    {
    //        this.StatValueType = StatValueType.Bool;
    //        this.Factory = BoolFactory;
    //    }

    //    public static readonly SpellModelState LineOfSightRequired = new(0, nameof(LineOfSightRequired));
    //    /// <summary>
    //    /// Wether or not we tell every other player what was the spell's target cell <br></br>
    //    /// ex: éclipse, pièges, roublardise ou lorsqu'on est invisible <br></br>
    //    /// donc tous des cellStatus invisible ou des perso invisible
    //    ///     roublardise peut être codé pour mettre invi avant de cast le spell
    //    ///     
    //    /// </summary>
    //    public static readonly SpellModelState BroadcastTargetedCell = new(1, nameof(BroadcastTargetedCell));


    //    public static readonly Dictionary<CharacteristicId, SpellModelState> values = new();
    //    static SpellModelState()
    //    {
    //        var fields = typeof(SpellModelState).GetFields().Where(f => f.FieldType == typeof(SpellModelState));
    //        foreach (var field in fields)
    //        {
    //            var value = (SpellModelState) field.GetValue(null);
    //            values[value.ID] = value;
    //        }
    //    }
    //}

}
