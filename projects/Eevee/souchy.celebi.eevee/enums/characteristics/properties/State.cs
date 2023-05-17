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
            this.Factory = BoolFactory;
        }

        public static readonly State Visible              = new(0,  nameof(Visible    ));
        public static readonly State Ghosted              = new(1,  nameof(Ghosted    )); // phasing // is 30% opacity, unlocks line of sight, but blocks movement
        public static readonly State Flying               = new(2,  nameof(Flying     )); // slash Hovering
        public static readonly State Underground          = new(3,  nameof(Underground));
        public static readonly State Drenched             = new(4,  nameof(Drenched   )); // wet
        public static readonly State Shocked              = new(5,  nameof(Shocked    )); // 
        public static readonly State Hot                  = new(6,  nameof(Hot        )); // burning
        public static readonly State Grounded             = new(7,  nameof(Grounded   )); // Muddy
        public static readonly State Unmoveable           = new(8,  nameof(Unmoveable )); // everything 
        public static readonly State Rooted               = new(9,  nameof(Rooted     )); // can't translate (dash/push/pull)
        public static readonly State Gravity              = new(10, nameof(Gravity    )); // can't teleport
        public static readonly State Heavy                = new(11, nameof(Heavy      )); // carry/throw
        public static readonly State Carrying             = new(12, nameof(Carrying   ));
        public static readonly State Carried              = new(13, nameof(Carried    ));
        public static readonly State Pacifist             = new(14, nameof(Pacifist   ));
        public static readonly State Invulnerable         = new(15, nameof(Invulnerable));
        public static readonly State InvulnerableDistance = new(16, nameof(InvulnerableDistance));
        public static readonly State InvulnerableMelee    = new(17, nameof(InvulnerableMelee));


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
