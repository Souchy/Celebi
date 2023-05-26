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
        public SpellModelProperty(int localId, string name) : base(CharacteristicCategory.SpellModel, localId, name)
        {
            this.StatValueType = StatValueType.Simple;
            this.Factory = SimpleFactory;
        }

        public static readonly SpellModelProperty MaxCharges        = new(1, nameof(MaxCharges));
        public static readonly SpellModelProperty MaxCastPerTarget  = new(2, nameof(MaxCastPerTarget));
        public static readonly SpellModelProperty MaxCastPerTurn    = new(3, nameof(MaxCastPerTurn));
        public static readonly SpellModelProperty Cooldown          = new(4, nameof(Cooldown));
        public static readonly SpellModelProperty CooldownInitial   = new(5, nameof(CooldownInitial));
        public static readonly SpellModelProperty CooldownGlobal    = new(6, nameof(CooldownGlobal));
        /// <summary>
        /// Number of times the spell can pierce creatures
        /// </summary>
        public static readonly SpellModelProperty MaxPierces        = new(7, nameof(MaxPierces));
        /// <summary>
        /// Adds to the SpellChainSchema.maxChains prop
        /// </summary>
        public static readonly SpellModelProperty MaxChains         = new(8, nameof(MaxChains));
        /// <summary>
        /// Adds to the SpellChainSchema.zone.size.x prop
        /// </summary>
        public static readonly SpellModelProperty ChainRange        = new(9, nameof(ChainRange));
        /// <summary>
        /// Forks operate inside a Chain.  <br></br>
        /// It adds to the number of samples in the chain zone (Zone.maxSampleCount).
        /// </summary>
        public static readonly SpellModelProperty MaxForks          = new(10, nameof(MaxForks));


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
    public sealed record SpellModelState : CharacteristicType
    {
        public SpellModelState(int localId, string name) : base(CharacteristicCategory.SpellModel, localId, name)
        {
            this.StatValueType = StatValueType.Bool;
            this.Factory = BoolFactory;
        }

        public static readonly SpellModelState LineOfSightRequired = new(0, nameof(LineOfSightRequired));
        /// <summary>
        /// Wether or not we tell every other player what was the spell's target cell <br></br>
        /// ex: éclipse, pièges, roublardise ou lorsqu'on est invisible <br></br>
        /// donc tous des cellStatus invisible ou des perso invisible
        ///     roublardise peut être codé pour mettre invi avant de cast le spell
        ///     
        /// </summary>
        public static readonly SpellModelState BroadcastTargetedCell = new(1, nameof(BroadcastTargetedCell));


        public static readonly Dictionary<CharacteristicId, SpellModelState> values = new();
        static SpellModelState()
        {
            var fields = typeof(SpellModelState).GetFields().Where(f => f.FieldType == typeof(SpellModelState));
            foreach (var field in fields)
            {
                var value = (SpellModelState) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
