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

        public static readonly State Visible              = new(1,  nameof(Visible    ));
        public static readonly State Ghosted              = new(2,  nameof(Ghosted    )); // phasing // is 30% opacity, unlocks line of sight, but blocks movement
        public static readonly State Flying               = new(3,  nameof(Flying     )); // slash Hovering
        public static readonly State Underground          = new(4,  nameof(Underground));
        public static readonly State Drenched             = new(5,  nameof(Drenched   )); // wet
        public static readonly State Shocked              = new(6,  nameof(Shocked    )); // 
        public static readonly State Hot                  = new(7,  nameof(Hot        )); // burning
        public static readonly State Grounded             = new(8,  nameof(Grounded   )); // Muddy
        public static readonly State Unmoveable           = new(9,  nameof(Unmoveable )); // everything 
        public static readonly State Rooted               = new(10, nameof(Rooted     )); // can't translate (dash/push/pull)
        public static readonly State Gravity              = new(11, nameof(Gravity    )); // can't teleport
        public static readonly State Heavy                = new(12, nameof(Heavy      )); // carry/throw
        public static readonly State Carrying             = new(13, nameof(Carrying   ));
        public static readonly State Carried              = new(14, nameof(Carried    ));
        public static readonly State Pacifist             = new(15, nameof(Pacifist   ));
        public static readonly State Invulnerable         = new(16, nameof(Invulnerable));
        public static readonly State InvulnerableDistance = new(17, nameof(InvulnerableDistance));
        public static readonly State InvulnerableMelee    = new(18, nameof(InvulnerableMelee));


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
