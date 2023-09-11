using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    /// <summary>
    /// Often states will have an icon over the creature
    /// </summary>
    public sealed record State : CharacteristicType
    {
        public State(int localId, string name) : base(CharacteristicCategory.State, localId, name)
        {
            this.StatValueType = StatValueType.Bool;
        }

        /// <summary>
        /// Does not block LoS but blocks movement.
        /// Cannot be locked.
        /// </summary>
        public static readonly State Visible              = new(1,  nameof(Visible    ));
        /// <summary>
        /// Phasing. 30% opacity. Still visible (blocks los), but does not block movement. Can walk through other creatures
        /// Cannot be locked.
        /// </summary>
        public static readonly State Ghosted              = new(2,  nameof(Ghosted    ));
        /// <summary>
        /// Like pokemon's Fly.
        /// Out of range of some ground-level spells
        /// </summary>
        public static readonly State Flying               = new(3,  nameof(Flying     )); 
        /// <summary>
        /// Like pokemon's Dig.
        /// Out of range of some above-only spells.
        /// </summary>
        public static readonly State Underground          = new(4,  nameof(Underground));

        public static readonly State Drenched             = new(5,  nameof(Drenched   )); // wet (water)
        public static readonly State Shocked              = new(6,  nameof(Shocked    )); // (air)
        public static readonly State Hot                  = new(7,  nameof(Hot        )); // burning (fire)
        public static readonly State Grounded             = new(8,  nameof(Grounded   )); // Muddy (earth)

        /// <summary>
        /// Cannot translate or teleport or be carried
        /// </summary>
        public static readonly State Unmoveable           = new(9,  nameof(Unmoveable )); 
        /// <summary>
        /// Cannot translate (dash, push, pull...)
        /// </summary>
        public static readonly State Rooted               = new(10, nameof(Rooted     )); 
        /// <summary>
        /// Cannot teleport
        /// </summary>
        public static readonly State Gravity              = new(11, nameof(Gravity    ));
        /// <summary>
        /// Cannot be carried.
        /// If already carried, can only be thrown 1 cell (so in melee).
        /// </summary>
        public static readonly State Heavy                = new(12, nameof(Heavy      ));
        /// <summary>
        /// Carrying/portant another creature
        /// </summary>
        public static readonly State Carrying             = new(13, nameof(Carrying   ));
        /// <summary>
        /// Carried/porté by another creature
        /// </summary>
        public static readonly State Carried              = new(14, nameof(Carried    ));
        /// <summary>
        /// Cannot do damage (deal 0)
        /// </summary>
        public static readonly State Pacifist             = new(15, nameof(Pacifist   ));
        /// <summary>
        /// Receive no damage (0)
        /// </summary>
        public static readonly State Invulnerable         = new(16, nameof(Invulnerable));
        public static readonly State InvulnerableDistance = new(17, nameof(InvulnerableDistance));
        public static readonly State InvulnerableMelee    = new(18, nameof(InvulnerableMelee));
        /// <summary>
        /// Unlocks all LoS on all spells on the creature. ex: cra.Acuité Absolue
        /// </summary>
        public static readonly State UnlockLoS            = new(19, nameof(UnlockLoS));
        /// <summary>
        /// Makes every spell now require a line of sight
        /// </summary>
        public static readonly State LockLoS              = new(20, nameof(LockLoS));

        /// <summary>
        /// Cannot be locked by other creatures
        /// </summary>
        public static readonly State Unlockable           = new(21, nameof(Unlockable));
        /// <summary>
        /// Cannot lock other creatures
        /// </summary>
        public static readonly State Unlocker             = new(22, nameof(Unlocker));



        public static readonly Dictionary<CharacteristicId, State> values = new();
        static State()
        {
            var fields = typeof(State).GetFields().Where(f => f.FieldType == typeof(State));
            foreach (var field in fields)
            {
                var value = (State) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
