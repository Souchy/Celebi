
namespace souchy.celebi.eevee.enums.characteristics.creature
{
    /// <summary>
    /// For Creatures only. <br></br>
    /// Some of these stats reset every turn (resources) <br></br>
    /// The previous positions dont reset though. <para></para>
    /// </summary>
    public sealed record Contextual : CharacteristicType
    {
        public Contextual(int localId, string name, StatValueType valueType = StatValueType.Simple) : base(CharacteristicCategory.Contextual, localId, name)
        {
            this.StatValueType = valueType;
        }

        // TODO feel like putting those contextual resource properties into Resource yaknow, then add [gained,lost,used] to ResourceProperty enum
        // nah nah nah, it's context, it's just annoying to repeat for all resources

        /// <summary>
        /// Used to cast spells
        /// </summary>
        public static readonly Contextual LifeUsed           = new(1, nameof(LifeUsed));
        /// <summary>
        /// HealReceived? 
        /// </summary>
        public static readonly Contextual HealReceived       = new(2, nameof(HealReceived));
        public static readonly Contextual HealDone           = new(3, nameof(HealReceived));
        /// <summary>
        /// LifeLost rename DamageReceived?
        /// problem is spells that do -100life but are not damage (ex: mob li-fo koutoulou)
        /// </summary>
        public static readonly Contextual DamageTaken        = new(4, nameof(DamageTaken));
        public static readonly Contextual DamageDone         = new(5, nameof(DamageDone));

        /// <summary>
        /// Mana used to cast spells
        /// </summary>
        public static readonly Contextual ManaUsed           = new(11, nameof(ManaUsed));
        /// <summary>
        /// Mana gained on self by stimulant, telefrag, etc <br></br>
        /// telefrag is good example because it can only give mana to self <br></br>
        /// comes from someone doing ManaGiven 
        /// </summary>
        public static readonly Contextual ManaGained         = new(12, nameof(ManaGained));
        /// <summary>
        /// Mana given to self or to others via stimulant, relai, boite à outil, etc <br></br>
        /// the target will have ManaGained in response
        /// </summary>
        public static readonly Contextual ManaGiven          = new(13, nameof(ManaGained));
        /// <summary>
        /// Mana that has been reduced on self by self or by other targets (ralentissement, sablier, but on self) <br></br>
        /// comes from someone doing ManaReduced
        /// </summary>
        public static readonly Contextual ManaLost           = new(14, nameof(ManaLost));
        /// <summary>
        ///  reduced on self or other targets (ralentissement, sablier...) <br></br>
        ///  the target will hvae ManaLost in response
        /// </summary>
        public static readonly Contextual ManaReduced        = new(15, nameof(ManaReduced));

        //
        public static readonly Contextual MovementUsed       = new(21, nameof(MovementUsed));
        public static readonly Contextual MovementGained     = new(22, nameof(MovementGained));
        public static readonly Contextual MovementGiven      = new(23, nameof(MovementGained));
        public static readonly Contextual MovementLost       = new(24, nameof(MovementLost));
        public static readonly Contextual MovementReduced    = new(25, nameof(MovementReduced));
        //
        public static readonly Contextual RageUsed           = new(31, nameof(RageUsed));
        public static readonly Contextual RageGained         = new(32, nameof(RageGained));
        public static readonly Contextual RageGiven          = new(33, nameof(RageGained));
        public static readonly Contextual RageLost           = new(34, nameof(RageLost));
        public static readonly Contextual RageReduced        = new(35, nameof(RageReduced));


        //
        public static readonly Contextual CountHitsGiven     = new(100, nameof(CountHitsGiven));
        public static readonly Contextual CountHitsTaken     = new(101, nameof(CountHitsTaken));
        /// <summary>
        /// See Other.SwapInCooldown. This needs to go down -1 every turn
        /// </summary>
        public static readonly Contextual SwapInRemainingCooldown  = new(102, nameof(SwapInRemainingCooldown));
        /// <summary>
        /// See Other.SwapOutCooldown. This needs to go down -1 every turn
        /// </summary>
        public static readonly Contextual SwapOutRemainingCooldown = new(103, nameof(SwapOutRemainingCooldown));



        /// <summary>                                        
        /// Cell ID (int = x + y * width)                    
        /// </summary>                                       
        public static readonly Contextual StartTurnPosition  = new(200, nameof(StartTurnPosition));
        public static readonly Contextual StartFightPosition = new(201, nameof(StartFightPosition));
        /// <summary>                                        
        /// Cell IDs (int = x + y * width)                   
        /// </summary>                                       
        public static readonly Contextual PreviousPositions  = new(202, nameof(PreviousPositions), StatValueType.List);
        public static readonly Contextual NextPositions      = new(203, nameof(NextPositions), StatValueType.List);

        // spells cast? list<(spellid, cell)>
        // damage done by current spell 
        // 

        


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
