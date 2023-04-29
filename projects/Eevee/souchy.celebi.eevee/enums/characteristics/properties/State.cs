using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record State : CharacteristicType
    {
        public State(int localId, string name) : base(CharacteristicCategory.State, localId, name)
        {
            this.StatValueType = StatValueType.Bool;
            this.Factory = BoolFactory;
        }

        public static readonly State Visible     = new(16, nameof(Visible    ));
        public static readonly State Ghosted     = new(17, nameof(Ghosted    )); // phasing // is 30% opacity, unlocks line of sight, but blocks movement
        public static readonly State Flying      = new(18, nameof(Flying     )); // slash Hovering
        public static readonly State Underground = new(19, nameof(Underground));
        public static readonly State Drenched    = new(20, nameof(Drenched   )); // wet
        public static readonly State Shocked     = new(21, nameof(Shocked    )); // 
        public static readonly State Hot         = new(22, nameof(Hot        )); // burning
        public static readonly State Grounded    = new(23, nameof(Grounded   )); // Muddy
        public static readonly State Unmoveable  = new(24, nameof(Unmoveable )); // everything 
        public static readonly State Rooted      = new(25, nameof(Rooted     )); // can't translate (dash/push/pull)
        public static readonly State Gravity     = new(26, nameof(Gravity    )); // can't teleport
        public static readonly State Heavy       = new(27, nameof(Heavy      )); // carry/throw
        public static readonly State Carrying    = new(28, nameof(Carrying   ));
        public static readonly State Carried     = new(29, nameof(Carried    ));
        public static readonly State Pacifist    = new(30, nameof(Pacifist   ));


        public static readonly Dictionary<CharacteristicId, State> values = new();
        static State()
        {
            var fields = typeof(State).GetFields();
            foreach (var field in fields)
            {
                var value = (State) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
